using DickinsonBros.DurableRest.Abstractions;
using DickinsonBros.DurableRest.Abstractions.Models;
using DickinsonBros.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RollerCoaster.Account.API.Proxy;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.Proxy.Models;
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
        const int CREATE_USER_ACCOUNT_PROXY_OPTION_RETRYS = 1;
        const double CREATE_USER_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS = 1;

        //CreateAdminProxyOptions
        const string CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RESOURCE = "SampleCreateAdminAccountResource";
        const int CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RETRYS = 2;
        const double CREATE_ADMIN_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS = 2;

        //LogProxyOptions
        const string LOG_PROXY_OPTION_RESOURCE = "SampleLogResource";
        const int LOG_PROXY_OPTION_RETRYS = 3;
        const double LOG_PROXY_OPTION_TIMEOUT_IN_SECONDS = 3;

        //LoginProxyOptions
        const string LOGIN_PROXY_OPTION_RESOURCE = "SampleLoginResource";
        const int LOGIN_PROXY_OPTION_RETRYS = 4;
        const double LOGIN_PROXY_OPTION_TIMEOUT_IN_SECONDS = 4;

        //UserAuthorizedOptions
        const string USER_AUTHORIZED_PROXY_OPTION_RESOURCE = "SampleUserAuthorizedResource";
        const int USER_AUTHORIZED_PROXY_OPTION_RETRYS = 5;
        const double USER_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS = 5;

        //AdminAuthorizedOptions
        const string ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE = "SampleAdminAuthorizedResource";
        const int ADMIN_AUTHORIZED_PROXY_OPTION_RETRYS = 6;
        const double ADMIN_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS = 6;

        //RefreshTokensOptions
        const string REFRESH_TOKENS_PROXY_OPTION_RESOURCE = "RefreshTokensResource";
        const int REFRESH_TOKENS_PROXY_OPTION_RETRYS = 7;
        const double REFRESH_TOKENS_PROXY_OPTION_TIMEOUT_IN_SECONDS = 7;

        //UpdateEmailPreferenceOptions
        const string UPDATE_EMAIL_PREFERENCE_OPTION_RESOURCE = "UpdateEmailPreferenceResource";
        const int UPDATE_EMAIL_PREFERENCE_PROXY_OPTION_RETRYS = 8;
        const double UPDATE_EMAIL_PREFERENCE_PROXY_OPTION_TIMEOUT_IN_SECONDS = 8;

        //UpdateEmailPreferenceWithTokenOptions
        const string UPDATE_EMAIL_PREFERENCE_WITH_TOKEN_OPTION_RESOURCE = "UpdateEmailPreferenceithTokenResource";
        const int UPDATE_EMAIL_PREFERENCEE_WITH_TOKEN_PROXY_OPTION_RETRYS = 9;
        const double UPDATE_EMAIL_PREFERENCEE_WITH_TOKEN_PROXY_OPTION_TIMEOUT_IN_SECONDS = 9;

        //ActivateEmailOptions
        const string ACTIVATE_EMAIL_OPTION_RESOURCE = "ActivateEmailResource";
        const int ACTIVATE_EMAIL_PROXY_OPTION_RETRYS = 10;
        const double ACTIVATE_EMAIL_PROXY_OPTION_TIMEOUT_IN_SECONDS = 10;

        //UpdatePasswordOptions
        const string UPDATE_PASSWORD_OPTION_RESOURCE = "UpdatePasswordResource";
        const int UPDATE_PASSWORD_PROXY_OPTION_RETRYS = 10;
        const double UPDATE_PASSWORD_PROXY_OPTION_TIMEOUT_IN_SECONDS = 10;

        //ResetPasswordOptions
        const string RESET_PASSWORD_OPTION_RESOURCE = "ResetPasswordResource";
        const int RESET_PASSWORD_PROXY_OPTION_RETRYS = 11;
        const double RESET_PASSWORD_PROXY_OPTION_TIMEOUT_IN_SECONDS = 11;

        //RequestPasswordResetEmailOptions
        const string REQUEST_PASSWORD_RESET_EMAIL_OPTION_RESOURCE = "RequestPasswordResetEmailResource";
        const int REQUEST_PASSWORD_RESET_EMAIL_PROXY_OPTION_RETRYS = 12;
        const double REQUEST_PASSWORD_RESET_EMAIL_PROXY_OPTION_TIMEOUT_IN_SECONDS = 12;

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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.Log.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.Log.TimeoutInSeconds, (double)observedTimeoutInSeconds);
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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.Login.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.Login.TimeoutInSeconds, (double)observedTimeoutInSeconds);
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

                    var expectedMethod = HttpMethod.Get;
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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UserAuthorized.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UserAuthorized.TimeoutInSeconds, (double)observedTimeoutInSeconds);

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

                    var expectedMethod = HttpMethod.Get;
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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.AdminAuthorized.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.AdminAuthorized.TimeoutInSeconds, (double)observedTimeoutInSeconds);

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

        #region RefreshTokensAsync

        [TestMethod]
        public async Task RefreshTokensAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var refreshTokensRequest = new RefreshTokensRequest();
                    var httpResponse = new HttpResponse<RefreshTokensResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{REFRESH_TOKENS_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(refreshTokensRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<RefreshTokensResponse>
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
                    var observed = await uut.RefreshTokensAsync(refreshTokensRequest);

                    //Assert
                    durableRestServiceMock
                    .Verify(
                        durableRestService => durableRestService.ExecuteAsync<RefreshTokensResponse>
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
                    Assert.AreEqual(uutConcrete._accountProxyOptions.RefreshTokens.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.RefreshTokens.TimeoutInSeconds, (double)observedTimeoutInSeconds);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task RefreshTokensAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var refreshTokensRequest = new RefreshTokensRequest();
                    var httpResponse = new HttpResponse<RefreshTokensResponse>();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{REFRESH_TOKENS_PROXY_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(refreshTokensRequest), Encoding.UTF8, "application/json");


                    var durableRestServiceMock = serviceProvider.GetMock<IDurableRestService>();
                    durableRestServiceMock
                        .Setup
                        (
                            durableRestService => durableRestService.ExecuteAsync<RefreshTokensResponse>
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
                    var observed = await uut.RefreshTokensAsync(refreshTokensRequest);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);
                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        #endregion

        #region UpdateEmailPreferenceAsync

        [TestMethod]
        public async Task UpdateEmailPreferenceAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var updateEmailPreferenceRequest = new UpdateEmailPreferenceRequest();
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{UPDATE_EMAIL_PREFERENCE_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(updateEmailPreferenceRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.UpdateEmailPreferenceAsync(updateEmailPreferenceRequest, bearerToken);

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
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UpdateEmailPreference.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UpdateEmailPreference.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task UpdateEmailPreferenceAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
             (
                 async (serviceProvider) =>
                 {
                    //Setup
                    var updateEmailPreferenceRequest = new UpdateEmailPreferenceRequest();
                     var bearerToken = "SampleBearerToken";
                     var httpResponse = new HttpResponseMessage();

                     var observedHttpClient = (HttpClient)null;
                     var observedHttpRequestMessage = (HttpRequestMessage)null;
                     var observedRetrys = (int?)null;
                     var observedTimeoutInSeconds = (double?)null;

                     var expectedMethod = HttpMethod.Post;
                     var expectedRequestUri = new Uri($"{UPDATE_EMAIL_PREFERENCE_OPTION_RESOURCE}", UriKind.Relative);
                     var expectedContent = new StringContent(JsonSerializer.Serialize(updateEmailPreferenceRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.UpdateEmailPreferenceAsync(updateEmailPreferenceRequest, bearerToken);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

                 },
                serviceCollection => ConfigureServices(serviceCollection)
            );
        }

        #endregion

        #region UpdateEmailPreferenceWithTokenAsync

        [TestMethod]
        public async Task UpdateEmailPreferenceWithTokenAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var updateEmailPreferenceWithTokenRequest = new UpdateEmailPreferenceWithTokenRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{UPDATE_EMAIL_PREFERENCE_WITH_TOKEN_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(updateEmailPreferenceWithTokenRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.UpdateEmailPreferenceWithTokenAsync(updateEmailPreferenceWithTokenRequest);

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
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UpdateEmailPreferenceWithToken.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UpdateEmailPreferenceWithToken.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task UpdateEmailPreferenceWithTokenAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var updateEmailPreferenceWithTokenRequest = new UpdateEmailPreferenceWithTokenRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{UPDATE_EMAIL_PREFERENCE_WITH_TOKEN_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(updateEmailPreferenceWithTokenRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.UpdateEmailPreferenceWithTokenAsync(updateEmailPreferenceWithTokenRequest);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

                 },
                serviceCollection => ConfigureServices(serviceCollection)
            );
        }

        #endregion

        #region ActivateEmailAsync

        [TestMethod]
        public async Task ActivateEmailAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var activateEmailRequest = new ActivateEmailRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{ACTIVATE_EMAIL_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(activateEmailRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.ActivateEmailAsync(activateEmailRequest);

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
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.ActivateEmail.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.ActivateEmail.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task ActivateEmailAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var activateEmailRequest = new ActivateEmailRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{ACTIVATE_EMAIL_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(activateEmailRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.ActivateEmailAsync(activateEmailRequest);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

                },
                serviceCollection => ConfigureServices(serviceCollection)
            );
        }

        #endregion

        #region UpdatePasswordPreferenceAsync

        [TestMethod]
        public async Task UpdatePasswordAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var updatePasswordRequest = new UpdatePasswordRequest();
                    var bearerToken = "SampleBearerToken";
                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{UPDATE_PASSWORD_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(updatePasswordRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.UpdatePasswordAsync(updatePasswordRequest, bearerToken);

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
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UpdatePassword.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.UpdatePassword.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task UpdatePasswordAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
             (
                 async (serviceProvider) =>
                 {
                     //Setup
                     var updatePasswordRequest = new UpdatePasswordRequest();
                     var bearerToken = "SampleBearerToken";
                     var httpResponse = new HttpResponseMessage();

                     var observedHttpClient = (HttpClient)null;
                     var observedHttpRequestMessage = (HttpRequestMessage)null;
                     var observedRetrys = (int?)null;
                     var observedTimeoutInSeconds = (double?)null;

                     var expectedMethod = HttpMethod.Post;
                     var expectedRequestUri = new Uri($"{UPDATE_PASSWORD_OPTION_RESOURCE}", UriKind.Relative);
                     var expectedContent = new StringContent(JsonSerializer.Serialize(updatePasswordRequest), Encoding.UTF8, "application/json");

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
                     var observed = await uut.UpdatePasswordAsync(updatePasswordRequest, bearerToken);

                     //Assert
                     Assert.IsNotNull(observed);
                     Assert.AreEqual(httpResponse, observed);

                 },
                serviceCollection => ConfigureServices(serviceCollection)
            );
        }

        #endregion

        #region ResetPasswordAsync

        [TestMethod]
        public async Task ResetPasswordAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var resetPasswordRequest = new ResetPasswordRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{RESET_PASSWORD_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(resetPasswordRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.ResetPasswordAsync(resetPasswordRequest);

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
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.ResetPassword.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.ResetPassword.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task ResetPasswordAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var resetPasswordRequest = new ResetPasswordRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{RESET_PASSWORD_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(resetPasswordRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.ResetPasswordAsync(resetPasswordRequest);

                    //Assert
                    Assert.IsNotNull(observed);
                    Assert.AreEqual(httpResponse, observed);

                },
                serviceCollection => ConfigureServices(serviceCollection)
            );
        }

        #endregion

        #region ResetPasswordAsync

        [TestMethod]
        public async Task RequestPasswordResetEmailAsync_Runs_durableRestServiceExecuteAsyncCalled()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var requestPasswordResetEmailRequest = new RequestPasswordResetEmailRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{REQUEST_PASSWORD_RESET_EMAIL_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(requestPasswordResetEmailRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.RequestPasswordResetEmailAsync(requestPasswordResetEmailRequest);

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
                    Assert.AreEqual(expectedContent.ToString(), observedHttpRequestMessage.Content.ToString());
                    Assert.IsNotNull(observedRetrys);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.RequestPasswordResetEmail.Retrys, (int)observedRetrys);
                    Assert.IsNotNull(observedTimeoutInSeconds);
                    Assert.AreEqual(uutConcrete._accountProxyOptions.RequestPasswordResetEmail.TimeoutInSeconds, (double)observedTimeoutInSeconds);

                },
               serviceCollection => ConfigureServices(serviceCollection)
           );
        }

        [TestMethod]
        public async Task RequestPasswordResetEmailAsync_Runs_ReturnsHttpResponseMessage()
        {
            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    var requestPasswordResetEmailRequest = new RequestPasswordResetEmailRequest();

                    var httpResponse = new HttpResponseMessage();

                    var observedHttpClient = (HttpClient)null;
                    var observedHttpRequestMessage = (HttpRequestMessage)null;
                    var observedRetrys = (int?)null;
                    var observedTimeoutInSeconds = (double?)null;

                    var expectedMethod = HttpMethod.Post;
                    var expectedRequestUri = new Uri($"{REQUEST_PASSWORD_RESET_EMAIL_OPTION_RESOURCE}", UriKind.Relative);
                    var expectedContent = new StringContent(JsonSerializer.Serialize(requestPasswordResetEmailRequest), Encoding.UTF8, "application/json");

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
                    var observed = await uut.RequestPasswordResetEmailAsync(requestPasswordResetEmailRequest);

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
                    accountProxyOptions.CreateUserAccount = new ProxyOptions
                    {
                        Resource = CREATE_USER_ACCOUNT_PROXY_OPTION_RESOURCE,
                        Retrys = CREATE_USER_ACCOUNT_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = CREATE_USER_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.CreateAdminAccount = new ProxyOptions
                    {
                        Resource = CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RESOURCE,
                        Retrys = CREATE_ADMIN_ACCOUNT_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = CREATE_ADMIN_ACCOUNT_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.Log = new ProxyOptions
                    {
                        Resource = LOG_PROXY_OPTION_RESOURCE,
                        Retrys = LOG_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = LOG_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.Login = new ProxyOptions
                    {
                        Resource = LOGIN_PROXY_OPTION_RESOURCE,
                        Retrys = LOGIN_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = LOGIN_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.UserAuthorized = new ProxyOptions
                    {
                        Resource = USER_AUTHORIZED_PROXY_OPTION_RESOURCE,
                        Retrys = USER_AUTHORIZED_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = USER_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.AdminAuthorized = new ProxyOptions
                    {
                        Resource = ADMIN_AUTHORIZED_PROXY_OPTION_RESOURCE,
                        Retrys = ADMIN_AUTHORIZED_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = ADMIN_AUTHORIZED_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.RefreshTokens = new ProxyOptions
                    {
                        Resource = REFRESH_TOKENS_PROXY_OPTION_RESOURCE,
                        Retrys = REFRESH_TOKENS_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = REFRESH_TOKENS_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.UpdateEmailPreference = new ProxyOptions
                    {
                        Resource = UPDATE_EMAIL_PREFERENCE_OPTION_RESOURCE,
                        Retrys = UPDATE_EMAIL_PREFERENCE_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = UPDATE_EMAIL_PREFERENCE_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.UpdateEmailPreferenceWithToken = new ProxyOptions
                    {
                        Resource = UPDATE_EMAIL_PREFERENCE_WITH_TOKEN_OPTION_RESOURCE,
                        Retrys = UPDATE_EMAIL_PREFERENCEE_WITH_TOKEN_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = UPDATE_EMAIL_PREFERENCEE_WITH_TOKEN_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.ActivateEmail = new ProxyOptions
                    {
                        Resource = ACTIVATE_EMAIL_OPTION_RESOURCE,
                        Retrys = ACTIVATE_EMAIL_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = ACTIVATE_EMAIL_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.UpdatePassword = new ProxyOptions
                    {
                        Resource = UPDATE_PASSWORD_OPTION_RESOURCE,
                        Retrys = UPDATE_PASSWORD_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = UPDATE_PASSWORD_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.ResetPassword = new ProxyOptions
                    {
                        Resource = RESET_PASSWORD_OPTION_RESOURCE,
                        Retrys = RESET_PASSWORD_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = RESET_PASSWORD_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                    accountProxyOptions.RequestPasswordResetEmail = new ProxyOptions
                    {
                        Resource = REQUEST_PASSWORD_RESET_EMAIL_OPTION_RESOURCE,
                        Retrys = REQUEST_PASSWORD_RESET_EMAIL_PROXY_OPTION_RETRYS,
                        TimeoutInSeconds = REQUEST_PASSWORD_RESET_EMAIL_PROXY_OPTION_TIMEOUT_IN_SECONDS
                    };
                });


            return serviceCollection;
        }
        #endregion
    }
}
