using MediatR;

namespace ApplicationCore.Abstractions.Common;

public interface ICommand<TResponse> : IRequest<TResponse>
{

}
