using FluentValidation.TestHelper;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace PruneUrl.Backend.Application.Commands.Tests;

[TestFixture]
[Parallelizable]
public sealed class CreateSequenceIdCommandValidatorUnitTests
{
  [TestCase(null)]
  [TestCase("")]
  [TestCase("             ")]
  public void ValidateTest_InvalidProperties(string? testId)
  {
#pragma warning disable CS8604 // Possible null reference argument.
    var command = new CreateSequenceIdCommand(testId);
#pragma warning restore CS8604 // Possible null reference argument.
    var validator = new CreateSequenceIdCommandValidator();

    TestValidationResult<CreateSequenceIdCommand> result = validator.TestValidate(command);
    result.ShouldHaveValidationErrorFor(cmd => cmd.Id);
  }

  [Test]
  public void ValidateTest_ValidProperties()
  {
    const string testId = "testing123";
    var command = new CreateSequenceIdCommand(testId);
    var validator = new CreateSequenceIdCommandValidator();

    TestValidationResult<CreateSequenceIdCommand> result = validator.TestValidate(command);
    result.ShouldNotHaveValidationErrorFor(cmd => cmd.Id);
  }
}
