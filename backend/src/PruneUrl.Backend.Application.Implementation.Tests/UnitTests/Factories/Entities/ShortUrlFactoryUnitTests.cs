using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Implementation.Tests;

[TestFixture]
[Parallelizable]
public sealed class ShortUrlFactoryUnitTests
{
  [TestCase("")]
  [TestCase("www.youtube.com")]
  [TestCase("https://www.youtube.com")]
  [TestCase("This is a load gibberish")]
  [TestCase("")]
  [TestCase("www.youtube.com")]
  [TestCase("https://www.youtube.com")]
  [TestCase("This is a load gibberish")]
  public void CreateTest_LongUrlAndSequenceIdParameters(string longUrl)
  {
    const int testSequenceId = 23652;
    const string shortUrlToUse = "testing123";

    IShortUrlProvider shortUrlProvider = Substitute.For<IShortUrlProvider>();

    shortUrlProvider.GetShortUrl(Arg.Any<int>()).Returns(shortUrlToUse);

    ShortUrlFactory shortUrlFactory = new(shortUrlProvider);

    ShortUrl actualShortUrl = shortUrlFactory.Create(longUrl, testSequenceId);
    Assert.Multiple(() =>
    {
      Assert.That(actualShortUrl.Id, Is.EqualTo(testSequenceId));
      Assert.That(actualShortUrl.LongUrl, Is.EqualTo(longUrl));
      Assert.That(actualShortUrl.Url, Is.EqualTo(shortUrlToUse));
    });
    shortUrlProvider.Received(1).GetShortUrl(Arg.Any<int>());
    shortUrlProvider.Received(1).GetShortUrl(testSequenceId);
  }

  [Test]
  public void CreateTest_NoParameters()
  {
    ShortUrlFactory shortUrlFactory = new(Substitute.For<IShortUrlProvider>());

    ShortUrl actualShortUrl = shortUrlFactory.Create();
    Assert.Multiple(() =>
    {
      Assert.That(actualShortUrl.Id, Is.EqualTo(0));
      Assert.That(actualShortUrl.LongUrl, Is.EqualTo(string.Empty));
      Assert.That(actualShortUrl.Url, Is.EqualTo(string.Empty));
    });
  }
}
