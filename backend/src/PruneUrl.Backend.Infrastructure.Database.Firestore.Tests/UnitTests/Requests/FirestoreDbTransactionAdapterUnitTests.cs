using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Requests;

[TestFixture]
[Parallelizable]
public sealed class FirestoreDbTransactionAdapterUnitTests
{
  [Test]
  public async Task GetByIdAsyncTest_ReturnsMappedEntityIfAdapteeReturnsEntity()
  {
    string testId = Guid.NewGuid().ToString();
    var adapteeDbTransaction = Substitute.For<IDbTransaction<FirestoreEntityDTO>>();
    var mapper = Substitute.For<IMapper>();
    var testFirestoreEntity = Substitute.For<FirestoreEntityDTO>();
    var expectedEntity = Substitute.For<IEntity>();

    adapteeDbTransaction
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
      .Returns(testFirestoreEntity);
    mapper.Map<FirestoreEntityDTO, IEntity>(Arg.Any<FirestoreEntityDTO>()).Returns(expectedEntity);

    var adapter = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(
      adapteeDbTransaction,
      mapper
    );
    IEntity? actualEntity = await adapter.GetByIdAsync(testId);
    Assert.That(actualEntity, Is.EqualTo(expectedEntity));
    await adapteeDbTransaction
      .Received(1)
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    await adapteeDbTransaction.Received(1).GetByIdAsync(testId, default);
    mapper.Received(1).Map<FirestoreEntityDTO, IEntity>(Arg.Any<FirestoreEntityDTO>());
    mapper.Received(1).Map<FirestoreEntityDTO, IEntity>(testFirestoreEntity);
  }

  [Test]
  public async Task GetByIdAsyncTest_ReturnsNullIfAdapteeReturnsNull()
  {
    string testId = Guid.NewGuid().ToString();
    var adapteeDbTransaction = Substitute.For<IDbTransaction<FirestoreEntityDTO>>();
    var mapper = Substitute.For<IMapper>();

    var adapter = new FirestoreDbTransactionAdapter<IEntity, FirestoreEntityDTO>(
      adapteeDbTransaction,
      mapper
    );
    IEntity? actualEntity = await adapter.GetByIdAsync(testId);
    Assert.That(actualEntity, Is.Null);
    await adapteeDbTransaction
      .Received(1)
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    await adapteeDbTransaction.Received(1).GetByIdAsync(testId, default);
  }

  [Test]
  public void UpdateTest()
  {
    var testEntity = Substitute.For<IEntity>();
    var adapteeDbTransaction = Substitute.For<IDbTransaction<FirestoreEntityDTO>>();
    var mapper = Substitute.For<IMapper>();
    var adapterFirestoreDbTransaction = new FirestoreDbTransactionAdapter<
      IEntity,
      FirestoreEntityDTO
    >(adapteeDbTransaction, mapper);
    var testFirestoreEntity = Substitute.For<FirestoreEntityDTO>();

    mapper.Map<IEntity, FirestoreEntityDTO>(Arg.Any<IEntity>()).Returns(testFirestoreEntity);

    adapterFirestoreDbTransaction.Update(testEntity);
    adapteeDbTransaction.Received(1).Update(Arg.Any<FirestoreEntityDTO>());
    adapteeDbTransaction.Received(1).Update(testFirestoreEntity);
    mapper.Received(1).Map<IEntity, FirestoreEntityDTO>(Arg.Any<IEntity>());
    mapper.Received(1).Map<IEntity, FirestoreEntityDTO>(testEntity);
  }
}
