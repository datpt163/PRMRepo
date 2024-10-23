using Capstone.Application.Module.Auths.Response;


namespace Capstone.Application.Module.Auth.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public RegisterResponse? User { get; set; }
    }
}
