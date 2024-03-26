using FluentValidation.TestHelper;
using NUnit.Framework;

namespace PruneUrl.Backend.Application.Commands.Tests;

[TestFixture]
[Parallelizable]
public sealed class CreateShortUrlCommandValidatorUnitTests
{
  [Test]
  public void ValidateTest_InvalidProperties()
  {
    const string testLongUrl = "testing123";
    CreateShortUrlCommand command = new(testLongUrl);
    CreateShortUrlCommandValidator validator = new();

    TestValidationResult<CreateShortUrlCommand> result = validator.TestValidate(command);
    result.ShouldHaveValidationErrorFor(cmd => cmd.LongUrl);
  }

  [Test]
  public void ValidateTest_ValidProperties()
  {
    const string testLongUrl = "https://www.youtube.com";
    CreateShortUrlCommand command = new(testLongUrl);
    CreateShortUrlCommandValidator validator = new();

    TestValidationResult<CreateShortUrlCommand> result = validator.TestValidate(command);
    result.ShouldNotHaveValidationErrorFor(cmd => cmd.LongUrl);
  }
}
