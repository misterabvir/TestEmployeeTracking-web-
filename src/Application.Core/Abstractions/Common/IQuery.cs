using MediatR;

namespace ApplicationCore.Abstractions.Common;

/// <summary>
///  Wrapper for MediatR IRequest for queries
/// </summary>
/// <typeparam name="TResponse"></typeparam> <summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<TResponse> : IRequest<TResponse>
{

}
