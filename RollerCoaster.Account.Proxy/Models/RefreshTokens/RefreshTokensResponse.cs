using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RollerCoaster.Account.Proxy.Models.RefreshTokens
{
    [ExcludeFromCodeCoverage]
    public class RefreshTokensResponse
    {
        public string AccessToken { get; set; }
        public System.DateTime AccessTokenExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public System.DateTime RefreshTokenExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
