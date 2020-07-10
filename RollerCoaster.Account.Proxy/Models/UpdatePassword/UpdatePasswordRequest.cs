using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models.UpdatePassword
{
    [ExcludeFromCodeCoverage]
    public class UpdatePasswordRequest
    {
        public string ExistingPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
