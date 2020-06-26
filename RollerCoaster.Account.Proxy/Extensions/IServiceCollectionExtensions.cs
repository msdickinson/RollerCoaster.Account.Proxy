using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.API.Proxy.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountProxyService(this IServiceCollection serviceCollection, Uri baseAddress, TimeSpan httpClientTimeout)
        {
            serviceCollection.AddHttpClient<IAccountProxyService, AccountProxyService>(client =>
            {
                client.BaseAddress = baseAddress;
                client.Timeout = httpClientTimeout;
            });
            return serviceCollection;
        }
    }
}
