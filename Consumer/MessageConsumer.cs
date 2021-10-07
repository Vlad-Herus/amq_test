using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Samples.Shared;

namespace Consumer
{
    public class Message
    {
        public string Text { get; set; }
    }

    public class MessageConsumer :
        IConsumer<ISampleEvent>
    {
        public MessageConsumer()
        {
        }

        public async Task Consume(ConsumeContext<ISampleEvent> context)
        {

            Console.WriteLine($"Processing {context.Message.Message} ");
            await Task.Delay(5000);
            Console.WriteLine($"Processing {context.Message.Message} finished");
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
