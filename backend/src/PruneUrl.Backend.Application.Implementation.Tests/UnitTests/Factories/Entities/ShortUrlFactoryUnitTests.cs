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
      string testId = Guid.NewGuid().ToString();
      DateTime testCreated = DateTime.Now;
      string shortUrlToUse = "testing123";

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

      ShortUrl actualShortUrl = shortUrlFactory.Create(longUrl, testSequenceId);
      Assert.Multiple(() =>
      {
        Assert.That(actualShortUrl.Id, Is.EqualTo(testId));
        Assert.That(actualShortUrl.LongUrl, Is.EqualTo(longUrl));
        Assert.That(actualShortUrl.Url, Is.EqualTo(shortUrlToUse));
        Assert.That(actualShortUrl.Created, Is.EqualTo(testCreated));
      });
      dateTimeProviderMock.Verify(x => x.GetNow(), Times.Once);
      entityIdProviderMock.Verify(x => x.NewId(), Times.Once);
      shortUrlProviderMock.Verify(x => x.GetShortUrl(It.IsAny<int>()), Times.Once);
      shortUrlProviderMock.Verify(x => x.GetShortUrl(testSequenceId), Times.Once);
    }

    #endregion Public Methods
  }
}