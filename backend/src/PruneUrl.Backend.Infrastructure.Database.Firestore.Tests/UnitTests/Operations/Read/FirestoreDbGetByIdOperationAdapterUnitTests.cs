using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Operations.Read;

[TestFixture]
[Parallelizable]
public sealed class DbGetByIdOperationAdapterUnitTests
{
  [Test]
  public async Task GetByIdAsyncTest_ReturnsMappedEntityIfAdapteeReturnsEntity()
  {
    string testId = Guid.NewGuid().ToString();
    var adapteeDbGetByIdOperation = Substitute.For<IDbGetByIdOperation<FirestoreEntityDTO>>();
    var mapper = Substitute.For<IMapper>();
    var testFirestoreEntity = Substitute.For<FirestoreEntityDTO>();
    var expectedEntity = Substitute.For<IEntity>();

    adapteeDbGetByIdOperation
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
      .Returns(testFirestoreEntity);
    mapper.Map<FirestoreEntityDTO, IEntity>(Arg.Any<FirestoreEntityDTO>()).Returns(expectedEntity);

    var adapter = new FirestoreDbGetByIdOperationAdapter<IEntity, FirestoreEntityDTO>(
      adapteeDbGetByIdOperation,
      mapper
    );
    IEntity? actualEntity = await adapter.GetByIdAsync(testId);
    Assert.That(actualEntity, Is.EqualTo(expectedEntity));
    await adapteeDbGetByIdOperation
      .Received(1)
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    await adapteeDbGetByIdOperation.Received(1).GetByIdAsync(testId, default);
    mapper.Received(1).Map<FirestoreEntityDTO, IEntity>(Arg.Any<FirestoreEntityDTO>());
    mapper.Received(1).Map<FirestoreEntityDTO, IEntity>(testFirestoreEntity);
  }

  [Test]
  public async Task GetByIdAsyncTest_ReturnsNullIfAdapteeReturnsNull()
  {
    string testId = Guid.NewGuid().ToString();
    var adapteeDbGetByIdOperation = Substitute.For<IDbGetByIdOperation<FirestoreEntityDTO>>();
    var mapper = Substitute.For<IMapper>();

    var adapter = new FirestoreDbGetByIdOperationAdapter<IEntity, FirestoreEntityDTO>(
      adapteeDbGetByIdOperation,
      mapper
    );
    IEntity? actualEntity = await adapter.GetByIdAsync(testId);
    Assert.That(actualEntity, Is.Null);
    await adapteeDbGetByIdOperation
      .Received(1)
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    await adapteeDbGetByIdOperation.Received(1).GetByIdAsync(testId, default);
  }
}
