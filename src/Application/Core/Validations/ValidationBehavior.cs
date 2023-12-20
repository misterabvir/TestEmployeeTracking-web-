using Domain.Common;
using FluentValidation;
using MediatR;

namespace Core.Validations;

internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        var context = new ValidationContext<object>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(v => v.Errors)
            .Where(v => v is not null)
            .ToList();
        if (failures.Any())
        {
            return (TResponse)Result.Failure(new Error("Validation.Error", "Request has not valid parameters", ResultErrorStatus.BadRequest));
        }

        return await next();
    }
}
