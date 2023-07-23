using AutoMapper;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Requests
{
  [TestFixture]
  [Parallelizable]
  public sealed class FirestoreDbTransactionAdapterUnitTests
  {
    #region Public Methods

    [Test]
    public async Task GetByIdAsyncTest_ReturnsMappedEntityIfAdapteeReturnsEntity()
    {
      string testId = Guid.NewGuid().ToString();
      var adapteeDbTransactionMock = new Mock<IDbTransaction<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();
      var testFirestoreEntity = Mock.Of<FirestoreEntityDTO>();
      var expectedEntity = Mock.Of<IEntity>();

      adapteeDbTransactionMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(testFirestoreEntity);
      mapperMock.Setup(x => x.Map<FirestoreEntityDTO, IEntity>(It.IsAny<FirestoreEntityDTO>())).Returns(expectedEntity);

      var adapter = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(adapteeDbTransactionMock.Object, mapperMock.Object);
      IEntity? actualEntity = await adapter.GetByIdAsync(testId);
      Assert.That(actualEntity, Is.EqualTo(expectedEntity));
      adapteeDbTransactionMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbTransactionMock.Verify(x => x.GetByIdAsync(testId, default), Times.Once);
      mapperMock.Verify(x => x.Map<FirestoreEntityDTO, IEntity>(It.IsAny<FirestoreEntityDTO>()), Times.Once);
      mapperMock.Verify(x => x.Map<FirestoreEntityDTO, IEntity>(testFirestoreEntity), Times.Once);
    }

    [Test]
    public async Task GetByIdAsyncTest_ReturnsNullIfAdapteeReturnsNull()
    {
      string testId = Guid.NewGuid().ToString();
      var adapteeDbTransactionMock = new Mock<IDbTransaction<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();

      var adapter = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(adapteeDbTransactionMock.Object, mapperMock.Object);
      IEntity? actualEntity = await adapter.GetByIdAsync(testId);
      Assert.That(actualEntity, Is.Null);
      adapteeDbTransactionMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbTransactionMock.Verify(x => x.GetByIdAsync(testId, default), Times.Once);
    }

    [Test]
    public void UpdateTest()
    {
      var testEntity = Mock.Of<IEntity>();
      var adapteeDbTransactionMock = new Mock<IDbTransaction<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();
      var adapterFirestoreDbTransaction = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(adapteeDbTransactionMock.Object, mapperMock.Object);
      var testFirestoreEntity = Mock.Of<FirestoreEntityDTO>();

      mapperMock.Setup(x => x.Map<IEntity, FirestoreEntityDTO>(It.IsAny<IEntity>())).Returns(testFirestoreEntity);

      adapterFirestoreDbTransaction.Update(testEntity);
      adapteeDbTransactionMock.Verify(x => x.Update(It.IsAny<FirestoreEntityDTO>()), Times.Once);
      adapteeDbTransactionMock.Verify(x => x.Update(testFirestoreEntity), Times.Once);
      mapperMock.Verify(x => x.Map<IEntity, FirestoreEntityDTO>(It.IsAny<IEntity>()), Times.Once);
      mapperMock.Verify(x => x.Map<IEntity, FirestoreEntityDTO>(testEntity), Times.Once);
    }

    #endregion Public Methods
  }
}