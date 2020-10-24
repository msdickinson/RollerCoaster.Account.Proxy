using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.Proxy.Configurators;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.API.Proxy.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountProxyService(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IConfigureOptions<AccountProxyOptions>, AccountProxyOptionsConfigurator>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var accountProxyOptions = serviceProvider.GetRequiredService<IOptions<AccountProxyOptions>>();

            serviceCollection.AddHttpClient<IAccountProxyService, AccountProxyService>(client =>
            {
                client.BaseAddress = new Uri(accountProxyOptions.Value.BaseURL);
                client.Timeout = new TimeSpan(0, 0, accountProxyOptions.Value.HttpClientTimeoutInSeconds);
            });

            return serviceCollection;
        }
    }
}
