using DickinsonBros.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.Proxy.Configurators;
using System.Threading.Tasks;

namespace RollerCoaster.Account.Proxy.Tests.Configurators
{
    [TestClass]
    public class AccountProxyOptionsConfiguratorTests : BaseTest
    {
        [TestMethod]
        public async Task Configure_Runs_ConfigReturns()
        {
            var accountProxyOptions = new AccountProxyOptions
            {
                ActivateEmail = new Models.ProxyOptions
                {
                    Resource = "SampleActivateEmailResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                AdminAuthorized = new Models.ProxyOptions
                {
                    Resource = "SampleAdminAuthorizedResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                CreateAdminAccount = new Models.ProxyOptions
                {
                    Resource = "SampleCreateAdminAccountResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                CreateUserAccount = new Models.ProxyOptions
                {
                    Resource = "SampleCreateUserAccountResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                UserAuthorized = new Models.ProxyOptions
                {
                    Resource = "SampleUserAuthorizedResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                BaseURL = "SampleBaseURL",
                HttpClientTimeoutInSeconds = 1,
                Log = new Models.ProxyOptions
                {
                    Resource = "SampleLogResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                Login = new Models.ProxyOptions
                {
                    Resource = "SampleLoginResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                RefreshTokens = new Models.ProxyOptions
                {
                    Resource = "SampleRefreshTokensResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                RequestPasswordResetEmail = new Models.ProxyOptions
                {
                    Resource = "SampleRequestPasswordResetEmailResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                ResetPassword = new Models.ProxyOptions
                {
                    Resource = "SampleRResetPasswordResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                UpdateEmailPreference = new Models.ProxyOptions
                {
                    Resource = "SampleUpdateEmailPreferenceResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                UpdateEmailPreferenceWithToken = new Models.ProxyOptions
                {
                    Resource = "SampleUpdateEmailPreferenceWithTokenResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
                UpdatePassword = new Models.ProxyOptions
                {
                    Resource = "SampleUpdatePasswordResource",
                    Retrys = 1,
                    TimeoutInSeconds = 2
                },
            };

            var configurationRoot = BuildConfigurationRoot(accountProxyOptions);

            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup
                    //Act
                    var options = serviceProvider.GetRequiredService<IOptions<AccountProxyOptions>>().Value;

                    //Assert
                    Assert.IsNotNull(options);

                    Assert.AreEqual(accountProxyOptions.ActivateEmail.Resource                              , options.ActivateEmail.Resource);
                    Assert.AreEqual(accountProxyOptions.ActivateEmail.Retrys                                , options.ActivateEmail.Retrys);
                    Assert.AreEqual(accountProxyOptions.ActivateEmail.TimeoutInSeconds                      , options.ActivateEmail.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.AdminAuthorized.Resource                            , options.AdminAuthorized.Resource);
                    Assert.AreEqual(accountProxyOptions.AdminAuthorized.Retrys                              , options.AdminAuthorized.Retrys);
                    Assert.AreEqual(accountProxyOptions.AdminAuthorized.TimeoutInSeconds                    , options.AdminAuthorized.TimeoutInSeconds);
                        
                    Assert.AreEqual(accountProxyOptions.BaseURL                                             , options.BaseURL);

                    Assert.AreEqual(accountProxyOptions.CreateAdminAccount.Resource                         , options.CreateAdminAccount.Resource);
                    Assert.AreEqual(accountProxyOptions.CreateAdminAccount.Retrys                           , options.CreateAdminAccount.Retrys);
                    Assert.AreEqual(accountProxyOptions.CreateAdminAccount.TimeoutInSeconds                 , options.CreateAdminAccount.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.CreateUserAccount.Resource                          , options.CreateUserAccount.Resource);
                    Assert.AreEqual(accountProxyOptions.CreateUserAccount.Retrys                            , options.CreateUserAccount.Retrys);
                    Assert.AreEqual(accountProxyOptions.CreateUserAccount.TimeoutInSeconds                  , options.CreateUserAccount.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.HttpClientTimeoutInSeconds                          , options.HttpClientTimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.Log.Resource                                        , options.Log.Resource);
                    Assert.AreEqual(accountProxyOptions.Log.Retrys                                          , options.Log.Retrys);
                    Assert.AreEqual(accountProxyOptions.Log.TimeoutInSeconds                                , options.Log.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.Login.Resource                                      , options.Login.Resource);
                    Assert.AreEqual(accountProxyOptions.Login.Retrys                                        , options.Login.Retrys);
                    Assert.AreEqual(accountProxyOptions.Login.TimeoutInSeconds                              , options.Login.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.RefreshTokens.Resource                              , options.RefreshTokens.Resource);
                    Assert.AreEqual(accountProxyOptions.RefreshTokens.Retrys                                , options.RefreshTokens.Retrys);
                    Assert.AreEqual(accountProxyOptions.RefreshTokens.TimeoutInSeconds                      , options.RefreshTokens.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.RequestPasswordResetEmail.Resource                  , options.RequestPasswordResetEmail.Resource);
                    Assert.AreEqual(accountProxyOptions.RequestPasswordResetEmail.Retrys                    , options.RequestPasswordResetEmail.Retrys);
                    Assert.AreEqual(accountProxyOptions.RequestPasswordResetEmail.TimeoutInSeconds          , options.RequestPasswordResetEmail.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.ResetPassword.Resource                              , options.ResetPassword.Resource);
                    Assert.AreEqual(accountProxyOptions.ResetPassword.Retrys                                , options.ResetPassword.Retrys);
                    Assert.AreEqual(accountProxyOptions.ResetPassword.TimeoutInSeconds                      , options.ResetPassword.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.UpdateEmailPreference.Resource                      , options.UpdateEmailPreference.Resource);
                    Assert.AreEqual(accountProxyOptions.UpdateEmailPreference.Retrys                        , options.UpdateEmailPreference.Retrys);
                    Assert.AreEqual(accountProxyOptions.UpdateEmailPreference.TimeoutInSeconds              , options.UpdateEmailPreference.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.UpdateEmailPreferenceWithToken.Resource             , options.UpdateEmailPreferenceWithToken.Resource);
                    Assert.AreEqual(accountProxyOptions.UpdateEmailPreferenceWithToken.Retrys               , options.UpdateEmailPreferenceWithToken.Retrys);
                    Assert.AreEqual(accountProxyOptions.UpdateEmailPreferenceWithToken.TimeoutInSeconds     , options.UpdateEmailPreferenceWithToken.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.UpdatePassword.Resource                             , options.UpdatePassword.Resource);
                    Assert.AreEqual(accountProxyOptions.UpdatePassword.Retrys                               , options.UpdatePassword.Retrys);
                    Assert.AreEqual(accountProxyOptions.UpdatePassword.TimeoutInSeconds                     , options.UpdatePassword.TimeoutInSeconds);

                    Assert.AreEqual(accountProxyOptions.UserAuthorized.Resource                             , options.UserAuthorized.Resource);
                    Assert.AreEqual(accountProxyOptions.UserAuthorized.Retrys                               , options.UserAuthorized.Retrys);
                    Assert.AreEqual(accountProxyOptions.UserAuthorized.TimeoutInSeconds                     , options.UserAuthorized.TimeoutInSeconds);

                    await Task.CompletedTask.ConfigureAwait(false);

                },
                serviceCollection => ConfigureServices(serviceCollection, configurationRoot)
            );
        }

        #region Helpers

        private IServiceCollection ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<IConfigureOptions<AccountProxyOptions>, AccountProxyOptionsConfigurator>();

            return serviceCollection;
        }

        #endregion
    }
}
