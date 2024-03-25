using FluentValidation.TestHelper;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands.CreateShortUrl;

namespace PruneUrl.Backend.Application.Commands.Tests.UnitTests.CreateShortUrl;

[TestFixture]
[Parallelizable]
public sealed class CreateShortUrlCommandValidatorUnitTests
{
  [Test]
  public void ValidateTest_InvalidProperties()
  {
    string testLongUrl = "testing123";
    int testSequenceId = -6124323;
    var command = new CreateShortUrlCommand(testLongUrl, testSequenceId);
    var validator = new CreateShortUrlCommandValidator();

    TestValidationResult<CreateShortUrlCommand> result = validator.TestValidate(command);
    result.ShouldHaveValidationErrorFor(cmd => cmd.LongUrl);
    result.ShouldHaveValidationErrorFor(cmd => cmd.SequenceId);
  }

  [Test]
  public void ValidateTest_ValidProperties()
  {
    string testLongUrl = "https://www.youtube.com";
    int testSequenceId = 6124323;
    var command = new CreateShortUrlCommand(testLongUrl, testSequenceId);
    var validator = new CreateShortUrlCommandValidator();

    TestValidationResult<CreateShortUrlCommand> result = validator.TestValidate(command);
    result.ShouldNotHaveValidationErrorFor(cmd => cmd.LongUrl);
    result.ShouldNotHaveValidationErrorFor(cmd => cmd.SequenceId);
  }
}
