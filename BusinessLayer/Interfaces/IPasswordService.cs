using ModelLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IPasswordService
    {
        HashPasswordResponseDto HashPassword(string password);
        VerifyPasswordResponseDto VerifyPassword(string inputPassword, string storedHash);
    }
}
