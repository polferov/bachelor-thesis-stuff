using OpenTelemetry.Trace;

namespace SampleApp;

public class TraceProducer : BackgroundService
{
    private readonly TracerProvider _tp;

    public TraceProducer(TracerProvider tp)
    {
        _tp = tp;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoTrace();
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task DoTrace()
    {
        var tracer = _tp.GetTracer("sampleapp");
        using var root = tracer.StartRootSpan("RootSpan");
        using var span = tracer.StartActiveSpan("DoTrace");
        span.AddEvent("Trace event 1");
        await Task.Delay(10);
        span.AddEvent("Trace event 2");
    }
}