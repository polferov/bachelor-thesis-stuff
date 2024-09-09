using System.Diagnostics.Metrics;

namespace SampleApp;

public class Heartbeat : BackgroundService
{
    private readonly IMeterFactory _mf;

    public Heartbeat(IMeterFactory mf)
    {
        _mf = mf;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var meter = _mf.Create("sampleapp_heartbeat");
            var counter = meter.CreateCounter<long>("heartbeat");
            counter.Add(1);
            await Task.Delay(1000, stoppingToken);
        }
    }
}