using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models.UpdateEmailPreferenceWithToken
{
    [ExcludeFromCodeCoverage]
    public class UpdateEmailPreferenceWithTokenRequest
    {
        public string Token { get; set; }
        public EmailPreference EmailPreference { get; set; }
    }
}
