using Domain.Common;
using Grpc.Core;

namespace Grpc.Extensions;

public static class StatusExtension
{ 
    public static Status GetStatus(this Error error) => error.Status switch
    {
        ResultErrorStatus.NotFound => new(StatusCode.NotFound, error.Message),
        ResultErrorStatus.InvalidArgument => new(StatusCode.InvalidArgument, error.Message),
        ResultErrorStatus.BadRequest => new(StatusCode.Cancelled, error.Message),
        _ => new Status(StatusCode.OK, string.Empty)
    };
}
