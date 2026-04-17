using ModelLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IUserValidator
    {
        void ValidateSignup(SignupRequestDto request);
    }
}