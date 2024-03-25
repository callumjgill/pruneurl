using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
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
    [TestCase(true)]
    [TestCase(false)]
    public void ValidateRequestTest(bool isValid)
    {
      var validator = Substitute.For<IValidator<IRequest>>();
      var request = Substitute.For<IRequest>();
      var validationResult = Substitute.For<ValidationResult>();

      validationResult.IsValid.Returns(isValid);
      validator.Validate(Arg.Any<IRequest>()).Returns(validationResult);

      Constraint expectedOutcome = isValid
        ? Throws.Nothing
        : Throws.TypeOf<InvalidRequestException>();
      Assert.That(() => validator.ValidateRequest(request), expectedOutcome);
      validator.Received(1).Validate(Arg.Any<IRequest>());
      validator.Received(1).Validate(request);
    }
  }
}
