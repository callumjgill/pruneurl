using AutoMapper;
using Google.Cloud.Firestore;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests;

[TestFixture]
[Parallelizable]
public sealed class FirestoreDbTransactionFactoryUnitTests
{
  [Test]
  public void CreateTest_Invalid()
  {
    FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    var factory = new FirestoreDbWriteBatchFactory(testFirestoreDb, Substitute.For<IMapper>());
    Assert.That(
      factory.Create<IEntity>,
      Throws
        .TypeOf<InvalidEntityTypeMapException>()
        .With.Message.EqualTo($"No mapping exists for the type {typeof(IEntity)}!")
    );
  }

  [Test]
  public Task CreateTest_Valid_SequenceId()
  {
    return CreateTest<SequenceId, SequenceIdDTO>();
  }

  [Test]
  public Task CreateTest_Valid_ShortUrl()
  {
    return CreateTest<ShortUrl, ShortUrlDTO>();
  }

  private async Task CreateTest<TEntity, TFirestoreEntity>()
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    var factory = new FirestoreDbTransactionFactory(testFirestoreDb, Substitute.For<IMapper>());
    await testFirestoreDb.RunTransactionAsync(transaction =>
    {
      IDbTransaction<TEntity> actualDbTransaction = factory.Create<TEntity>(transaction);
      Assert.That(
        actualDbTransaction,
        Is.TypeOf<FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity>>()
      );
      return Task.CompletedTask;
    });
  }
}
