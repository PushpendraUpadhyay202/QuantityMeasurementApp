using System.Text.RegularExpressions;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
namespace BusinessLayer.Validators
{
    public class UserValidator: IUserValidator
    {
        public void ValidateSignup(SignupRequestDto request)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            if(request == null)
                throw new ArgumentException("Request cannot be null");
            
            if(string.IsNullOrWhiteSpace(request.FirstName))
                throw new ArgumentException("First name is required");

            if(string.IsNullOrWhiteSpace(request.LastName))
                throw new ArgumentException("Last name is required");

            if(!Regex.IsMatch(request.Email, emailPattern))
                throw new ArgumentException("Invalid email");

            if(!Regex.IsMatch(request.Password, passwordPattern))
                throw new ArgumentException("Weak password");
        }
    }
}