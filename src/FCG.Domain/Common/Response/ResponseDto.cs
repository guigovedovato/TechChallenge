namespace FCG.Domain.Common.Response;

public class ResponseDto<T>(T? data, string message)
{
    public T? Data { get; set; } = data;

    public string Message { get; set; } = message;
}
