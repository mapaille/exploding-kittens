using Microsoft.ApplicationInsights;

namespace Mapaille.ExplodingKittens.WebApp;

public class TelemetryClientFlusherHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ILogger<TelemetryClientFlusherHostedService> _logger;
    private readonly TelemetryClient _telemetryClient;

    public TelemetryClientFlusherHostedService(
        IHostApplicationLifetime appLifetime,
        ILogger<TelemetryClientFlusherHostedService> logger,
        TelemetryClient telemetryClient)
    {
        _appLifetime = appLifetime;
        _logger = logger;
        _telemetryClient = telemetryClient;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TelemetryClientFlusherHostedService starting");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TelemetryClientFlusherHostedService stopping");

        _appLifetime.ApplicationStopped.Register(async () =>
        {
            _logger.LogInformation("Application stopped. Flushing telemetry client...");
            await FlushTelemetryClientAsync(cancellationToken);
        });

        return Task.CompletedTask;
    }

    private async Task FlushTelemetryClientAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            await _telemetryClient.FlushAsync(cts.Token);
            _logger.LogInformation("Telemetry client flushed successfully");
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Telemetry client flush operation timed out");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while flushing telemetry client");
        }
    }
}
