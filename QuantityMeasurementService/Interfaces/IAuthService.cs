public interface IAuthService
{
    string Register(string username,string password);
    string Login(string username, string password);

    Task<string> GoogleLogin(string idToken);
}