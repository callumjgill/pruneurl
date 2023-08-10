using AutoMapper;
using Google.Cloud.Firestore;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Requests
{
  [TestFixture]
  [Parallelizable]
  public sealed class FirestoreDbTransactionFactoryUnitTests
  {
    #region Public Methods

    [Test]
    public void CreateTest_Invalid()
    {
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var factory = new FirestoreDbWriteBatchFactory(testFirestoreDb, Substitute.For<IMapper>());
      Assert.That(factory.Create<IEntity>, Throws.TypeOf<InvalidEntityTypeMapException>().With.Message.EqualTo($"No mapping exists for the type {typeof(IEntity)}!"));
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

    #endregion Public Methods

    #region Private Methods

    private async Task CreateTest<TEntity, TFirestoreEntity>()
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var factory = new FirestoreDbTransactionFactory(testFirestoreDb, Substitute.For<IMapper>());
      await testFirestoreDb.RunTransactionAsync(transaction =>
      {
        IDbTransaction<TEntity> actualDbTransaction = factory.Create<TEntity>(transaction);
        Assert.That(actualDbTransaction, Is.TypeOf<FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity>>());
        return Task.CompletedTask;
      });
    }

    #endregion Private Methods
  }
}