namespace SampleApp;

public class LogsProducer : BackgroundService
{
    private readonly ILogger<LogsProducer> _logger;

    public LogsProducer(ILogger<LogsProducer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Hello, World!");
            await Task.Delay(1000, stoppingToken);
        }
    }
}