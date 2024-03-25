using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Requests.Decorators;
using PruneUrl.Backend.Application.Requests.Exceptions;

namespace PruneUrl.Backend.Application.Requests.Tests.UnitTests.Decorators;

[TestFixture]
[Parallelizable]
public sealed class ValidateRequestHandlerDecoratorUnitTests
{
  [Test]
  public void HandleTest_NoResponse_IsNotValid()
  {
    var requestHandler = Substitute.For<IRequestHandler<IRequest>>();
    var validator = Substitute.For<IValidator<IRequest>>();
    var validationResult = Substitute.For<ValidationResult>();
    var request = Substitute.For<IRequest>();
    CancellationToken cancellationToken = CancellationToken.None;

    validationResult.IsValid.Returns(false);
    validator.Validate(Arg.Any<IRequest>()).Returns(validationResult);
    requestHandler
      .Handle(Arg.Any<IRequest>(), Arg.Any<CancellationToken>())
      .Returns(Task.CompletedTask);

    var decorator = new ValidateRequestHandlerDecorator<IRequest>(requestHandler, validator);
    Assert.That(
      async () => await decorator.Handle(request, cancellationToken),
      Throws.TypeOf<InvalidRequestException>()
    );
    validator.Received(1).Validate(Arg.Any<IRequest>());
    validator.Received(1).Validate(request);
    requestHandler.DidNotReceive().Handle(Arg.Any<IRequest>(), Arg.Any<CancellationToken>());
    requestHandler.DidNotReceive().Handle(request, cancellationToken);
  }

  [Test]
  public void HandleTest_NoResponse_IsValid()
  {
    var requestHandler = Substitute.For<IRequestHandler<IRequest>>();
    var validator = Substitute.For<IValidator<IRequest>>();
    var validationResult = Substitute.For<ValidationResult>();
    var request = Substitute.For<IRequest>();
    CancellationToken cancellationToken = CancellationToken.None;

    validationResult.IsValid.Returns(true);
    validator.Validate(Arg.Any<IRequest>()).Returns(validationResult);
    requestHandler
      .Handle(Arg.Any<IRequest>(), Arg.Any<CancellationToken>())
      .Returns(Task.CompletedTask);

    var decorator = new ValidateRequestHandlerDecorator<IRequest>(requestHandler, validator);
    Assert.That(async () => await decorator.Handle(request, cancellationToken), Throws.Nothing);
    validator.Received(1).Validate(Arg.Any<IRequest>());
    validator.Received(1).Validate(request);
    requestHandler.Received(1).Handle(Arg.Any<IRequest>(), Arg.Any<CancellationToken>());
    requestHandler.Received(1).Handle(request, cancellationToken);
  }

  [Test]
  public void HandleTest_Response_IsNotValid()
  {
    var requestHandler = Substitute.For<IRequestHandler<IRequest<StubResponse>, StubResponse>>();
    var validator = Substitute.For<IValidator<IRequest<StubResponse>>>();
    var validationResult = Substitute.For<ValidationResult>();
    var request = Substitute.For<IRequest<StubResponse>>();
    var response = Substitute.For<StubResponse>();
    CancellationToken cancellationToken = CancellationToken.None;

    validationResult.IsValid.Returns(false);
    validator.Validate(Arg.Any<IRequest<StubResponse>>()).Returns(validationResult);
    requestHandler
      .Handle(Arg.Any<IRequest<StubResponse>>(), Arg.Any<CancellationToken>())
      .Returns(response);

    var decorator = new ValidateRequestHandlerDecorator<IRequest<StubResponse>, StubResponse>(
      requestHandler,
      validator
    );
    Assert.That(
      async () => await decorator.Handle(request, cancellationToken),
      Throws.TypeOf<InvalidRequestException>()
    );
    validator.Received(1).Validate(Arg.Any<IRequest<StubResponse>>());
    validator.Received(1).Validate(request);
    requestHandler
      .DidNotReceive()
      .Handle(Arg.Any<IRequest<StubResponse>>(), Arg.Any<CancellationToken>());
    requestHandler.DidNotReceive().Handle(request, cancellationToken);
  }

  [Test]
  public async Task HandleTest_Response_IsValid()
  {
    var requestHandler = Substitute.For<IRequestHandler<IRequest<StubResponse>, StubResponse>>();
    var validator = Substitute.For<IValidator<IRequest<StubResponse>>>();
    var validationResult = Substitute.For<ValidationResult>();
    var request = Substitute.For<IRequest<StubResponse>>();
    var response = Substitute.For<StubResponse>();
    CancellationToken cancellationToken = CancellationToken.None;

    validationResult.IsValid.Returns(true);
    validator.Validate(Arg.Any<IRequest<StubResponse>>()).Returns(validationResult);
    requestHandler
      .Handle(Arg.Any<IRequest<StubResponse>>(), Arg.Any<CancellationToken>())
      .Returns(response);

    var decorator = new ValidateRequestHandlerDecorator<IRequest<StubResponse>, StubResponse>(
      requestHandler,
      validator
    );
    StubResponse actualResponse = await decorator.Handle(request, cancellationToken);
    Assert.That(actualResponse, Is.EqualTo(response));
    validator.Received(1).Validate(Arg.Any<IRequest<StubResponse>>());
    validator.Received(1).Validate(request);
    await requestHandler
      .Received(1)
      .Handle(Arg.Any<IRequest<StubResponse>>(), Arg.Any<CancellationToken>());
    await requestHandler.Received(1).Handle(request, cancellationToken);
  }

  internal abstract class StubResponse { }
}
