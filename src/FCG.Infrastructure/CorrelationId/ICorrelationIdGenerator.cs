namespace FCG.Infrastructure.CorrelationId;

public interface ICorrelationIdGenerator
{
    public string Get();

    public void Set(string correlationId);
}