using AutoMapper;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Operations.Read
{
  [TestFixture]
  [Parallelizable]
  public sealed class DbGetByIdOperationAdapterUnitTests
  {
    #region Public Methods

    [Test]
    public async Task GetByIdAsyncTest_ReturnsMappedEntityIfAdapteeReturnsEntity()
    {
      string testId = Guid.NewGuid().ToString();
      var adapteeDbGetByIdOperationMock = new Mock<IDbGetByIdOperation<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();
      var testFirestoreEntity = Mock.Of<FirestoreEntityDTO>();
      var expectedEntity = Mock.Of<IEntity>();

      adapteeDbGetByIdOperationMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(testFirestoreEntity);
      mapperMock.Setup(x => x.Map<FirestoreEntityDTO, IEntity>(It.IsAny<FirestoreEntityDTO>())).Returns(expectedEntity);

      var adapter = new FirestoreDbGetByIdOperationAdapter<IEntity, FirestoreEntityDTO>(adapteeDbGetByIdOperationMock.Object, mapperMock.Object);
      IEntity? actualEntity = await adapter.GetByIdAsync(testId);
      Assert.That(actualEntity, Is.EqualTo(expectedEntity));
      adapteeDbGetByIdOperationMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbGetByIdOperationMock.Verify(x => x.GetByIdAsync(testId, default), Times.Once);
      mapperMock.Verify(x => x.Map<FirestoreEntityDTO, IEntity>(It.IsAny<FirestoreEntityDTO>()), Times.Once);
      mapperMock.Verify(x => x.Map<FirestoreEntityDTO, IEntity>(testFirestoreEntity), Times.Once);
    }

    [Test]
    public async Task GetByIdAsyncTest_ReturnsNullIfAdapteeReturnsNull()
    {
      string testId = Guid.NewGuid().ToString();
      var adapteeDbGetByIdOperationMock = new Mock<IDbGetByIdOperation<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();

      var adapter = new FirestoreDbGetByIdOperationAdapter<IEntity, FirestoreEntityDTO>(adapteeDbGetByIdOperationMock.Object, mapperMock.Object);
      IEntity? actualEntity = await adapter.GetByIdAsync(testId);
      Assert.That(actualEntity, Is.Null);
      adapteeDbGetByIdOperationMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbGetByIdOperationMock.Verify(x => x.GetByIdAsync(testId, default), Times.Once);
    }

    #endregion Public Methods
  }
}