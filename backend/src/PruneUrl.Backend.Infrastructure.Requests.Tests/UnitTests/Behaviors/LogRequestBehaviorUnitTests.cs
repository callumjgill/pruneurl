using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Infrastructure.Requests.Tests;

[TestFixture]
[Parallelizable]
public sealed class LogRequestBehaviorUnitTests
{
  [Test]
  public async Task HandleTest_Success()
  {
    ILogger<IRequest<StubResponse>> logger = Substitute.For<ILogger<IRequest<StubResponse>>>();
    LogRequestBehavior<IRequest<StubResponse>, StubResponse> requestBehavior = new(logger);
    IRequest<StubResponse> request = Substitute.For<IRequest<StubResponse>>();
    StubResponse response = Substitute.For<StubResponse>();
    CancellationToken cancellationToken = CancellationToken.None;
    Task<StubResponse> next() => Task.FromResult(response);

    StubResponse actualResponse = await requestBehavior.Handle(request, next, cancellationToken);
    Assert.That(actualResponse, Is.EqualTo(response));
    logger.RecievedLogWithStringMessage("Handling incoming request...", LogLevel.Information, 1);
    logger.RecievedLogWithStringMessage("Request handled.", LogLevel.Information, 1);
    logger.RecievedLogWithStringMessage($"Request object: {request}", LogLevel.Debug, 1);
    logger.RecievedLogWithStringMessage($"Response object: {response}", LogLevel.Debug, 1);
  }

  [Test]
  public void HandleTest_Failed()
  {
    ILogger<IRequest<StubResponse>> logger = Substitute.For<ILogger<IRequest<StubResponse>>>();
    LogRequestBehavior<IRequest<StubResponse>, StubResponse> requestBehavior = new(logger);
    IRequest<StubResponse> request = Substitute.For<IRequest<StubResponse>>();
    Exception exception = new();
    CancellationToken cancellationToken = CancellationToken.None;
    Task<StubResponse> next() => Task.FromException<StubResponse>(exception);

    Assert.That(
      async () => await requestBehavior.Handle(request, next, cancellationToken),
      Throws.Exception.EqualTo(exception)
    );
    logger.RecievedLogWithStringMessage("Handling incoming request...", LogLevel.Information, 1);
    logger.RecievedLogWithStringMessage("Request handled.", LogLevel.Information, 0);
    logger.RecievedLogWithStringMessage($"Request object: {request}", LogLevel.Debug, 1);
    logger.RecievedLogWithStringMessage(
      "An error occurred handling the request!",
      LogLevel.Error,
      1,
      exception
    );
  }

  internal abstract class StubResponse { }
}
