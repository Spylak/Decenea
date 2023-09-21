namespace Decenea.Common.DataTransferObjects.Auth;

public record RefreshTokenDto(string RefreshToken, DateTime RefreshTokenExpiryTime);