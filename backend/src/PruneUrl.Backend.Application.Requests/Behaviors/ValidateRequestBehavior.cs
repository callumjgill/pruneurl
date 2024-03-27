using FluentValidation;
using MediatR;

namespace PruneUrl.Backend.Application.Requests;

/// <summary>
/// A decorator which provides a validation as part of a request pipeline. This should be the
/// first in the pipeline, or at the very least before the actual request handler.
/// </summary>
/// <typeparam name="TRequest"> The request. </typeparam>
/// <typeparam name="TResponse"> The request response. </typeparam>
/// <param name="validator"> The <see cref="IValidator{T}" /> to use for validating the request. </param>
public sealed class ValidateRequestBehavior<TRequest, TResponse>(IValidator<TRequest> validator)
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IValidator<TRequest> validator = validator;

  /// <inheritdoc cref="IPipelineBehavior{TRequest, TResponse}.Handle(TRequest, RequestHandlerDelegate{TResponse}, CancellationToken)" />
  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken
  )
  {
    validator.ValidateRequest(request);
    TResponse response = await next();
    return response;
  }
}
