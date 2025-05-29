using MessagePack;

namespace FCG.Domain.Common.Response;

[MessagePackObject]
public class ApiResponse<T>(T? data, string message)
{
    [Key(0)]
    public T? Data { get; set; } = data;

    [Key(1)]
    public string Message { get; set; } = message;
}
