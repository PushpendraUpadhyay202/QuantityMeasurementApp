using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using QuantityMeasurementData;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth;

public class AuthService:IAuthService
{
    private readonly QuantityDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(QuantityDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public string Register(string username, string password)
    {
        PasswordHelper.CreatePasswordHash(password, out string hash, out string salt);

        var user = new UserEntity
        {
            Username = username,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return "User registered";
    }

    public string Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == username);

        if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Invalid credentials");

        return GenerateToken(user);
    }

    

public async Task<string> GoogleLogin(string idToken)
{
    var settings = new GoogleJsonWebSignature.ValidationSettings
    {
        Audience = new List<string> { _config["GoogleAuth:ClientId"] }
    };

    var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

    
    var email = payload.Email;
    var name = payload.Name;

    // Check if user exists
    var user = _context.Users.FirstOrDefault(x => x.Username == email);

    if (user == null)
    {
        user = new UserEntity
        {
            Username = email,
            PasswordHash = "", 
            PasswordSalt = ""
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    // Generate  JWT
    return GenerateToken(user);
}

    private string GenerateToken(UserEntity user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}