using FluentValidation.TestHelper;
using NUnit.Framework;
using PruneUrl.Backend.Application.Queries.GetSequenceId;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetSequenceId;

[TestFixture]
[Parallelizable]
public sealed class GetSequenceIdQueryValidatorUnitTests
{
  [TestCase(null)]
  [TestCase("")]
  [TestCase("             ")]
  public void ValidateTest_InvalidProperties(string? testId)
  {
#pragma warning disable CS8604 // Possible null reference argument.
    var query = new GetSequenceIdQuery(testId);
#pragma warning restore CS8604 // Possible null reference argument.
    var validator = new GetSequenceIdQueryValidator();

    TestValidationResult<GetSequenceIdQuery> result = validator.TestValidate(query);
    result.ShouldHaveValidationErrorFor(qry => qry.Id);
  }

  [Test]
  public void ValidateTest_ValidProperties()
  {
    const string testId = "testing123";
    var query = new GetSequenceIdQuery(testId);
    var validator = new GetSequenceIdQueryValidator();

    TestValidationResult<GetSequenceIdQuery> result = validator.TestValidate(query);
    result.ShouldNotHaveValidationErrorFor(qry => qry.Id);
  }
}
