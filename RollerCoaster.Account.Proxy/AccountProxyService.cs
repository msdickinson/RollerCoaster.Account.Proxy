using DickinsonBros.DurableRest.Abstractions;
using DickinsonBros.DurableRest.Abstractions.Models;
using Microsoft.Extensions.Options;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.Proxy.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RollerCoaster.Account.API.Proxy
{
    public class AccountProxyService : IAccountProxyService
    {
        internal readonly AccountProxyOptions _accountProxyOptions;
        internal readonly IDurableRestService _durableRestService;
        internal readonly HttpClient _httpClient;
        public AccountProxyService(IDurableRestService durableRestService, HttpClient httpClient, IOptions<AccountProxyOptions> accountProxyOptions)
        {
            _durableRestService = durableRestService;
            _accountProxyOptions = accountProxyOptions.Value;
            _httpClient = httpClient;
        }

        public async Task<HttpResponse<CreateAccountResponse>> CreateAccountAsync(CreateAccountRequest createAccountRequest)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_accountProxyOptions.CreateAccount.Resource, UriKind.Relative),
                Content = new StringContent(JsonSerializer.Serialize(createAccountRequest), Encoding.UTF8, "application/json")
            };

            return await _durableRestService.ExecuteAsync<CreateAccountResponse>(_httpClient, httpRequestMessage, _accountProxyOptions.CreateAccount.Retrys, _accountProxyOptions.CreateAccount.TimeoutInSeconds).ConfigureAwait(false);
        }
    }
}
