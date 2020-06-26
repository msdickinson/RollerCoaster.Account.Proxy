using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models
{
    [ExcludeFromCodeCoverage]
    public class CreateAccountRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
