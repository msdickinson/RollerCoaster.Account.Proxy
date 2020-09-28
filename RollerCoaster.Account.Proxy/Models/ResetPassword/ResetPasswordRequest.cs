using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models.ResetPassword
{
    [ExcludeFromCodeCoverage]
    public class ResetPasswordRequest
    {
        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}
