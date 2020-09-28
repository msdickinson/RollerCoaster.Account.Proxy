using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models.CreateUserAccount
{
    [ExcludeFromCodeCoverage]
    public class CreateUserAccountResponse
    {
        public string AccessToken { get; set; }
        public System.DateTime AccessTokenExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public System.DateTime RefreshTokenExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
