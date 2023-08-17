namespace Decenea.Domain.DataTransferObjects.Auth;

public record RefreshTokenDto(string RefreshToken, DateTime RefreshTokenExpiryTime);