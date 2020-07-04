using DickinsonBros.DurableRest.Abstractions;
using DickinsonBros.DurableRest.Abstractions.Models;
using Microsoft.Extensions.Options;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.Proxy.Models;
using RollerCoaster.Account.Proxy.Models.CreateAdminAccount;
using RollerCoaster.Account.Proxy.Models.CreateUserAccount;
using RollerCoaster.Account.Proxy.Models.Login;
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

        public const string AUTHORIZATION = "Authorization";

        public AccountProxyService(IDurableRestService durableRestService, HttpClient httpClient, IOptions<AccountProxyOptions> accountProxyOptions)
        {
            _durableRestService = durableRestService;
            _accountProxyOptions = accountProxyOptions.Value;
            _httpClient = httpClient;
        }

        public async Task<HttpResponse<CreateUserAccountResponse>> CreateUserAccountAsync(CreateUserAccountRequest createUserAccountRequest)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_accountProxyOptions.CreateUserAccount.Resource, UriKind.Relative),
                Content = new StringContent(JsonSerializer.Serialize(createUserAccountRequest), Encoding.UTF8, "application/json")
            };

            return await _durableRestService.ExecuteAsync<CreateUserAccountResponse>(_httpClient, httpRequestMessage, _accountProxyOptions.CreateUserAccount.Retrys, _accountProxyOptions.CreateUserAccount.TimeoutInSeconds).ConfigureAwait(false);
        }

        public async Task<HttpResponse<CreateAdminAccountResponse>> CreateAdminAccountAsync(CreateAdminAccountRequest createAdminAccountRequest)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_accountProxyOptions.CreateAdminAccount.Resource, UriKind.Relative),
                Content = new StringContent(JsonSerializer.Serialize(createAdminAccountRequest), Encoding.UTF8, "application/json")
            };

            return await _durableRestService.ExecuteAsync<CreateAdminAccountResponse>(_httpClient, httpRequestMessage, _accountProxyOptions.CreateAdminAccount.Retrys, _accountProxyOptions.CreateAdminAccount.TimeoutInSeconds).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> LogAsync()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_accountProxyOptions.Log.Resource, UriKind.Relative),
            };

            return await _durableRestService.ExecuteAsync(_httpClient, httpRequestMessage, _accountProxyOptions.Log.Retrys, _accountProxyOptions.Log.TimeoutInSeconds).ConfigureAwait(false);
        }

        public async Task<HttpResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_accountProxyOptions.Login.Resource, UriKind.Relative),
                Content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json")
            };

            return await _durableRestService.ExecuteAsync<LoginResponse>(_httpClient, httpRequestMessage, _accountProxyOptions.Login.Retrys, _accountProxyOptions.Login.TimeoutInSeconds).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> UserAuthorizedAsync(string bearerToken)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_accountProxyOptions.UserAuthorized.Resource, UriKind.Relative)
            };

            httpRequestMessage.Headers.Add(AUTHORIZATION, bearerToken);

            return await _durableRestService.ExecuteAsync(_httpClient, httpRequestMessage, _accountProxyOptions.UserAuthorized.Retrys, _accountProxyOptions.UserAuthorized.TimeoutInSeconds).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> AdminAuthorizedAsync(string bearerToken)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_accountProxyOptions.AdminAuthorized.Resource, UriKind.Relative)
            };

            httpRequestMessage.Headers.Add(AUTHORIZATION, bearerToken);

            return await _durableRestService.ExecuteAsync(_httpClient, httpRequestMessage, _accountProxyOptions.AdminAuthorized.Retrys, _accountProxyOptions.AdminAuthorized.TimeoutInSeconds).ConfigureAwait(false);
        }
    }
}
