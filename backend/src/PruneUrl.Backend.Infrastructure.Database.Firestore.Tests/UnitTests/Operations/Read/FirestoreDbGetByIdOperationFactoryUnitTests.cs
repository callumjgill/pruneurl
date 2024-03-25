using AutoMapper;
using Google.Cloud.Firestore;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests;

[TestFixture]
[Parallelizable]
[Description(
  "These tests don't require communicating with the FirestoreDb emulator, so they can be ran in parallel with each other and other test fixtures."
)]
public sealed class FirestoreDbGetByIdOperationFactoryUnitTests
{
  [Test]
  public void CreateTest_Invalid()
  {
    FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    var factory = new FirestoreDbGetByIdOperationFactory(
      testFirestoreDb,
      Substitute.For<IMapper>()
    );
    Assert.That(
      factory.Create<IEntity>,
      Throws
        .TypeOf<InvalidEntityTypeMapException>()
        .With.Message.EqualTo($"No mapping exists for the type {typeof(IEntity)}!")
    );
  }

  [Test]
  public void CreateTest_Valid_SequenceId()
  {
    CreateTest<SequenceId, SequenceIdDTO>();
  }

  [Test]
  public void CreateTest_Valid_ShortUrl()
  {
    CreateTest<ShortUrl, ShortUrlDTO>();
  }

  private void CreateTest<TEntity, TFirestoreEntity>()
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    var factory = new FirestoreDbGetByIdOperationFactory(
      testFirestoreDb,
      Substitute.For<IMapper>()
    );
    IDbGetByIdOperation<TEntity> actualDbGetByIdOperation = factory.Create<TEntity>();
    Assert.That(
      actualDbGetByIdOperation,
      Is.TypeOf<FirestoreDbGetByIdOperationAdapter<TEntity, TFirestoreEntity>>()
    );
  }
}
