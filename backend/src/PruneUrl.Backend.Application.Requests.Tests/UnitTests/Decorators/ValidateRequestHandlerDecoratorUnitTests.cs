using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Requests.Decorators;
using PruneUrl.Backend.Application.Requests.Exceptions;

namespace PruneUrl.Backend.Application.Requests.Tests.UnitTests.Decorators
{
  [TestFixture]
  [Parallelizable]
  public sealed class ValidateRequestHandlerDecoratorUnitTests
  {
    #region Public Methods

    [Test]
    public void HandleTest_NoResponse_IsNotValid()
    {
      var requestHandlerMock = new Mock<IRequestHandler<IRequest>>();
      var validatorMock = new Mock<IValidator<IRequest>>();
      var validationResultMock = new Mock<ValidationResult>();
      var requestMock = Mock.Of<IRequest>();
      CancellationToken cancellationToken = CancellationToken.None;

      validationResultMock.Setup(x => x.IsValid).Returns(false);
      validatorMock.Setup(x => x.Validate(It.IsAny<IRequest>())).Returns(validationResultMock.Object);
      requestHandlerMock.Setup(x => x.Handle(It.IsAny<IRequest>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

      var decorator = new ValidateRequestHandlerDecorator<IRequest>(requestHandlerMock.Object, validatorMock.Object);
      Assert.That(async () => await decorator.Handle(requestMock, cancellationToken), Throws.TypeOf<InvalidRequestException>());
      validatorMock.Verify(x => x.Validate(It.IsAny<IRequest>()), Times.Once);
      validatorMock.Verify(x => x.Validate(requestMock), Times.Once);
      requestHandlerMock.Verify(x => x.Handle(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()), Times.Never);
      requestHandlerMock.Verify(x => x.Handle(requestMock, cancellationToken), Times.Never);
    }

    [Test]
    public void HandleTest_NoResponse_IsValid()
    {
      var requestHandlerMock = new Mock<IRequestHandler<IRequest>>();
      var validatorMock = new Mock<IValidator<IRequest>>();
      var validationResultMock = new Mock<ValidationResult>();
      var requestMock = Mock.Of<IRequest>();
      CancellationToken cancellationToken = CancellationToken.None;

      validationResultMock.Setup(x => x.IsValid).Returns(true);
      validatorMock.Setup(x => x.Validate(It.IsAny<IRequest>())).Returns(validationResultMock.Object);
      requestHandlerMock.Setup(x => x.Handle(It.IsAny<IRequest>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

      var decorator = new ValidateRequestHandlerDecorator<IRequest>(requestHandlerMock.Object, validatorMock.Object);
      Assert.That(async () => await decorator.Handle(requestMock, cancellationToken), Throws.Nothing);
      validatorMock.Verify(x => x.Validate(It.IsAny<IRequest>()), Times.Once);
      validatorMock.Verify(x => x.Validate(requestMock), Times.Once);
      requestHandlerMock.Verify(x => x.Handle(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()), Times.Once);
      requestHandlerMock.Verify(x => x.Handle(requestMock, cancellationToken), Times.Once);
    }

    [Test]
    public void HandleTest_Response_IsNotValid()
    {
      var requestHandlerMock = new Mock<IRequestHandler<IRequest<StubResponse>, StubResponse>>();
      var validatorMock = new Mock<IValidator<IRequest<StubResponse>>>();
      var validationResultMock = new Mock<ValidationResult>();
      var requestMock = Mock.Of<IRequest<StubResponse>>();
      var responseMock = Mock.Of<StubResponse>();
      CancellationToken cancellationToken = CancellationToken.None;

      validationResultMock.Setup(x => x.IsValid).Returns(false);
      validatorMock.Setup(x => x.Validate(It.IsAny<IRequest<StubResponse>>())).Returns(validationResultMock.Object);
      requestHandlerMock.Setup(x => x.Handle(It.IsAny<IRequest<StubResponse>>(), It.IsAny<CancellationToken>())).ReturnsAsync(responseMock);

      var decorator = new ValidateRequestHandlerDecorator<IRequest<StubResponse>, StubResponse>(requestHandlerMock.Object, validatorMock.Object);
      Assert.That(async () => await decorator.Handle(requestMock, cancellationToken), Throws.TypeOf<InvalidRequestException>());
      validatorMock.Verify(x => x.Validate(It.IsAny<IRequest<StubResponse>>()), Times.Once);
      validatorMock.Verify(x => x.Validate(requestMock), Times.Once);
      requestHandlerMock.Verify(x => x.Handle(It.IsAny<IRequest<StubResponse>>(), It.IsAny<CancellationToken>()), Times.Never);
      requestHandlerMock.Verify(x => x.Handle(requestMock, cancellationToken), Times.Never);
    }

    [Test]
    public async Task HandleTest_Response_IsValid()
    {
      var requestHandlerMock = new Mock<IRequestHandler<IRequest<StubResponse>, StubResponse>>();
      var validatorMock = new Mock<IValidator<IRequest<StubResponse>>>();
      var validationResultMock = new Mock<ValidationResult>();
      var requestMock = Mock.Of<IRequest<StubResponse>>();
      var responseMock = Mock.Of<StubResponse>();
      CancellationToken cancellationToken = CancellationToken.None;

      validationResultMock.Setup(x => x.IsValid).Returns(true);
      validatorMock.Setup(x => x.Validate(It.IsAny<IRequest<StubResponse>>())).Returns(validationResultMock.Object);
      requestHandlerMock.Setup(x => x.Handle(It.IsAny<IRequest<StubResponse>>(), It.IsAny<CancellationToken>())).ReturnsAsync(responseMock);

      var decorator = new ValidateRequestHandlerDecorator<IRequest<StubResponse>, StubResponse>(requestHandlerMock.Object, validatorMock.Object);
      StubResponse response = await decorator.Handle(requestMock, cancellationToken);
      Assert.That(response, Is.EqualTo(responseMock));
      validatorMock.Verify(x => x.Validate(It.IsAny<IRequest<StubResponse>>()), Times.Once);
      validatorMock.Verify(x => x.Validate(requestMock), Times.Once);
      requestHandlerMock.Verify(x => x.Handle(It.IsAny<IRequest<StubResponse>>(), It.IsAny<CancellationToken>()), Times.Once);
      requestHandlerMock.Verify(x => x.Handle(requestMock, cancellationToken), Times.Once);
    }

    #endregion Public Methods

    #region Internal Classes

    internal abstract class StubResponse
    {
    }

    #endregion Internal Classes
  }
}