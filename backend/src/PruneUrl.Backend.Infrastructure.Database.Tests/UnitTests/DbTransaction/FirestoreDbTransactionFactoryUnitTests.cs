﻿using AutoMapper;
using Google.Cloud.Firestore;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.UnitTests.DbTransaction
{
  [TestFixture]
  [Parallelizable]
  [Description("These tests don't require communicating with the FirestoreDb emulator, so they can be ran in parallel with each other and other test fixtures.")]
  public sealed class FirestoreDbTransactionFactoryUnitTests
  {
    #region Public Methods

    [Test]
    public void CreateTest_Invalid()
    {
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var factory = new FirestoreDbTransactionFactory(testFirestoreDb, Mock.Of<IMapper>());
      Assert.That(factory.Create<IEntity>, Throws.TypeOf<InvalidEntityTypeMapException>().With.Message.EqualTo($"No mapping exists for the type {typeof(IEntity)}!"));
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

    #endregion Public Methods

    #region Private Methods

    private void CreateTest<TEntity, TFirestoreEntity>()
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var factory = new FirestoreDbTransactionFactory(testFirestoreDb, Mock.Of<IMapper>());
      IDbTransaction<TEntity> actualTransaction = factory.Create<TEntity>();
      Assert.That(actualTransaction, Is.TypeOf<FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity>>());
    }

    #endregion Private Methods
  }
}