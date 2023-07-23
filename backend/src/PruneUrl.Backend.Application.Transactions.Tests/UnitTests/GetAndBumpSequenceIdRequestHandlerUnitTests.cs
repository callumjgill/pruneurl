using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Configuration.Entities.SequenceId;
using PruneUrl.Backend.Application.Exceptions.Database;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Transactions.Tests.UnitTests
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetAndBumpSequenceIdRequestHandlerUnitTests
  {
    #region Public Methods

    [Test]
    public void HandleTest_Fail()
    {
      string testId = Guid.NewGuid().ToString();
      var dbTransactionProviderMock = new Mock<IDbTransactionProvider>();
      var sequenceIdOptionsMock = new Mock<IOptions<SequenceIdOptions>>();

      SequenceId testSequenceId = EntityTestHelper.CreateSequenceId(testId, 1254);

      sequenceIdOptionsMock.Setup(x => x.Value).Returns(new SequenceIdOptions() { Id = testId });
      dbTransactionProviderMock.Setup(x => x.RunTransactionAsync(It.IsAny<Func<IDbTransaction<SequenceId>, Task<SequenceId>>>(), It.IsAny<CancellationToken>()))
                               .Returns((Func<IDbTransaction<SequenceId>, Task<SequenceId>> callback, CancellationToken _) => callback(Mock.Of<IDbTransaction<SequenceId>>()));

      CancellationToken cancellationToken = CancellationToken.None;
      var requestHandler = new GetAndBumpSequenceIdRequestHandler(dbTransactionProviderMock.Object, sequenceIdOptionsMock.Object, Mock.Of<ISequenceIdFactory>());
      Assert.That(async () => await requestHandler.Handle(new GetAndBumpSequenceIdRequest(), cancellationToken), Throws.TypeOf<EntityNotFoundException>().With.Message.EqualTo($"Entity of type {typeof(SequenceId)} with id {testId} was not found!"));
      sequenceIdOptionsMock.Verify(x => x.Value, Times.Once);
    }

    [Test]
    public async Task HandleTest_Success()
    {
      string testId = Guid.NewGuid().ToString();
      var dbTransactionProviderMock = new Mock<IDbTransactionProvider>();
      var sequenceIdFactoryMock = new Mock<ISequenceIdFactory>();
      var sequenceIdOptionsMock = new Mock<IOptions<SequenceIdOptions>>();

      SequenceId testSequenceId = EntityTestHelper.CreateSequenceId(testId, 1254);
      var dbTransactionMock = new Mock<IDbTransaction<SequenceId>>();
      SequenceId nextSequenceId = EntityTestHelper.CreateSequenceId(testId, testSequenceId.Value + 1);

      sequenceIdOptionsMock.Setup(x => x.Value).Returns(new SequenceIdOptions() { Id = testId });
      dbTransactionMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(testSequenceId);
      dbTransactionProviderMock.Setup(x => x.RunTransactionAsync(It.IsAny<Func<IDbTransaction<SequenceId>, Task<SequenceId>>>(), It.IsAny<CancellationToken>()))
                               .Returns((Func<IDbTransaction<SequenceId>, Task<SequenceId>> callback, CancellationToken _) => callback(dbTransactionMock.Object));
      sequenceIdFactoryMock.Setup(x => x.CreateFromExisting(It.IsAny<SequenceId>(), It.IsAny<int>())).Returns(nextSequenceId);

      CancellationToken cancellationToken = CancellationToken.None;
      var requestHandler = new GetAndBumpSequenceIdRequestHandler(dbTransactionProviderMock.Object, sequenceIdOptionsMock.Object, sequenceIdFactoryMock.Object);
      GetAndBumpSequenceIdResponse response = await requestHandler.Handle(new GetAndBumpSequenceIdRequest(), cancellationToken);
      Assert.That(response.SequenceId, Is.EqualTo(testSequenceId));
      dbTransactionMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      dbTransactionMock.Verify(x => x.GetByIdAsync(testId, cancellationToken), Times.Once);
      dbTransactionMock.Verify(x => x.Update(It.IsAny<SequenceId>()), Times.Once);
      dbTransactionMock.Verify(x => x.Update(nextSequenceId), Times.Once);
      sequenceIdFactoryMock.Verify(x => x.CreateFromExisting(It.IsAny<SequenceId>(), It.IsAny<int>()), Times.Once);
      sequenceIdFactoryMock.Verify(x => x.CreateFromExisting(testSequenceId, testSequenceId.Value + 1), Times.Once);
      sequenceIdOptionsMock.Verify(x => x.Value, Times.Once);
    }

    #endregion Public Methods
  }
}