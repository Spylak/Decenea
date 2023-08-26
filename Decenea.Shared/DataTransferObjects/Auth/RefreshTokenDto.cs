namespace Decenea.Shared.DataTransferObjects.Auth;

public record RefreshTokenDto(string RefreshToken, DateTime RefreshTokenExpiryTime);