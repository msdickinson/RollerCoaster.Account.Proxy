using RollerCoaster.Account.Proxy.Models;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.API.Proxy.Models
{
    [ExcludeFromCodeCoverage]
    public class AccountProxyOptions
    {
        public string BaseURL { get; set; }
        public int HttpClientTimeoutInSeconds { get; set; }
        public ProxyOption CreateAccount { get; set; }

    }
}
