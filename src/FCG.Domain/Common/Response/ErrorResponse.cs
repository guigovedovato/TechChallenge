using MessagePack;

namespace FCG.Domain.Common.Response;

[MessagePackObject]
public record ErrorResponse
{
    [Key(0)]
    public required string ErrorId { get; set; }

    [Key(1)]
    public int StatusCode { get; set; }

    [Key(2)]
    public required string Message { get; set; }
}
