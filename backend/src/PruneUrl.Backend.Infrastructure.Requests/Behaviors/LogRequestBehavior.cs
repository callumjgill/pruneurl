using MediatR;
using Microsoft.Extensions.Logging;

namespace PruneUrl.Backend.Infrastructure.Requests;

/// <summary>
/// A behavior which provides logging as part of a request pipeline. This should be the
/// first in the pipeline.
/// </summary>
/// <typeparam name="TRequest"> The request. </typeparam>
/// <typeparam name="TResponse"> The request response. </typeparam>
/// <param name="logger"> The <see cref="ILogger{T}" /> to append log entries to. </param>
public sealed class LogRequestBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly ILogger<TRequest> logger = logger;

  /// <inheritdoc cref="IPipelineBehavior{TRequest, TResponse}.Handle(TRequest, RequestHandlerDelegate{TResponse}, CancellationToken)" />
  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken
  )
  {
    try
    {
      logger.LogInformation("Handling incoming request...");
      logger.LogDebug("Request object: {request}", request);

      TResponse response = await next();

      logger.LogDebug("Response object: {response}", response);
      logger.LogInformation("Request handled.");

      return response;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An error occurred handling the request!");
      throw;
    }
  }
}
