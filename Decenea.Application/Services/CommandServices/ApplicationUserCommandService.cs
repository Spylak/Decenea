using Decenea.Application.Advertisements.Commands.LoginUser;
using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;
using Decenea.Domain.Helpers;
using Decenea.Infrastructure.Persistance;
using Decenea.Shared.DataTransferObjects.ApplicationUser;
using Decenea.Shared.DataTransferObjects.Auth;
using Decenea.Shared.Extensions;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Services.CommandServices;

public class ApplicationUserCommandService : IApplicationUserCommandService
{
    private readonly UserManager<User> _userManager;
    private readonly DeceneaDbContext _dbContext;
    public ApplicationUserCommandService(UserManager<User> userManager, DeceneaDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
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