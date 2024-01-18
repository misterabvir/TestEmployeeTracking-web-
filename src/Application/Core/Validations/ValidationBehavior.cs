using Domain.Common;
using FluentValidation;
using MediatR;

namespace Core.Validations;

/// <summary>
/// Pipeline behavior for validation of requests
/// </summary>
/// <typeparam name="TRequest"> Request with data </typeparam>
/// <typeparam name="TResponse"> Result with data or error </typeparam>
internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result
{
    /// <summary>
    /// All validators for TRequest
    /// </summary>
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators"> All validators for TRequest</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    /// <summary>
    /// Handles validation
    /// </summary>
    /// <param name="request"> Incoming request </param>
    /// <param name="next"> Next handler </param>
    /// <param name="cancellationToken"> Cancellation token </param>
    /// <returns> Result with data or error </returns>
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
            return (dynamic)Activator
                .CreateInstance(
                    typeof(Error<>).MakeGenericType(typeof(TResponse).GenericTypeArguments.First()), 
                    "Validation.Error", 
                    failures.First().ErrorMessage, 
                    ResultErrorStatus.BadRequest)!;           
        }

        return await next();
    }
}
