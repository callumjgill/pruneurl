using FluentValidation.Results;
using NUnit.Framework;
using PruneUrl.Backend.Application.Requests.Exceptions;

namespace PruneUrl.Backend.Application.Requests.Tests.UnitTests.Exceptions
{
  [TestFixture]
  [Parallelizable]
  public sealed class InvalidRequestExceptionUnitTests
  {
    [Test]
    public void ConstructorTest()
    {
      var validationFailures = new List<ValidationFailure>
      {
        new ValidationFailure("Property1", string.Empty),
        new ValidationFailure("Property2", string.Empty),
        new ValidationFailure("Property3", string.Empty)
      };
      string expectedMessage = $"The request is invalid!{Environment.NewLine}";
      foreach (ValidationFailure validationFailure in validationFailures)
      {
        expectedMessage +=
          $"\tProperty '{validationFailure.PropertyName}' is invalid.{Environment.NewLine}";
      }

      var exception = new InvalidRequestException(validationFailures);
      Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
  }
}
