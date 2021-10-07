using GreenPipes;
using MassTransit;
using MassTransit.ActiveMqTransport;
using MassTransit.AspNetCoreIntegration.HealthChecks;
using MassTransit.Registration;
using MassTransit.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace publish
{
    public class GS1
    {
        public static async Task Main()
        {
            Microsoft.Extensions.Hosting.IHost host = CreateHostBuilder()
                .Build();

            await host.RunAsync();
        }


        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.UsingActiveMq((context, cfg) =>
                        {
                            cfg.Host("127.0.0.1", 61616, h =>
                            {
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });

                    services.AddMassTransitHostedService();
                    services.AddTransient<Worker>();
                });
    }
}