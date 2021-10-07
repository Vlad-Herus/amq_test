using MassTransit;
using MassTransit.Registration;
using Microsoft.Extensions.Hosting;
using Samples.Shared;

namespace publish
{

    public class Worker : BackgroundService
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (File.Exists("D:\\push"))
                {
                    for (int i = 0; i < 200; i++)
                    {
                        await _bus.Publish<ISampleEvent>(new { Message = i.ToString() });
                    }
                    File.Delete("D:\\push");

                }

                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}

namespace Samples.Shared
{
    public interface ISampleEvent
    {
        string Message { get; }
    }

}

