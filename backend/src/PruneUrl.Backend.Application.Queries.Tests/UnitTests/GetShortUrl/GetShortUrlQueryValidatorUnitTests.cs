using FluentValidation.TestHelper;
using NUnit.Framework;
using PruneUrl.Backend.Application.Queries.GetShortUrl;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetShortUrl
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetShortUrlQueryValidatorUnitTests
  {
    #region Public Methods

    [TestCase("")]
    [TestCase("        ")]
    [TestCase(null)]
    public void ValidateTest_InvalidProperties(string? testShortUrl)
    {
#pragma warning disable CS8604 // Possible null reference argument.
      var query = new GetShortUrlQuery(testShortUrl);
#pragma warning restore CS8604 // Possible null reference argument.
      var validator = new GetShortUrlQueryValidator();

      TestValidationResult<GetShortUrlQuery> result = validator.TestValidate(query);
      result.ShouldHaveValidationErrorFor(qy => qy.ShortUrl);
    }

    [Test]
    public void ValidateTest_ValidProperties()
    {
      const string testShortUrl = "Testing123";
      var query = new GetShortUrlQuery(testShortUrl);
      var validator = new GetShortUrlQueryValidator();

      TestValidationResult<GetShortUrlQuery> result = validator.TestValidate(query);
      result.ShouldNotHaveValidationErrorFor(qy => qy.ShortUrl);
    }

    #endregion Public Methods
  }
}