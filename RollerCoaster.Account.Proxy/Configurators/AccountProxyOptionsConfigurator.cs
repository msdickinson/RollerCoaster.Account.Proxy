using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RollerCoaster.Account.API.Proxy.Models;

namespace RollerCoaster.Account.Proxy.Configurators
{
    public class AccountProxyOptionsConfigurator : IConfigureOptions<AccountProxyOptions>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public AccountProxyOptionsConfigurator(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        void IConfigureOptions<AccountProxyOptions>.Configure(AccountProxyOptions options)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var configuration = provider.GetRequiredService<IConfiguration>();
                var accountProxyOptions = configuration.GetSection(nameof(AccountProxyOptions)).Get<AccountProxyOptions>();
                configuration.Bind($"{nameof(AccountProxyOptions)}", options);
            }
        }
    }
}
