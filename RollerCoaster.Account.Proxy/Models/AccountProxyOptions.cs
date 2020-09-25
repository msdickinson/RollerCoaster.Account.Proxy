using RollerCoaster.Account.Proxy.Models;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.API.Proxy.Models
{
    [ExcludeFromCodeCoverage]
    public class AccountProxyOptions
    {
        public string BaseURL { get; set; }
        public int HttpClientTimeoutInSeconds { get; set; }
        public ProxyOptions AdminAuthorized { get; set; }
        public ProxyOptions CreateAdminAccount { get; set; }
        public ProxyOptions CreateUserAccount { get; set; }
        public ProxyOptions Log { get; set; }
        public ProxyOptions Login { get; set; }
        public ProxyOptions UserAuthorized { get; set; }
        public ProxyOptions RefreshTokens { get; set; }
        public ProxyOptions UpdateEmailPreference { get; set; }
        public ProxyOptions UpdateEmailPreferenceWithToken { get; set; }
        public ProxyOptions ActivateEmail { get; set; }
        public ProxyOptions UpdatePassword { get; set; }
        public ProxyOptions ResetPassword { get; set; }
        public ProxyOptions RequestPasswordResetEmail { get; set; }
    }
}
