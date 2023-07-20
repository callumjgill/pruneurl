using AutoMapper;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.DbTransaction
{
  [TestFixture]
  [Parallelizable]
  public sealed class FirestoreDbTransactionAdapterUnitTests
  {
    #region Public Methods

    [Test]
    public async Task CommitAsyncTest()
    {
      var cancellationToken = new CancellationToken();
      var adapteeDbTransactionMock = new Mock<IDbTransaction<FirestoreEntityDTO>>();
      var adapterFirestoreDbTransaction = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(adapteeDbTransactionMock.Object, Mock.Of<IMapper>());

      await adapterFirestoreDbTransaction.CommitAsync(cancellationToken);
      adapteeDbTransactionMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbTransactionMock.Verify(x => x.CommitAsync(cancellationToken), Times.Once);
    }

    [Test]
    public void CreateTest()
    {
      var testEntity = Mock.Of<IEntity>();
      var adapteeDbTransactionMock = new Mock<IDbTransaction<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();
      var adapterFirestoreDbTransaction = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(adapteeDbTransactionMock.Object, mapperMock.Object);
      var testFirestoreEntity = Mock.Of<FirestoreEntityDTO>();

      adapteeDbTransactionMock.Setup(x => x.Create(It.IsAny<FirestoreEntityDTO>())).Returns(adapteeDbTransactionMock.Object);
      mapperMock.Setup(x => x.Map<IEntity, FirestoreEntityDTO>(It.IsAny<IEntity>())).Returns(testFirestoreEntity);

      IDbTransaction<IEntity> transaction = adapterFirestoreDbTransaction.Create(testEntity);
      Assert.That(transaction, Is.EqualTo(adapterFirestoreDbTransaction));
      adapteeDbTransactionMock.Verify(x => x.Create(It.IsAny<FirestoreEntityDTO>()), Times.Once);
      adapteeDbTransactionMock.Verify(x => x.Create(testFirestoreEntity), Times.Once);
      mapperMock.Verify(x => x.Map<IEntity, FirestoreEntityDTO>(It.IsAny<IEntity>()), Times.Once);
      mapperMock.Verify(x => x.Map<IEntity, FirestoreEntityDTO>(testEntity), Times.Once);
    }

    [Test]
    public void DeleteTest()
    {
      var testId = Guid.NewGuid().ToString();
      var adapteeDbTransactionMock = new Mock<IDbTransaction<FirestoreEntityDTO>>();
      var adapterFirestoreDbTransaction = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(adapteeDbTransactionMock.Object, Mock.Of<IMapper>());

      IDbTransaction<IEntity> transaction = adapterFirestoreDbTransaction.Delete(testId);
      Assert.That(transaction, Is.EqualTo(adapterFirestoreDbTransaction));
      adapteeDbTransactionMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
      adapteeDbTransactionMock.Verify(x => x.Delete(testId), Times.Once);
    }

    #endregion Public Methods
  }
}