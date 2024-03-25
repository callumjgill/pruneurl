using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Application.Queries.GetShortUrl;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetShortUrl;

[TestFixture]
[Parallelizable]
public sealed class GetShortUrlQueryValidatorUnitTests
{
  [TestCase("")]
  [TestCase("        ")]
  [TestCase(null)]
  public void ValidateTest_InvalidProperties(string? testShortUrl)
  {
#pragma warning disable CS8604 // Possible null reference argument.
    var query = new GetShortUrlQuery(testShortUrl);
#pragma warning restore CS8604 // Possible null reference argument.
    var validator = new GetShortUrlQueryValidator(Substitute.For<ISequenceIdProvider>());

    TestValidationResult<GetShortUrlQuery> result = validator.TestValidate(query);
    result.ShouldHaveValidationErrorFor(qy => qy.ShortUrl);
  }

  [Test]
  public void ValidateTest_InvalidShortUrl_SequenceIdProviderReturnsNegativeNumber()
  {
    ISequenceIdProvider sequenceIdProvider = Substitute.For<ISequenceIdProvider>();

    sequenceIdProvider.GetSequenceId(Arg.Any<string>()).Returns(-1);

    const string testShortUrl = "Testing123";
    var query = new GetShortUrlQuery(testShortUrl);
    var validator = new GetShortUrlQueryValidator(sequenceIdProvider);

    TestValidationResult<GetShortUrlQuery> result = validator.TestValidate(query);
    result.ShouldHaveValidationErrorFor(qy => qy.ShortUrl);
    sequenceIdProvider.Received(1).GetSequenceId(Arg.Any<string>());
    sequenceIdProvider.Received(1).GetSequenceId(testShortUrl);
  }

  [Test]
  public void ValidateTest_ValidProperties()
  {
    const string testShortUrl = "Testing123";
    var query = new GetShortUrlQuery(testShortUrl);
    var validator = new GetShortUrlQueryValidator(Substitute.For<ISequenceIdProvider>());

    TestValidationResult<GetShortUrlQuery> result = validator.TestValidate(query);
    result.ShouldNotHaveValidationErrorFor(qy => qy.ShortUrl);
  }
}
