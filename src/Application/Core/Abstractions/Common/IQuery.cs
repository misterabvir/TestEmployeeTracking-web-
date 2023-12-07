using MediatR;

namespace Core.Abstractions.Common;

public interface IQuery<TResponse> : IRequest<TResponse>
{

}
