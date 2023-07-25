using FluentValidation.Results;
using NUnit.Framework;
using PruneUrl.Backend.Application.Requests.Exceptions;

namespace PruneUrl.Backend.Application.Requests.Tests.UnitTests.Exceptions
{
  [TestFixture]
  [Parallelizable]
  public sealed class InvalidRequestExceptionUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      const string requestName = "Test";
      List<ValidationFailure> validationFailures = new List<ValidationFailure>
      {
        new ValidationFailure("Property1", "Error1"),
        new ValidationFailure("Property2", "Error2"),
        new ValidationFailure("Property3", "Error3"),
      };
      string expectedMessage = $"The '{requestName}' request is invalid! Validation errors:{Environment.NewLine}";
      foreach (ValidationFailure validationFailure in validationFailures)
      {
        expectedMessage += $"\tProperty '{validationFailure.PropertyName}' is invalid: {validationFailure.ErrorMessage}{Environment.NewLine}";
      }

      var exception = new InvalidRequestException(requestName, validationFailures);
      Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    #endregion Public Methods
  }
}