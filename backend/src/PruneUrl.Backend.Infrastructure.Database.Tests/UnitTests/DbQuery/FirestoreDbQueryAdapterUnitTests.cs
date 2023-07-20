using AutoMapper;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.DbQuery;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DbQuery;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.DbQuery
{
  [TestFixture]
  [Parallelizable]
  public sealed class FirestoreDbQueryAdapterUnitTests
  {
    #region Public Methods

    [Test]
    public async Task GetByIdAsyncTest_ReturnsMappedEntityIfAdapteeReturnsEntity()
    {
      string testId = Guid.NewGuid().ToString();
      var adapteeDbQueryMock = new Mock<IDbQuery<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();
      var testFirestoreEntity = Mock.Of<FirestoreEntityDTO>();
      var expectedEntity = Mock.Of<IEntity>();

      adapteeDbQueryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(testFirestoreEntity);
      mapperMock.Setup(x => x.Map<FirestoreEntityDTO, IEntity>(It.IsAny<FirestoreEntityDTO>())).Returns(expectedEntity);

      var adapter = new FirestoreDbQueryAdapter<IEntity, FirestoreEntityDTO>(adapteeDbQueryMock.Object, mapperMock.Object);
      IEntity? actualEntity = await adapter.GetByIdAsync(testId);
      Assert.That(actualEntity, Is.EqualTo(expectedEntity));
      adapteeDbQueryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbQueryMock.Verify(x => x.GetByIdAsync(testId, default), Times.Once);
      mapperMock.Verify(x => x.Map<FirestoreEntityDTO, IEntity>(It.IsAny<FirestoreEntityDTO>()), Times.Once);
      mapperMock.Verify(x => x.Map<FirestoreEntityDTO, IEntity>(testFirestoreEntity), Times.Once);
    }

    [Test]
    public async Task GetByIdAsyncTest_ReturnsNullIfAdapteeReturnsNull()
    {
      string testId = Guid.NewGuid().ToString();
      var adapteeDbQueryMock = new Mock<IDbQuery<FirestoreEntityDTO>>();
      var mapperMock = new Mock<IMapper>();

      var adapter = new FirestoreDbQueryAdapter<IEntity, FirestoreEntityDTO>(adapteeDbQueryMock.Object, mapperMock.Object);
      IEntity? actualEntity = await adapter.GetByIdAsync(testId);
      Assert.That(actualEntity, Is.Null);
      adapteeDbQueryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
      adapteeDbQueryMock.Verify(x => x.GetByIdAsync(testId, default), Times.Once);
    }

    #endregion Public Methods
  }
}