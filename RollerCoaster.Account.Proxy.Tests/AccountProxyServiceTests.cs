using DickinsonBros.DurableRest.Abstractions;
using DickinsonBros.DurableRest.Abstractions.Models;
using DickinsonBros.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RollerCoaster.Account.API.Proxy;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.Proxy.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RollerCoaster.Account.Proxy.Tests
{
    [TestClass]
    public class AccountProxyServiceTests : BaseTest
    {

        const string BASE_URL = "https://localhost8080";
        const int HTTP_CLIENT_TIMEOUT_IN_SECONDS = 30;
        const string CREATE_ACCOUNT_PROXY_OPTION_RESOURCE = "SampleCreateAccountResource";
        const int CREATE_ACCOUNT_PROXY_OPTION_RETRYS = 2;
        const double CREATE_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS = 30;

        public int HttpClientTimeoutInSeconds { get; set; }
        public ProxyOption CreateAccount { get; set; }

        #region CreateAccountAsync

        [TestMethod]
        public async Task CreateAccountAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var createAccountRequest = new CreateAccountRequest();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{CREATE_ACCOUNT_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(createAccountRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<CreateAccountResponse>
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .Callback((HttpClient httpClient, HttpRequestMessage httpRequestMessage, int retrys, double timeoutInSeconds) =>
                        {
                            observedHttpClient = httpClient;
                            observedHttpRequestMessage = httpRequestMessage;
                            observedRetrys = retrys;
                            observedTimeoutInSeconds = timeoutInSeconds;
                        });

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    await uut.CreateAccountAsync(createAccountRequest);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<CreateAccountResponse>
                        (
                            It.IsAny<HttpClient>(),
                            It.IsAny<HttpRequestMessage>(),
                            It.IsAny<int>(),
                            It.IsAny<double>()
                        ),
                        Times.Once
                    );

                    Assert.AreEqual(uutConcrete._httpClient, observedHttpClient);
                    Assert.AreEqual(expectedMethod, observedHttpRequestMessage.Method);
                    Assert.AreEqual(expectedRequestUri.OriginalString, observedHttpRequestMessage.RequestUri.OriginalString);
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateAccount.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateAccount.TimeoutInSeconds, (double)observedTimeoutInSeconds);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task CreateAccountAsync_Runs_ReturnsHttpResponse()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var createAccountRequest = new CreateAccountRequest();
                    var httpResponse = new HttpResponse<CreateAccountResponse>();

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{CREATE_ACCOUNT_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(createAccountRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<CreateAccountResponse>
                            (
                                It.IsAny<HttpClient>(),
                                It.IsAny<HttpRequestMessage>(),
                                It.IsAny<int>(),
                                It.IsAny<double>()
                            )
                        )
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.CreateAccountAsync(createAccountRequest);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<CreateAccountResponse>
                        (
                            It.IsAny<HttpClient>(),
                            It.IsAny<HttpRequestMessage>(),
                            It.IsAny<int>(),
                            It.IsAny<double>()
                        ),
                        Times.Once
                    );
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(observed, httpResponse);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region Helpers

        private IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAccountProxyService, AccountProxyService>();
            serviceCollection.AddSingleton(Mock.Of<IDurableRestService>());
            serviceCollection.AddSingleton(Mock.Of<HttpClient>());
            serviceCollection.AddOptions<AccountProxyOptions>()
                .Configure((accountProxyOptions) =>
                {

                    accountProxyOptions.BaseURL = BASE_URL;
                    accountProxyOptions.HttpClientTimeoutInSeconds = HTTP_CLIENT_TIMEOUT_IN_SECONDS;
                    accountProxyOptions.CreateAccount = new ProxyOption
                    {
                        Resource = CREATE_ACCOUNT_PROXY_OPTION_RESOURCE,
                        Retrys = CREATE_ACCOUNT_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = CREATE_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                });


            return serviceCollection;
        }
        #endregion
    }
}
