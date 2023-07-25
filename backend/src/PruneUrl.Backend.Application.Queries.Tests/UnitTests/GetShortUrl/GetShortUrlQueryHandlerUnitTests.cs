using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Exceptions.Database;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Application.Queries.GetShortUrl;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetShortUrl
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetShortUrlQueryHandlerUnitTests
  {
    #region Public Methods

    [Test]
    public async Task GetShortUrlTest_EntityFound()
    {
      const string testShortUrl = "hsfwgss";
      const int testSequenceId = 233243;
      var dbGetByIdOperationMock = new Mock<IDbGetByIdOperation<ShortUrl>>();
      var sequenceIdProviderMock = new Mock<ISequenceIdProvider>();
      ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl();
      var query = new GetShortUrlQuery(testShortUrl);
      var cancellationToken = CancellationToken.None;

      dbGetByIdOperationMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(testShortUrlEntity);
      sequenceIdProviderMock.Setup(x => x.GetSequenceId(It.IsAny<string>())).Returns(testSequenceId);

      var handler = new GetShortUrlQueryHandler(dbGetByIdOperationMock.Object, sequenceIdProviderMock.Object);
      GetShortUrlQueryResponse response = await handler.Handle(query, cancellationToken);
      Assert.That(response.ShortUrl, Is.EqualTo(testShortUrlEntity));
      dbGetByIdOperationMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      dbGetByIdOperationMock.Verify(x => x.GetByIdAsync(testSequenceId.ToString(), cancellationToken), Times.Once);
      sequenceIdProviderMock.Verify(x => x.GetSequenceId(It.IsAny<string>()), Times.Once);
      sequenceIdProviderMock.Verify(x => x.GetSequenceId(testShortUrl), Times.Once);
    }

    [Test]
    public void GetShortUrlTest_EntityNotFound()
    {
      const string testShortUrl = "hsfwgss";
      const int testSequenceId = 233243;
      var dbGetByIdOperationMock = new Mock<IDbGetByIdOperation<ShortUrl>>();
      var sequenceIdProviderMock = new Mock<ISequenceIdProvider>();
      ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl();
      var query = new GetShortUrlQuery(testShortUrl);
      var cancellationToken = CancellationToken.None;

      sequenceIdProviderMock.Setup(x => x.GetSequenceId(It.IsAny<string>())).Returns(testSequenceId);

      var handler = new GetShortUrlQueryHandler(dbGetByIdOperationMock.Object, sequenceIdProviderMock.Object);
      Assert.That(async () => await handler.Handle(query, cancellationToken), Throws.TypeOf<EntityNotFoundException>().With.Message.EqualTo($"Entity of type {typeof(ShortUrl)} with id {testSequenceId} was not found!"));
      dbGetByIdOperationMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      dbGetByIdOperationMock.Verify(x => x.GetByIdAsync(testSequenceId.ToString(), cancellationToken), Times.Once);
      sequenceIdProviderMock.Verify(x => x.GetSequenceId(It.IsAny<string>()), Times.Once);
      sequenceIdProviderMock.Verify(x => x.GetSequenceId(testShortUrl), Times.Once);
    }

    #endregion Public Methods
  }
}