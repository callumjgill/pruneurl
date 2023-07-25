using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using PruneUrl.Backend.Application.Requests.Exceptions;
using PruneUrl.Backend.Application.Requests.Extensions;

namespace PruneUrl.Backend.Application.Requests.Tests.UnitTests.Extensions
{
  [TestFixture]
  [Parallelizable]
  public sealed class ValidatorExtensionsUnitTests
  {
    #region Public Methods

    [TestCase(true)]
    [TestCase(false)]
    public void ValidateRequestTest(bool isValid)
    {
      var validatorMock = new Mock<IValidator<IRequest>>();
      var requestMock = Mock.Of<IRequest>();
      var validationResultMock = new Mock<ValidationResult>();

      validationResultMock.Setup(x => x.IsValid).Returns(isValid);
      validatorMock.Setup(x => x.Validate(It.IsAny<IRequest>())).Returns(validationResultMock.Object);

      Constraint expectedOutcome = isValid ? Throws.Nothing : Throws.TypeOf<InvalidRequestException>();
      Assert.That(() => validatorMock.Object.ValidateRequest(requestMock), expectedOutcome);
      validatorMock.Verify(x => x.Validate(It.IsAny<IRequest>()), Times.Once);
      validatorMock.Verify(x => x.Validate(requestMock), Times.Once);
    }

    #endregion Public Methods
  }
}