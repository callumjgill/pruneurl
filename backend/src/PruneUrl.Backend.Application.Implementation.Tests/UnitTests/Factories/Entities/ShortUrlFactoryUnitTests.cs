using Moq;
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
    #region Public Methods

    [Test]
    public void CreateTest_Invalid()
    {
      var shortUrlFactory = new ShortUrlFactory(
        Mock.Of<IDateTimeProvider>(),
        Mock.Of<IEntityIdProvider>(),
        Mock.Of<IShortUrlProvider>());

      Assert.That(() => shortUrlFactory.Create(string.Empty), Throws.TypeOf<ArgumentNullException>());
    }

    [TestCase("", null)]
    [TestCase("www.youtube.com", null)]
    [TestCase("https://www.youtube.com", null)]
    [TestCase("This is a load gibberish", null)]
    [TestCase("", "")]
    [TestCase("www.youtube.com", "absdf")]
    [TestCase("https://www.youtube.com", "kjh")]
    [TestCase("This is a load gibberish", "This is not a load of gibberish")]
    public void CreateTest_Valid(string longUrl, string? shortUrl)
    {
      int? testSequenceId = shortUrl == null ? 0 : null;
      string testId = Guid.NewGuid().ToString();
      DateTime testCreated = DateTime.Now;
      string shortUrlToUse = shortUrl ?? "testing123";

      Times invocationsForDateTimeProvider = Times.Once();
      Times invocationsForEntityIdProvider = Times.Once();
      Times invocationsForShortUrlProvider = shortUrl == null ? Times.Once() : Times.Never();

      var dateTimeProviderMock = new Mock<IDateTimeProvider>();
      var entityIdProviderMock = new Mock<IEntityIdProvider>();
      var shortUrlProviderMock = new Mock<IShortUrlProvider>();

      dateTimeProviderMock.Setup(x => x.GetNow()).Returns(testCreated);
      entityIdProviderMock.Setup(x => x.NewId()).Returns(testId);
      shortUrlProviderMock.Setup(x => x.GetShortUrl(It.IsAny<int>())).Returns(shortUrlToUse);

      var shortUrlFactory = new ShortUrlFactory(
        dateTimeProviderMock.Object,
        entityIdProviderMock.Object,
        shortUrlProviderMock.Object);

      ShortUrl actualShortUrl = shortUrlFactory.Create(longUrl, testSequenceId, shortUrl);
      Assert.Multiple(() =>
      {
        Assert.That(actualShortUrl.Id, Is.EqualTo(testId));
        Assert.That(actualShortUrl.LongUrl, Is.EqualTo(longUrl));
        Assert.That(actualShortUrl.Url, Is.EqualTo(shortUrlToUse));
        Assert.That(actualShortUrl.Created, Is.EqualTo(testCreated));
      });
      dateTimeProviderMock.Verify(x => x.GetNow(), invocationsForDateTimeProvider);
      entityIdProviderMock.Verify(x => x.NewId(), invocationsForEntityIdProvider);
      shortUrlProviderMock.Verify(x => x.GetShortUrl(It.IsAny<int>()), invocationsForShortUrlProvider);
      if (testSequenceId.HasValue)
      {
        shortUrlProviderMock.Verify(x => x.GetShortUrl(testSequenceId.Value), invocationsForShortUrlProvider);
      }
    }

    #endregion Public Methods
  }
}