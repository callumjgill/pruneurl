using Google.Cloud.Firestore;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.DbQuery;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DbQuery;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.UnitTests.DbQuery
{
  [TestFixture]
  [Parallelizable]
  [Description("These tests don't require communicating with the FirestoreDb emulator, so they can be ran in parallel with each other and other test fixtures.")]
  public sealed class FirestoreDbQueryFactoryUnitTests
  {
    #region Public Methods

    [TestCase<IEntity>]
    [TestCase<ShortUrl>]
    [TestCase<SequenceId>]
    public void CreateTest<T>() where T : IEntity
    {
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var factory = new FirestoreDbQueryFactory(testFirestoreDb);
      IDbQuery<T> actualQuery = factory.Create<T>();
      Assert.That(actualQuery, Is.TypeOf<FirestoreDbQuery<T>>());
    }

    #endregion Public Methods
  }
}