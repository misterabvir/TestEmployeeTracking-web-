using MediatR;

namespace ApplicationCore.Abstractions.Common;

/// <summary>
/// Wrapper for MediatR IRequestHandler<TRequest, TResponse> working with ICommand
/// </summary>
/// <typeparam name="TCommand"> Command </typeparam>
/// <typeparam name="TResponse"></typeparam> Response type <summary>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{

}