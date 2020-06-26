using DickinsonBros.DurableRest.Abstractions.Models;
using RollerCoaster.Account.Proxy.Models;
using System.Threading.Tasks;

namespace RollerCoaster.Account.API.Proxy
{
    public interface IAccountProxyService
    {
        Task<HttpResponse<CreateAccountResponse>> CreateAccountAsync(CreateAccountRequest createAccountRequest);
    }
}