using BusinessLayer.Interfaces;
using DataAccessLayer.Data;
using ModelLayer.DTOs;
using ModelLayer.Models;
namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private AppDbContext _context;
        private IUserValidator _validator;
        private IPasswordService _passwordService;
        private IJwtService _jwtService;

        public AuthService(AppDbContext context, IUserValidator validator, IPasswordService passwordService, IJwtService jwtService)
        {
            _context = context;
            _validator = validator;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public SignupResponseDto Signup(SignupRequestDto request)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email.Equals(request.Email));

            if (user != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            _validator.ValidateSignup(request);
            string hashedPassword = _passwordService.HashPassword(request.Password).HashedPassword;
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                HashedPassword = hashedPassword
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new SignupResponseDto
            {
                UserId = newUser.UserId,
                Email = request.Email,
            };
        }

        public LoginResponseDto Login(LoginRequestDto request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
                throw new KeyNotFoundException("Invalid email or password");

            var verification = _passwordService.VerifyPassword(request.Password, user.HashedPassword);

            if (!verification.IsValid)
                throw new UnauthorizedAccessException("Invalid email or password");

            string token = _jwtService.GenerateToken(user);

            return new LoginResponseDto { Token = token };
        }
    }
}