using ModelLayer.DTOs;
namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        SignupResponseDto Signup(SignupRequestDto request);
        LoginResponseDto Login(LoginRequestDto request);
    }
}