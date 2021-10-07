using MassTransit;
using MassTransit.Registration;

namespace Consumer;

public class Worker : BackgroundService
{
    readonly IBusDepot _bus;

    public Worker(IBusDepot bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (File.Exists("D:\\start"))
            {
                await _bus.Start(stoppingToken);
                File.Delete("D:\\start");
            }
            else if(File.Exists("D:\\stop"))
            {
                await _bus.Stop(stoppingToken);
                File.Delete("D:\\stop");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}

