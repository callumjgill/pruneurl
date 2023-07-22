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
  public sealed class FirestoreDbWriteBatchAdapterUnitTests
  {
    #region Public Methods

    [Test]
    public async Task CommitAsyncTest()
    {
      var cancellationToken = new CancellationToken();
      var adapteeDbWriteBatchMock = new Mock<IDbWriteBatch<FirestoreEntityDTO>>();
      var adapterFirestoreDbWriteBatch = new FirestoreDbWriteBatchAdapter<IEntity, FirestoreEntityDTO>(adapteeDbWriteBatchMock.Object, Mock.Of<IMapper>());

      await adapterFirestoreDbWriteBatch.CommitAsync(cancellationToken);
      adapteeDbWriteBatchMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbWriteBatchMock.Verify(x => x.CommitAsync(cancellationToken), Times.Once);
    }

    [Test]
    public void CreateTest()
    {
      var testEntity = Mock.Of<IEntity>();
      var adapteeDbWriteBatchMock = new Mock<IDbWriteBatch<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();
      var adapterFirestoreDbWriteBatch = new FirestoreDbWriteBatchAdapter<IEntity, FirestoreEntityDTO>(adapteeDbWriteBatchMock.Object, mapperMock.Object);
      var testFirestoreEntity = Mock.Of<FirestoreEntityDTO>();

      mapperMock.Setup(x => x.Map<IEntity, FirestoreEntityDTO>(It.IsAny<IEntity>())).Returns(testFirestoreEntity);

      adapterFirestoreDbWriteBatch.Create(testEntity);
      adapteeDbWriteBatchMock.Verify(x => x.Create(It.IsAny<FirestoreEntityDTO>()), Times.Once);
      adapteeDbWriteBatchMock.Verify(x => x.Create(testFirestoreEntity), Times.Once);
      mapperMock.Verify(x => x.Map<IEntity, FirestoreEntityDTO>(It.IsAny<IEntity>()), Times.Once);
      mapperMock.Verify(x => x.Map<IEntity, FirestoreEntityDTO>(testEntity), Times.Once);
    }

    [Test]
    public void DeleteTest()
    {
      var testId = Guid.NewGuid().ToString();
      var adapteeDbWriteBatchMock = new Mock<IDbWriteBatch<FirestoreEntityDTO>>();
      var adapterFirestoreDbWriteBatch = new FirestoreDbWriteBatchAdapter<IEntity, FirestoreEntityDTO>(adapteeDbWriteBatchMock.Object, Mock.Of<IMapper>());

      adapterFirestoreDbWriteBatch.Delete(testId);
      adapteeDbWriteBatchMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
      adapteeDbWriteBatchMock.Verify(x => x.Delete(testId), Times.Once);
    }

    #endregion Public Methods
  }
}