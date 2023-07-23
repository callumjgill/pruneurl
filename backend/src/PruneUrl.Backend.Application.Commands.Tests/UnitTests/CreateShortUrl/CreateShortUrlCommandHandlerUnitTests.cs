using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands.CreateShortUrl;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Commands.Tests.UnitTests.CreateShortUrl
{
  [TestFixture]
  [Parallelizable]
  public sealed class CreateShortUrlCommandHandlerUnitTests
  {
    #region Public Methods

    [Test]
    public async Task HandleTest()
    {
      string testLongUrl = "https://www.youtube.com";
      int testSequenceId = 6124323;
      var dbWriteBatchMock = new Mock<IDbWriteBatch<ShortUrl>>();
      var shortUrlFactoryMock = new Mock<IShortUrlFactory>();
      ShortUrl testShortUrl = EntityTestHelper.CreateShortUrl();
      CancellationToken cancellationToken = CancellationToken.None;

      shortUrlFactoryMock.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<int>())).Returns(testShortUrl);
      dbWriteBatchMock.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

      var handler = new CreateShortUrlCommandHandler(dbWriteBatchMock.Object, shortUrlFactoryMock.Object);
      var command = new CreateShortUrlCommand(testLongUrl, testSequenceId);

      await handler.Handle(command, cancellationToken);

      shortUrlFactoryMock.Verify(x => x.Create(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
      shortUrlFactoryMock.Verify(x => x.Create(testLongUrl, testSequenceId), Times.Once);
      dbWriteBatchMock.Verify(x => x.Create(It.IsAny<ShortUrl>()), Times.Once);
      dbWriteBatchMock.Verify(x => x.Create(testShortUrl), Times.Once);
      dbWriteBatchMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
      dbWriteBatchMock.Verify(x => x.CommitAsync(cancellationToken), Times.Once);
    }

    #endregion Public Methods
  }
}