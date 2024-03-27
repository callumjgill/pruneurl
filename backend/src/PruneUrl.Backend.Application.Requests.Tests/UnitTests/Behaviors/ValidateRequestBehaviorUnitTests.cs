using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using NUnit.Framework;

namespace PruneUrl.Backend.Application.Requests.Tests;

[TestFixture]
[Parallelizable]
public sealed class ValidateRequestBehaviorUnitTests
{
  [Test]
  public void HandleTest_Response_IsNotValid()
  {
    IValidator<IRequest<StubResponse>> validator = Substitute.For<
      IValidator<IRequest<StubResponse>>
    >();
    ValidationResult validationResult = Substitute.For<ValidationResult>();
    IRequest<StubResponse> request = Substitute.For<IRequest<StubResponse>>();
    StubResponse response = Substitute.For<StubResponse>();
    CancellationToken cancellationToken = CancellationToken.None;
    Task<StubResponse> next() => Task.FromResult(response);

    validationResult.IsValid.Returns(false);
    validator.Validate(Arg.Any<IRequest<StubResponse>>()).Returns(validationResult);

    ValidateRequestBehavior<IRequest<StubResponse>, StubResponse> behaviour = new(validator);
    Assert.That(
      async () => await behaviour.Handle(request, next, cancellationToken),
      Throws.TypeOf<InvalidRequestException>()
    );
    validator.Received(1).Validate(Arg.Any<IRequest<StubResponse>>());
    validator.Received(1).Validate(request);
  }

  [Test]
  public async Task HandleTest_Response_IsValid()
  {
    IValidator<IRequest<StubResponse>> validator = Substitute.For<
      IValidator<IRequest<StubResponse>>
    >();
    ValidationResult validationResult = Substitute.For<ValidationResult>();
    IRequest<StubResponse> request = Substitute.For<IRequest<StubResponse>>();
    StubResponse response = Substitute.For<StubResponse>();
    CancellationToken cancellationToken = CancellationToken.None;
    Task<StubResponse> next() => Task.FromResult(response);

    validationResult.IsValid.Returns(true);
    validator.Validate(Arg.Any<IRequest<StubResponse>>()).Returns(validationResult);

    ValidateRequestBehavior<IRequest<StubResponse>, StubResponse> decorator = new(validator);
    StubResponse actualResponse = await decorator.Handle(request, next, cancellationToken);
    Assert.That(actualResponse, Is.EqualTo(response));
    validator.Received(1).Validate(Arg.Any<IRequest<StubResponse>>());
    validator.Received(1).Validate(request);
  }

  internal abstract class StubResponse { }
}
