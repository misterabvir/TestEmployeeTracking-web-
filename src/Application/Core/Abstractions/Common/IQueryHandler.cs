using MediatR;

namespace ApplicationCore.Abstractions.Common;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{

}
