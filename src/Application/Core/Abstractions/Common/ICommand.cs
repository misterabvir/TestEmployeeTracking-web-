using MediatR;

namespace Core.Abstractions.Common;

public interface ICommand<TResponse> : IRequest<TResponse>
{

}
