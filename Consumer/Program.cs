using GreenPipes;
using MassTransit;
using MassTransit.ActiveMqTransport;
using Serilog;
using Serilog.Events;

namespace Consumer
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
                .UseSerilog((host, log) =>
                {
                    log.MinimumLevel.Verbose();

                    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
                    log.WriteTo.Console();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.UsingActiveMq((context, cfg) =>
                        {

                            cfg.Host("127.0.0.1", 61616, h =>
                            {
                            });

                            cfg.ReceiveEndpoint("test_queue_sample", e =>
                            {
                                e.ConcurrentMessageLimit = 3;
                                e.PrefetchCount = 10;
                                e.Consumer<MessageConsumer>();
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}