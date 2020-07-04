using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models.Login
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
