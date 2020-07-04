using DickinsonBros.DurableRest.Abstractions;
using DickinsonBros.DurableRest.Abstractions.Models;
using DickinsonBros.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RollerCoaster.Account.API.Proxy;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.Proxy.Models;
using RollerCoaster.Account.Proxy.Models.CreateAdminAccount;
using RollerCoaster.Account.Proxy.Models.CreateUserAccount;
using RollerCoaster.Account.Proxy.Models.Login;
using System;
using System.Linq;
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

        //CreateAccountProxyOptions
        const string CREATE_USER_ACCOUNT_PROXY_OPTION_RESOURCE = "SampleCreateUserAccountResource";
        const int CREATE_USER_ACCOUNT_PROXY_OPTION_RETRYS = 2;
        const double CREATE_USER_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS = 30;

        //CreateAdminProxyOptions
        const string CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RESOURCE = "SampleCreateAdminAccountResource";
        const int CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RETRYS = 2;
        const double CREATE_ADMIN_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS = 30;

        //LogProxyOptions
        const string LOG_PROXY_OPTION_RESOURCE = "SampleLogResource";
        const int LOG_PROXY_OPTION_RETRYS = 2;
        const double LOG_PROXY_OPTION_TIMEOUT_IN_SECONDS = 30;

        //LoginProxyOptions
        const string LOGIN_PROXY_OPTION_RESOURCE = "SampleLoginResource";
        const int LOGIN_PROXY_OPTION_RETRYS = 2;
        const double LOGIN_PROXY_OPTION_TIMEOUT_IN_SECONDS = 30;

        //UserAuthorizedOptions
        const string USER_AUTHORIZED_PROXY_OPTION_RESOURCE = "SampleUserAuthorizedResource";
        const int USER_AUTHORIZED_PROXY_OPTION_RETRYS = 2;
        const double USER_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS = 30;

        //AdminAuthorizedOptions
        const string ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE = "SampleAdminAuthorizedResource";
        const int ADMIN_AUTHORIZED_PROXY_OPTION_RETRYS = 2;
        const double ADMIN_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS = 30;

        #region CreateUserAccountAsync

        [TestMethod]
        public async Task CreateUserAccountAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var createUserAccountRequest = new CreateUserAccountRequest();
                    var httpResponse = new HttpResponse<CreateUserAccountResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{CREATE_USER_ACCOUNT_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(createUserAccountRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<CreateUserAccountResponse>
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.CreateUserAccountAsync(createUserAccountRequest);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<CreateUserAccountResponse>
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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.TimeoutInSeconds, (double)observedTimeoutInSeconds);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task CreateUserAccountAsync_Runs_ReturnsHttpResponse()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var createUserAccountRequest = new CreateUserAccountRequest();
                    var httpResponse = new HttpResponse<CreateUserAccountResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{CREATE_USER_ACCOUNT_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(createUserAccountRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<CreateUserAccountResponse>
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
                        })
                        .ReturnsAsync(httpResponse);
                    
                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.CreateUserAccountAsync(createUserAccountRequest);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region CreateAdminAccountAsync

        [TestMethod]
        public async Task CreateAdminAccountAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var createAdminAccountRequest = new CreateAdminAccountRequest();
                    var httpResponse = new HttpResponse<CreateAdminAccountResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(createAdminAccountRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<CreateAdminAccountResponse>
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.CreateAdminAccountAsync(createAdminAccountRequest);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<CreateAdminAccountResponse>
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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateAdminAccount.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateAdminAccount.TimeoutInSeconds, (double)observedTimeoutInSeconds);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task CreateAdminAccountAsync_Runs_ReturnsHttpResponse()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var createAdminAccountRequest = new CreateAdminAccountRequest();
                    var httpResponse = new HttpResponse<CreateAdminAccountResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(createAdminAccountRequest), Encoding.UTF8, "application/json");

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<CreateAdminAccountResponse>
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.CreateAdminAccountAsync(createAdminAccountRequest);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region LogAsync

        [TestMethod]
        public async Task LogAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{LOG_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.LogAsync();

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync
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
                    Assert.IsNull(observedHttpRequestMessage.Content);
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.TimeoutInSeconds, (double)observedTimeoutInSeconds);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task LogAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{LOG_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.LogAsync();

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region LoginAsync

        [TestMethod]
        public async Task LoginAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var loginRequest = new LoginRequest();
                    var httpResponse = new HttpResponse<LoginResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{LOGIN_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<LoginResponse>
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.LoginAsync(loginRequest);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<LoginResponse>
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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateAdminAccount.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateAdminAccount.TimeoutInSeconds, (double)observedTimeoutInSeconds);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task LoginAsync_Runs_ReturnsHttpResponse()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var loginRequest = new LoginRequest();
                    var httpResponse = new HttpResponse<LoginResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{LOGIN_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<LoginResponse>
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.LoginAsync(loginRequest);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region UserAuthorizedAsync

        [TestMethod]
        public async Task UserAuthorizedAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{USER_AUTHORIZED_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.UserAuthorizedAsync(bearerToken);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync
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
                    Assert.IsTrue(observedHttpRequestMessage.Headers.First(e=> e.Key == AccountProxyService.AUTHORIZATION).Value.First() == bearerToken);
                    Assert.AreEqual(expectedRequestUri.OriginalString, observedHttpRequestMessage.RequestUri.OriginalString);
                    Assert.IsNull(observedHttpRequestMessage.Content);
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task UserAuthorizedAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{USER_AUTHORIZED_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.UserAuthorizedAsync(bearerToken);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region AdminAuthorizedAsync

        [TestMethod]
        public async Task AdminAuthorizedAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.AdminAuthorizedAsync(bearerToken);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync
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
                    Assert.IsTrue(observedHttpRequestMessage.Headers.First(e => e.Key == AccountProxyService.AUTHORIZATION).Value.First() == bearerToken);
                    Assert.AreEqual(expectedRequestUri.OriginalString, observedHttpRequestMessage.RequestUri.OriginalString);
                    Assert.IsNull(observedHttpRequestMessage.Content);
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.CreateUserAccount.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task AdminAuthorizedAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE}", UriKind.Relative);

                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync
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
                        })
                        .ReturnsAsync(httpResponse);

                    var uut = serviceProvider.GetRequiredService<IAccountProxyService>();
                    var uutConcrete = (AccountProxyService)uut;

                    //Act
                    var observed = await uut.AdminAuthorizedAsync(bearerToken);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

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
                    accountProxyOptions.CreateUserAccount = new ProxyOption
                    {
                        Resource = CREATE_USER_ACCOUNT_PROXY_OPTION_RESOURCE,
                        Retrys = CREATE_USER_ACCOUNT_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = CREATE_USER_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.CreateAdminAccount = new ProxyOption
                    {
                        Resource = CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RESOURCE,
                        Retrys = CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = CREATE_ADMIN_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.Log = new ProxyOption
                    {
                        Resource = LOG_PROXY_OPTION_RESOURCE,
                        Retrys = LOG_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = LOG_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.Login = new ProxyOption
                    {
                        Resource = LOGIN_PROXY_OPTION_RESOURCE,
                        Retrys = LOGIN_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = LOGIN_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.UserAuthorized = new ProxyOption
                    {
                        Resource = USER_AUTHORIZED_PROXY_OPTION_RESOURCE,
                        Retrys = USER_AUTHORIZED_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = USER_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.AdminAuthorized = new ProxyOption
                    {
                        Resource = ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE,
                        Retrys = ADMIN_AUTHORIZED_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = ADMIN_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                });


            return serviceCollection;
        }
        #endregion
    }
}
