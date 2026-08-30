using FluentValidation;
using MediatR;
using Inventory.Domain.Common.Results;
using Inventory.Domain.Common.Results.Abstractions;
using Microsoft.Extensions.Logging;

namespace Contract.Common.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            if (!_validators.Any())
            {
                _logger.LogInformation("No validators registered for {RequestName}. Continuing request.", requestName);
                return await next(cancellationToken);
            }

            _logger.LogInformation("Running {ValidatorCount} validators for {RequestName}.", _validators.Count(), requestName);

            var context = new ValidationContext<TRequest>(request);

            var failures = (await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken))))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .ToList();

            if (!failures.Any())
            {
                _logger.LogInformation("Validation passed for {RequestName}.", requestName);
                return await next(cancellationToken);
            }

            _logger.LogWarning("Validation failed for {RequestName}. Errors: {ValidationErrors}",
                requestName,
                string.Join(" | ", failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}")));

            var errors = failures
                .Select(f => Error.Validation($"Validation.{f.PropertyName}", f.ErrorMessage))
                .ToList();

            return (dynamic)errors;
        }
    }
}
