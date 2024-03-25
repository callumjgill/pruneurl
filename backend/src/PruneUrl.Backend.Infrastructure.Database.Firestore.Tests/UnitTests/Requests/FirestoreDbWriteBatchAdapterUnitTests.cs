using AutoMapper;
using NSubstitute;
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
    [Test]
    public async Task CommitAsyncTest()
    {
      var cancellationToken = new CancellationToken();
      var adapteeDbWriteBatch = Substitute.For<IDbWriteBatch<FirestoreEntityDTO>>();
      var adapterFirestoreDbWriteBatch = new FirestoreDbWriteBatchAdapter<
        IEntity,
        FirestoreEntityDTO
      >(adapteeDbWriteBatch, Substitute.For<IMapper>());

      await adapterFirestoreDbWriteBatch.CommitAsync(cancellationToken);
      await adapteeDbWriteBatch.Received(1).CommitAsync(Arg.Any<CancellationToken>());
      await adapteeDbWriteBatch.Received(1).CommitAsync(cancellationToken);
    }

    [Test]
    public void CreateTest()
    {
      var testEntity = Substitute.For<IEntity>();
      var adapteeDbWriteBatch = Substitute.For<IDbWriteBatch<FirestoreEntityDTO>>();
      var mapper = Substitute.For<IMapper>();
      var adapterFirestoreDbWriteBatch = new FirestoreDbWriteBatchAdapter<
        IEntity,
        FirestoreEntityDTO
      >(adapteeDbWriteBatch, mapper);
      var testFirestoreEntity = Substitute.For<FirestoreEntityDTO>();

      mapper.Map<IEntity, FirestoreEntityDTO>(Arg.Any<IEntity>()).Returns(testFirestoreEntity);

      adapterFirestoreDbWriteBatch.Create(testEntity);
      adapteeDbWriteBatch.Received(1).Create(Arg.Any<FirestoreEntityDTO>());
      adapteeDbWriteBatch.Received(1).Create(testFirestoreEntity);
      mapper.Received(1).Map<IEntity, FirestoreEntityDTO>(Arg.Any<IEntity>());
      mapper.Received(1).Map<IEntity, FirestoreEntityDTO>(testEntity);
    }

    [Test]
    public void DeleteTest()
    {
      var testId = Guid.NewGuid().ToString();
      var adapteeDbWriteBatch = Substitute.For<IDbWriteBatch<FirestoreEntityDTO>>();
      var adapterFirestoreDbWriteBatch = new FirestoreDbWriteBatchAdapter<
        IEntity,
        FirestoreEntityDTO
      >(adapteeDbWriteBatch, Substitute.For<IMapper>());

      adapterFirestoreDbWriteBatch.Delete(testId);
      adapteeDbWriteBatch.Received(1).Delete(Arg.Any<string>());
      adapteeDbWriteBatch.Received(1).Delete(testId);
    }
  }
}
