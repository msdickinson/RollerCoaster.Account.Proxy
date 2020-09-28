using DickinsonBros.DurableRest.Abstractions.Models;
using RollerCoaster.Account.Proxy.Models.ActivateEmail;
using RollerCoaster.Account.Proxy.Models.CreateAdminAccount;
using RollerCoaster.Account.Proxy.Models.CreateUserAccount;
using RollerCoaster.Account.Proxy.Models.Login;
using RollerCoaster.Account.Proxy.Models.RefreshTokens;
using RollerCoaster.Account.Proxy.Models.RequestPasswordResetEmail;
using RollerCoaster.Account.Proxy.Models.ResetPassword;
using RollerCoaster.Account.Proxy.Models.UpdateEmailPreference;
using RollerCoaster.Account.Proxy.Models.UpdateEmailPreferenceWithToken;
using RollerCoaster.Account.Proxy.Models.UpdatePassword;
using System.Net.Http;
using System.Threading.Tasks;

namespace RollerCoaster.Account.API.Proxy
{
    public interface IAccountProxyService
    {
        Task<HttpResponse<CreateUserAccountResponse>> CreateUserAccountAsync(CreateUserAccountRequest createUserAccountRequest);
        Task<HttpResponse<CreateAdminAccountResponse>> CreateAdminAccountAsync(CreateAdminAccountRequest createAdminAccountRequest);
        Task<HttpResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        Task<HttpResponseMessage> LogAsync();
        Task<HttpResponseMessage> UserAuthorizedAsync(string bearerToken);
        Task<HttpResponseMessage> AdminAuthorizedAsync(string bearerToken);
        Task<HttpResponse<RefreshTokensResponse>> RefreshTokensAsync(RefreshTokensRequest bearerToken);
        Task<HttpResponseMessage> UpdateEmailPreferenceAsync(UpdateEmailPreferenceRequest updateEmailPreferenceRequest, string bearerToken);
        Task<HttpResponseMessage> UpdateEmailPreferenceWithTokenAsync(UpdateEmailPreferenceWithTokenRequest updateEmailPreferenceWithTokenRequest);
        Task<HttpResponseMessage> ActivateEmailAsync(ActivateEmailRequest activateEmailRequest);
        Task<HttpResponseMessage> UpdatePasswordAsync(UpdatePasswordRequest updatePasswordRequest, string bearerToken);
        Task<HttpResponseMessage> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<HttpResponseMessage> RequestPasswordResetEmailAsync(RequestPasswordResetEmailRequest requestPasswordResetEmailRequest);

    }
}