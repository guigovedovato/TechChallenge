using FCG.Infrastructure.CorrelationId;

namespace FCG.Infrastructure.Log;

public class BaseLogger(Serilog.ILogger logger, ICorrelationIdGenerator correlationId)
{
    protected readonly Serilog.ILogger _logger = logger;
    protected readonly ICorrelationIdGenerator _correlationId = correlationId;

    public virtual void LogInformation(string message)
    {
        _logger.Information($"[CorrelationId: {_correlationId.Get()}] {message}");
    }

    public virtual void LogError(string message)
    {
        _logger.Error($"[CorrelationId: {_correlationId.Get()}] {message}");
    }

    public virtual void LogWarning(string message)
    {
        _logger.Warning($"[CorrelationId: {_correlationId.Get()}] {message}");
    }
}