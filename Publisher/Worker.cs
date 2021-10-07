using MassTransit;
using Samples.Shared;

namespace publish
{

    public class Worker
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish(int numberOfMessages, CancellationToken stoppingToken)
        {
            for (int i = 0; i < numberOfMessages; i++)
            {
                await _bus.Publish<ISampleEvent>(new { Message = i.ToString() });
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}



