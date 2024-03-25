using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PruneUrl.Backend.Application.Requests;

/// <summary>
/// Extensions for the <see cref="IValidator{T}" /> interface.
/// </summary>
internal static class ValidatorExtensions
{
  /// <summary>
  /// Validates the given <paramref name="request" />
  /// </summary>
  /// <typeparam name="TRequest"> </typeparam>
  /// <param name="request"> The request to validate. </param>
  /// <param name="validator"> This <see cref="IValidator{T}" />. </param>
  /// <exception cref="InvalidRequestException">
  /// The exception thrown if the request is invalid.
  /// </exception>
  public static void ValidateRequest<TRequest>(
    this IValidator<TRequest> validator,
    TRequest request
  )
    where TRequest : IBaseRequest
  {
    ValidationResult validationResult = validator.Validate(request);
    if (!validationResult.IsValid)
    {
      throw new InvalidRequestException(validationResult.Errors);
    }
  }
}
