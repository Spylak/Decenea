using Decenea.WebAPI.Services.CommandServices.ICommandServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Decenea.Domain.DataTransferObjects.Auth;
using Decenea.Domain.Entities.ApplicationUserEntities;
using Decenea.Shared.Extensions;
using Decenea.WebAPI.Domain.Constants;
using Decenea.WebAPI.Domain.Helpers;
using Decenea.WebAPI.Domain.Mappers;
using Decenea.WebAPI.Infrastructure.Data;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.WebAPI.Services.CommandServices;

public class ApplicationUserCommandService : IApplicationUserCommandService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly DeceneaDbContext _dbContext;
    public ApplicationUserCommandService(UserManager<ApplicationUser> userManager, DeceneaDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    public async Task<Result<IdentityResult, Exception>> RegisterUser(RegisterApplicationUserRequestDto requestDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = new ApplicationUser()
            {
                FirstName = requestDto.FirstName,
                Email = requestDto.Email,
                UserName = requestDto.Email,
                LastName = requestDto.LastName,
                MiddleName = requestDto.MiddleName,
                PhoneNumber = requestDto.PhoneNumber,
                CreatedBy = "ApplicationUserCommandService",
                ResidenceOf = requestDto.ResidenceOf
            };
            
            var registration = await _userManager
                .CreateAsync(user, requestDto.Password);

            if (!registration.Succeeded)
            {
                await transaction.RollbackAsync();
                return Result<IdentityResult, Exception>.Anticipated(registration);
            }

            var roleResult = await _userManager
                .AddToRoleAsync(user, ApplicationRoles.Guest);
            if (!roleResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return Result<IdentityResult, Exception>.Anticipated(roleResult);
            }
            
            await transaction.CommitAsync();
            return Result<IdentityResult, Exception>.Anticipated(roleResult);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result<IdentityResult, Exception>.Excepted(ex);
        }
    }

    public async Task<Result<LoginApplicationUserResponseDto, Exception>> LoginUser(LoginApplicationUserRequestDto requestDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var userDto = new LoginApplicationUserResponseDto();
            
            var user = await _userManager.Users
                .Include(i =>i.UserRoles)
                .ThenInclude(i => i.Role)
                .FirstOrDefaultAsync(i=> i.Email == requestDto.Email);
            
            if (user is null)
                return Result<LoginApplicationUserResponseDto, Exception>.Anticipated(null,"User not found.");
            
            if(user.LockoutEnabled)
                return Result<LoginApplicationUserResponseDto, Exception>.Anticipated(null,"User is locked.");
            
            if (!await _userManager.CheckPasswordAsync(user, requestDto.Password))
            {
                return Result<LoginApplicationUserResponseDto, Exception>.Anticipated(userDto,"Credentials don't match.");
            }
            
            var userRoles = user.UserRoles
                .Select(i => i.Role.Name)
                .Where(i => i != null)
                .Cast<string>() 
                .ToList();

            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: "ApplicationTokenSigningKey",
                expireAt: accessTokenExpiryTime,
                priviledges: u =>
                {
                    u.Roles.AddRange(userRoles);
                    
                    u.Permissions.AddRange(new[] { "Browse" });
                    
                    u.Claims.Add(new("UserName", requestDto.Email));
                    
                    u["UserId"] = user.Id.ToString(); //indexer based claim setting
                    u["CityId"] = user.CityId; 
                });
            
            if (requestDto.RememberMe)
            {
                var refreshToken = AuthTokenHelper.GenerateRefreshToken();
                userDto.RefreshToken = refreshToken.RefreshToken;
                userDto.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                user.RefreshToken = refreshToken.RefreshToken;
                user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                await _userManager.UpdateAsync(user);
            }
            
            userDto.AccessToken = jwtToken;
            userDto.AccessTokenExpiryTime = accessTokenExpiryTime;
            user.ApplicationUserToLoginApplicationUserDto(userDto);

            await transaction.CommitAsync();
            return Result<LoginApplicationUserResponseDto, Exception>.Anticipated(userDto);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result<LoginApplicationUserResponseDto, Exception>
                .Excepted(ex,$"Didn't manage to login user{requestDto.Email}");
        }
    }

    public async Task<Result<RegenerateAuthTokensResponseDto, Exception>> RegenerateAuthTokens(RegenerateAuthTokensRequestDto requestDto)
    {
        
        var claims = AuthTokenHelper
            .GetTokenClaims(requestDto.AccessToken);
        
        if (claims.Value is null)
        {
            return Result<RegenerateAuthTokensResponseDto, Exception>
                .Excepted(claims.Exception);
        }
        
        var username = claims.Value
            .GetUserNameClaimValue();       
        
        if (username is null)
        {
            return Result<RegenerateAuthTokensResponseDto, Exception>
                .Anticipated(null,"Username not found.");
        }
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
         
        try
        {
            var user = await _userManager
                .Users
                .Include(i => i.UserRoles)
                .ThenInclude(i => i.Role)
                .FirstOrDefaultAsync(i=> i.UserName == username);
            
            if (user is null)
                return Result<RegenerateAuthTokensResponseDto, Exception>.Anticipated(null,"User not found.");
            
            if(user.RefreshToken != requestDto.RefreshToken)
                return Result<RegenerateAuthTokensResponseDto, Exception>.Anticipated(null,"Invalid refresh token.");
            
            if(user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Result<RegenerateAuthTokensResponseDto, Exception>.Anticipated(null,"Expired refresh token.");
            
            var userRoles = user.UserRoles
                .Select(i => i.Role.Name)
                .Where(i => i != null)
                .Cast<string>() 
                .ToList();
            
            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: "ApplicationTokenSigningKey",
                expireAt: accessTokenExpiryTime,
                priviledges: u =>
                {
                    u.Roles.AddRange(userRoles);
                    
                    u.Permissions.AddRange(new[] { "Browse" , "Edit" });
                    
                    u.Claims.Add(new("UserName", username));
                    
                    u["UserId"] = user.Id.ToString(); //indexer based claim setting
                    u["CityId"] = user.CityId; 
                });

            var refreshToken = AuthTokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken.RefreshToken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
            await _userManager.UpdateAsync(user);
            await transaction.CommitAsync();

            return Result<RegenerateAuthTokensResponseDto, Exception>
                .Anticipated(new RegenerateAuthTokensResponseDto(jwtToken,refreshToken.RefreshToken,refreshToken.RefreshTokenExpiryTime,accessTokenExpiryTime));
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Log.Error("Failed to RegenerateAuthTokens for {user}, with error : {ex}",username,e);
            return Result<RegenerateAuthTokensResponseDto, Exception>
                .Excepted(e,$"Didn't manage to RegenerateAuthTokens user: {username}");
        }
    }
}