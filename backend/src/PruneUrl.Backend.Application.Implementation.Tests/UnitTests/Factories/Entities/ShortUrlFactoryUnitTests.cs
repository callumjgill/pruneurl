using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Implementation.Factories.Entities;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Implementation.Tests.UnitTests.Factories.Entities
{
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
    public void CreateTest_Valid(string longUrl)
    {
      int testSequenceId = 23652;
      DateTime testCreated = DateTime.Now;
      string shortUrlToUse = "testing123";

      var dateTimeProvider = Substitute.For<IDateTimeProvider>();
      var shortUrlProvider = Substitute.For<IShortUrlProvider>();

      dateTimeProvider.GetNow().Returns(testCreated);
      shortUrlProvider.GetShortUrl(Arg.Any<int>()).Returns(shortUrlToUse);

      var shortUrlFactory = new ShortUrlFactory(dateTimeProvider, shortUrlProvider);

      ShortUrl actualShortUrl = shortUrlFactory.Create(longUrl, testSequenceId);
      Assert.Multiple(() =>
      {
        Assert.That(actualShortUrl.Id, Is.EqualTo(testSequenceId.ToString()));
        Assert.That(actualShortUrl.LongUrl, Is.EqualTo(longUrl));
        Assert.That(actualShortUrl.Url, Is.EqualTo(shortUrlToUse));
        Assert.That(actualShortUrl.Created, Is.EqualTo(testCreated));
      });
      dateTimeProvider.Received(1).GetNow();
      shortUrlProvider.Received(1).GetShortUrl(Arg.Any<int>());
      shortUrlProvider.Received(1).GetShortUrl(testSequenceId);
    }
  }
}
