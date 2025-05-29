namespace FCG.Infrastructure.Cache;

public interface ICacheService
{
    object? Get(string key);
    void Remove(string key);
    void Set(string key, object content);
}
