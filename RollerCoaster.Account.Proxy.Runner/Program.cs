using DickinsonBros.DateTime.Extensions;
using DickinsonBros.DurableRest.Extensions;
using DickinsonBros.Encryption.Certificate.Extensions;
using DickinsonBros.Encryption.Certificate.Models;
using DickinsonBros.Guid.Abstractions;
using DickinsonBros.Guid.Extensions;
using DickinsonBros.Logger.Extensions;
using DickinsonBros.Redactor.Extensions;
using DickinsonBros.Redactor.Models;
using DickinsonBros.Stopwatch.Extensions;
using DickinsonBros.Telemetry.Abstractions;
using DickinsonBros.Telemetry.Extensions;
using DickinsonBros.Telemetry.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RollerCoaster.Account.API.Proxy.Extensions;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Account.API.Proxy.Runner.Models;
using RollerCoaster.Account.API.Proxy.Runner.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RollerCoaster.Account.API.Proxy.Runner
{
    class Program
    {
        IConfiguration _configuration;
        async static Task Main()
        {
            await new Program().DoMain();
        }
        async Task DoMain()
        {
            try
            {
                using var applicationLifetime = new ApplicationLifetime();
                var services = InitializeDependencyInjection();
                ConfigureServices(services, applicationLifetime);
                using var provider = services.BuildServiceProvider();
                var telemetryService = provider.GetRequiredService<ITelemetryService>();
                var guidService = provider.GetRequiredService<IGuidService>();

                {
                    var accountProxyService = provider.GetRequiredService<IAccountProxyService>();

                    //var restResponse = await accountProxyService.CreateAccountAsync(new CreateAccountRequest
                    //{
                    //    Email = $"{guidService.NewGuid().ToString()}@FakeMail.com",
                    //    Password = guidService.NewGuid().ToString(),
                    //    Username = guidService.NewGuid().ToString()
                    //});
                }

                {
                    var accountProxyService = provider.GetRequiredService<IAccountProxyService>();

                    //var restResponse = await accountProxyService.CreateAccountAsync(new CreateAccountRequest
                    //{
                    //    Email = $"{guidService.NewGuid().ToString()}@FakeMail.com",
                    //    Password = guidService.NewGuid().ToString(),
                    //    Username = guidService.NewGuid().ToString()
                    //});
                }
                await telemetryService.FlushAsync().ConfigureAwait(false);

                applicationLifetime.StopApplication();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("End...");
            }
        }

        private void ConfigureServices(IServiceCollection services, Services.ApplicationLifetime applicationLifetime)
        {
            services.AddOptions();
            services.AddLogging(cfg => cfg.AddConsole());

            //Add ApplicationLifetime
            services.AddSingleton<IApplicationLifetime>(applicationLifetime);

            //Add DateTime Service
            services.AddDateTimeService();

            //Add Stopwatch Service
            services.AddStopwatchService();

            //Add Guid Service
            services.AddGuidService();

            //Add Logging Service
            services.AddLoggingService();

            //Add Redactor Service
            services.AddRedactorService();
            services.Configure<RedactorServiceOptions>(_configuration.GetSection(nameof(RedactorServiceOptions)));

            //Add Certificate Encryption Service
            services.AddCertificateEncryptionService<CertificateEncryptionServiceOptions>();
            services.Configure<CertificateEncryptionServiceOptions<RunnerCertificateEncryptionServiceOptions>>(_configuration.GetSection(nameof(RunnerCertificateEncryptionServiceOptions)));

            services.AddCertificateEncryptionService<RunnerCertificateEncryptionServiceOptions>();
            services.Configure<CertificateEncryptionServiceOptions<RunnerCertificateEncryptionServiceOptions>>(_configuration.GetSection(nameof(RunnerCertificateEncryptionServiceOptions)));

            //Add Telemetry Service
            services.AddTelemetryService();
            services.AddSingleton<IConfigureOptions<TelemetryServiceOptions>, TelemetryServiceOptionsConfigurator>();

            //Add DurableRest Service
            services.AddDurableRestService();

            //Add Account Proxy Service
            services.AddAccountProxyService
            (
                new Uri(_configuration[$"{nameof(AccountProxyOptions)}:{nameof(AccountProxyOptions.BaseURL)}"]),
                new TimeSpan(0,0, Convert.ToInt32(_configuration[$"{nameof(AccountProxyOptions)}:{nameof(AccountProxyOptions.HttpClientTimeoutInSeconds)}"]))
            );
            services.Configure<AccountProxyOptions>(_configuration.GetSection(nameof(AccountProxyOptions)));
        }

        IServiceCollection InitializeDependencyInjection()
        {
            var aspnetCoreEnvironment = Environment.GetEnvironmentVariable("BUILD_CONFIGURATION");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{aspnetCoreEnvironment}.json", true);
            _configuration = builder.Build();
            var services = new ServiceCollection();
            services.AddSingleton(_configuration);
            return services;
        }
    }
}
