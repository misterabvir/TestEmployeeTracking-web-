using MediatR;

namespace ApplicationCore.Abstractions.Common;

/// <summary>
/// Wrapper for MediatR IRequestHandler<TRequest, TResponse> working with IQuery
/// </summary>
/// <typeparam name="TQuery">Query type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam> 

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{

}
