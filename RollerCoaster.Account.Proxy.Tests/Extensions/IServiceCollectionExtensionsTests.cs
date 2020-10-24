using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RollerCoaster.Account.API.Proxy.Extensions;
using System.Linq;
using RollerCoaster.Account.API.Proxy;
using Microsoft.Extensions.Options;
using RollerCoaster.Account.Proxy.Configurators;
using RollerCoaster.Account.API.Proxy.Models;
using DickinsonBros.Test;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RollerCoaster.Account.Proxy.Tests.Extensions
{
    [TestClass]
    public class IServiceCollectionExtensionsTests : BaseTest
    {

        [TestMethod]
        public void AddDateTimeService_Should_Succeed()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
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
            serviceCollection.AddSingleton<IConfiguration>(configurationRoot);

            // Act
            serviceCollection.AddAccountProxyService();


            // Assert
            Assert.IsTrue(serviceCollection.Any(serviceDefinition => serviceDefinition.ServiceType == typeof(IAccountProxyService) &&
                                                       serviceDefinition.ImplementationFactory != null &&
                                                       serviceDefinition.Lifetime == ServiceLifetime.Transient));

            Assert.IsTrue(serviceCollection.Any(serviceDefinition => serviceDefinition.ServiceType == typeof(IConfigureOptions<AccountProxyOptions>) &&
                               serviceDefinition.ImplementationType == typeof(AccountProxyOptionsConfigurator) &&
                               serviceDefinition.Lifetime == ServiceLifetime.Singleton));
        }
    }

    
}
