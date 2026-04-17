using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}