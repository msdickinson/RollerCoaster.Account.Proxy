using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RollerCoaster.Account.API.Proxy.Extensions;
using System.Linq;
using RollerCoaster.Account.API.Proxy;

namespace RollerCoaster.Account.Proxy.Tests.Extensions
{
    [TestClass]
    public class IServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void AddDateTimeService_Should_Succeed()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var baseAddress = new Uri("https://Localhost:8080");
            var httpClientTimeout = new TimeSpan(0, 0, 30);

            // Act
            serviceCollection.AddAccountProxyService(baseAddress, httpClientTimeout);


            // Assert
            Assert.IsTrue(serviceCollection.Any(serviceDefinition => serviceDefinition.ServiceType == typeof(IAccountProxyService) &&
                                                       serviceDefinition.ImplementationFactory != null &&
                                                       serviceDefinition.Lifetime == ServiceLifetime.Transient));
        }
    }
}
