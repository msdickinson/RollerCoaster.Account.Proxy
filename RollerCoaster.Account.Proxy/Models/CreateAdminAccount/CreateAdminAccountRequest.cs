using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models.CreateAdminAccount
{
    [ExcludeFromCodeCoverage]
    public class CreateAdminAccountRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
