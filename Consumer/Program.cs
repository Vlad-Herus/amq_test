using GreenPipes;
using MassTransit;
using MassTransit.ActiveMqTransport;
using MassTransit.Registration;

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
                                e.UseMessageRetry(aa => aa.Interval(5, 1000));
                                e.ConcurrentMessageLimit = 3;
                                e.PrefetchCount = 10;
                                e.Consumer<MessageConsumer>();
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    
                    //services.AddMassTransitHostedService(true);

                    services.AddHostedService<Worker>();
                });
    }
}