using Domain.Common;
using Grpc.Protos;

namespace Grpc.Extensions;

public static class SharedExtensions
{
    public static ErrorModel ToErrorModel(this Error error)
        => new()
        {
            Status = error.Status.ToString(),
            Title = error.Title,
            Description = error.Message
        };
}
