using MediatR;

namespace ApplicationCore.Abstractions.Common;

/// <summary>
/// Wrapper for MediatR IRequest for commands
/// </summary>
/// <typeparam name="TResponse"> Response type </typeparam>
public interface ICommand<TResponse> : IRequest<TResponse>
{

}
