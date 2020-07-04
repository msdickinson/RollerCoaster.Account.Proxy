using DickinsonBros.DurableRest.Abstractions.Models;
using RollerCoaster.Account.Proxy.Models;
using RollerCoaster.Account.Proxy.Models.CreateAdminAccount;
using RollerCoaster.Account.Proxy.Models.CreateUserAccount;
using RollerCoaster.Account.Proxy.Models.Login;
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
    }
}