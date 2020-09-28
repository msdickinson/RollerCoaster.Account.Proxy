using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models.RefreshTokens
{
    [ExcludeFromCodeCoverage]
    public class RefreshTokensRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
