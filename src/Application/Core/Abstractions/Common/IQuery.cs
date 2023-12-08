using MediatR;

namespace ApplicationCore.Abstractions.Common;

public interface IQuery<TResponse> : IRequest<TResponse>
{

}
