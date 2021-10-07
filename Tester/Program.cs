
using MassTransit.Registration;
using publish;

namespace Tester
{
    public class test
    {
        public static async Task Main()
        {
            Microsoft.Extensions.Hosting.IHost publishHost = publish.GS1.CreateHostBuilder()
                .Build();
            await publishHost.StartAsync();

            Microsoft.Extensions.Hosting.IHost consumeHost = Consumer.GS1.CreateHostBuilder()
                .Build();
            await consumeHost.StartAsync();

            var depot = consumeHost.Services.GetService(typeof(IBusDepot)) as IBusDepot;
            await depot!.Start(CancellationToken.None);

            var worker = publishHost.Services.GetService(typeof(Worker)) as Worker;
            await worker!.Publish(200, CancellationToken.None);

            await Task.Delay(15000);

            await depot!.Stop(CancellationToken.None);

            Console.WriteLine("--- restarting the depot ---");

            await depot!.Start(CancellationToken.None);

            await Task.Delay(15000);

            Console.WriteLine("--- 15 seconds elapsed ---");

            await depot!.Stop(CancellationToken.None);

            Console.ReadKey();
        }
    }
}