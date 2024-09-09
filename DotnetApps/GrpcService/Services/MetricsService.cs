using System.Text.Json;
using Grpc.Core;
using OpenTelemetry.Proto.Collector.Metrics.V1;

namespace GrpcService.Services;

public class MetricsService : OpenTelemetry.Proto.Collector.Metrics.V1.MetricsService.MetricsServiceBase
{
    private static bool _isFirst = true;

    public override async Task<ExportMetricsServiceResponse> Export(ExportMetricsServiceRequest request,
        ServerCallContext context)
    {
        if (!_isFirst) return new ExportMetricsServiceResponse();

        var json = JsonSerializer.Serialize(request, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        Console.WriteLine(json);
        _isFirst = false;

        return new ExportMetricsServiceResponse();
    }
}