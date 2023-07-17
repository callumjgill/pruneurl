using Google.Cloud.Firestore;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.UnitTests.DbTransaction
{
  [TestFixture]
  [Parallelizable]
  [Description("These tests don't require communicating with the FirestoreDb emulator, so they can be ran in parallel with each other and other test fixtures.")]
  public sealed class FirestoreDbTransactionFactoryUnitTests
  {
    #region Public Methods

    [TestCase<IEntity>]
    [TestCase<ShortUrl>]
    [TestCase<SequenceId>]
    public void CreateTest<T>() where T : IEntity
    {
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var factory = new FirestoreDbTransactionFactory(testFirestoreDb);
      IDbTransaction<T> actualTransaction = factory.Create<T>();
      Assert.That(actualTransaction, Is.TypeOf<FirestoreDbTransaction<T>>());
    }

    #endregion Public Methods
  }
}