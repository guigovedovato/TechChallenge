namespace FCG.Infrastructure.CorrelationId;

public class CorrelationIdGenerator : ICorrelationIdGenerator
{
    private static string _correlationId = string.Empty;

    public string Get() => _correlationId;

    public void Set(string correlationId) => _correlationId = correlationId;
}