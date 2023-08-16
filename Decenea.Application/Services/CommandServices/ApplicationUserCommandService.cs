using System.Security.Cryptography;
using Decenea.Application.Mappers;
using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Common;
using Decenea.Domain.Constants;
using Decenea.Domain.DataTransferObjects.ApplicationUser.LoginApplicationUser;
using Decenea.Domain.DataTransferObjects.ApplicationUser.RegisterApplicationUser;
using Decenea.Domain.Entities.ApplicationUser;
using Decenea.Infrastructure.Data;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.CommandServices;

public class ApplicationUserCommandService : IApplicationUserCommandService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly DeceneaDbContext _dbContext;
    public ApplicationUserCommandService(UserManager<ApplicationUser> userManager, DeceneaDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    public async Task<Result<IdentityResult, Exception>> RegisterUser(RegisterApplicationUserRequest request)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = new ApplicationUser()
            {
                FirstName = request.FirstName,
                Email = request.Email,
                UserName = request.Email,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                PhoneNumber = request.PhoneNumber,
                CreatedBy = "ApplicationUserCommandService",
                ResidenceOf = request.ResidenceOf
            };
            
            var registration = await _userManager
                .CreateAsync(user, request.Password);

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

    public async Task<Result<LoginApplicationUserDto, Exception>> LoginUser(LoginApplicationUserRequest request)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var userDto = new LoginApplicationUserDto();
            
            var user = await _userManager.Users
                .Include(i =>i.UserRoles)
                .ThenInclude(i => i.Role)
                .FirstOrDefaultAsync(i=> i.Email == request.Email);
            
            if (user is null)
                return Result<LoginApplicationUserDto, Exception>.Anticipated(null,"User not found.");
            
            if(user.LockoutEnabled)
                return Result<LoginApplicationUserDto, Exception>.Anticipated(null,"User is locked.");
            
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Result<LoginApplicationUserDto, Exception>.Anticipated(userDto,"Credentials don't match.");
            }

            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: "ApplicationTokenSigningKey",
                expireAt: accessTokenExpiryTime,
                priviledges: u =>
                {
                    u.Roles.AddRange(user.UserRoles.Select(i => i.Role.Name));
                    
                    u.Permissions.AddRange(new[] { "Browse" });
                    
                    u.Claims.Add(new("UserName", request.Email));
                    
                    u["UserID"] = user.Id.ToString(); //indexer based claim setting
                });
            
            if (request.RememberMe)
            {
                var randomBytes = RandomNumberGenerator.GetBytes(64);
                var refreshToken = Convert.ToBase64String(randomBytes);
                var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
                userDto.RefreshToken = refreshToken;
                userDto.RefreshTokenExpiryTime = refreshTokenExpiryTime;
                await _userManager.UpdateAsync(user);
            }
            
            userDto.AccessToken = jwtToken;
            userDto.AccessTokenExpiryTime = accessTokenExpiryTime;
            user.ApplicationUserToLoginApplicationUserDto(userDto);

            await transaction.CommitAsync();
            return Result<LoginApplicationUserDto, Exception>.Anticipated(userDto);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result<LoginApplicationUserDto, Exception>
                .Excepted(ex,$"Didn't manage to login user{request.Email}");
        }
    }
}