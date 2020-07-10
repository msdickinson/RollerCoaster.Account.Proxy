using RollerCoaster.Account.Proxy.Models;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.API.Proxy.Models
{
    [ExcludeFromCodeCoverage]
    public class AccountProxyOptions
    {
        public string BaseURL { get; set; }
        public int HttpClientTimeoutInSeconds { get; set; }
        public ProxyOption AdminAuthorized { get; set; }
        public ProxyOption CreateAdminAccount { get; set; }
        public ProxyOption CreateUserAccount { get; set; }
        public ProxyOption Log { get; set; }
        public ProxyOption Login { get; set; }
        public ProxyOption UserAuthorized { get; set; }
        public ProxyOption RefreshTokens { get; set; }
        public ProxyOption UpdateEmailPreference { get; set; }
        public ProxyOption UpdateEmailPreferenceWithToken { get; set; }
        public ProxyOption ActivateEmail { get; set; }
        public ProxyOption UpdatePassword { get; set; }
        public ProxyOption ResetPassword { get; set; }
        public ProxyOption RequestPasswordResetEmail { get; set; }
    }
}
