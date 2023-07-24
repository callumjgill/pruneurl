using FluentValidation;
using MediatR;
using PruneUrl.Backend.Application.Requests.Extensions;

namespace PruneUrl.Backend.Application.Requests.Decorators
{
  /// <summary>
  /// A decorator which provides a validation as part of a request pipeline. This should be the
  /// first in the pipeline, or at the very least before the actual request handler.
  /// </summary>
  /// <typeparam name="TRequest"> The request. </typeparam>
  public sealed class ValidateRequestHandlerDecorator<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
  {
    #region Private Fields

    private readonly IRequestHandler<TRequest> requestHandlerToDecorate;
    private readonly IValidator<TRequest> validator;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="ValidateRequestHandlerDecorator{TRequest}" /> class.
    /// </summary>
    /// <param name="requestHandlerToDecorate">
    /// The <see cref="IRequestHandler{TRequest, TResponse}" /> to decorate.
    /// </param>
    /// <param name="validator"> The <see cref="IValidator{T}" /> to use for validating the request. </param>
    public ValidateRequestHandlerDecorator(IRequestHandler<TRequest> requestHandlerToDecorate, IValidator<TRequest> validator)
    {
      this.requestHandlerToDecorate = requestHandlerToDecorate;
      this.validator = validator;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
    public async Task Handle(TRequest request, CancellationToken cancellationToken)
    {
      validator.ValidateRequest(request);
      await requestHandlerToDecorate.Handle(request, cancellationToken);
    }

    #endregion Public Methods
  }

  /// <summary>
  /// A decorator which provides a validation as part of a request pipeline. This should be the
  /// first in the pipeline, or at the very least before the actual request handler.
  /// </summary>
  /// <typeparam name="TRequest"> The request. </typeparam>
  /// <typeparam name="TResponse"> The request response. </typeparam>
  public sealed class ValidateRequestHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
  {
    #region Private Fields

    private readonly IRequestHandler<TRequest, TResponse> requestHandlerToDecorate;
    private readonly IValidator<TRequest> validator;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="ValidateRequestHandlerDecorator{TRequest}" /> class.
    /// </summary>
    /// <param name="requestHandlerToDecorate">
    /// The <see cref="IRequestHandler{TRequest, TResponse}" /> to decorate.
    /// </param>
    /// <param name="validator"> The <see cref="IValidator{T}" /> to use for validating the request. </param>
    public ValidateRequestHandlerDecorator(IRequestHandler<TRequest, TResponse> requestHandlerToDecorate, IValidator<TRequest> validator)
    {
      this.requestHandlerToDecorate = requestHandlerToDecorate;
      this.validator = validator;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)" />
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
      validator.ValidateRequest(request);
      TResponse response = await requestHandlerToDecorate.Handle(request, cancellationToken);
      return response;
    }

    #endregion Public Methods
  }
}