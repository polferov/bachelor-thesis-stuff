using System.Diagnostics.Metrics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// configure OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("sample_app"))
    .WithMetrics(m =>
        m.AddMeter("sampleapp_heartbeat")
            .AddOtlpExporter()
    )
    .WithTracing(t =>
        t.AddSource("sampleapp")
            .AddOtlpExporter()
    );

// export metrics every second
builder.Services.Configure<MetricReaderOptions>(opts =>
    opts.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 1000);

// add the heartbeat service
builder.Services.AddHostedService<Heartbeat>();

// add the trace producer service
builder.Services.AddHostedService<TraceProducer>();

var app = builder.Build();
app.Run();

public class Heartbeat(IMeterFactory mf) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // do metrics stuff
            using var meter = mf.Create("sampleapp_heartbeat");
            var counter = meter.CreateCounter<long>("heartbeat");
            counter.Add(1);
            await Task.Delay(1000, stoppingToken);
        }
    }
}

public class TraceProducer(TracerProvider tp) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // do trace stuff
            var tracer = tp.GetTracer("sampleapp");
            using var root = tracer.StartRootSpan("RootSpan");
            using var span = tracer.StartActiveSpan("DoTrace");
            span.AddEvent("Trace event 1");
            await Task.Delay(10, stoppingToken);
            span.AddEvent("Trace event 2");
            await Task.Delay(1000, stoppingToken);
        }
    }
}