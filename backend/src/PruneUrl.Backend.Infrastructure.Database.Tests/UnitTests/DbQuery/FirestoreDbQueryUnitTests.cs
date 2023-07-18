using Google.Cloud.Firestore;
using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DbQuery;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.UnitTests.DbQuery
{
  [TestFixture]
  [Description("These tests require communicating with the FirestoreDb emulator, which is in-memory and so this can be considered a unit test.")]
  public sealed class FirestoreDbQueryUnitTests
  {
    #region Private Fields

    private const string testCollectionPath = "QueryTest";

    #endregion Private Fields

    #region Public Methods

    [Test]
    public async Task GetByIdAsyncTest_DocumentInDb()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      DocumentReference testDocumentReference = testCollectionReference.Document(testId);
      var stubEntity = new StubFirestoreEntity(testId);
      await testDocumentReference.CreateAsync(stubEntity);

      // Test
      var dbQuery = new FirestoreDbQuery<StubFirestoreEntity>(testCollectionReference);
      StubFirestoreEntity? result = await dbQuery.GetByIdAsync(testId);
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Id, Is.EqualTo(testId));
    }

    [Test]
    public async Task GetByIdAsyncTest_DocumentNotInDb()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = testFirestoreDb.Collection(testCollectionPath);

      // Test
      var dbQuery = new FirestoreDbQuery<StubFirestoreEntity>(testCollectionReference);
      StubFirestoreEntity? result = await dbQuery.GetByIdAsync(testId);
      Assert.That(result, Is.Null);
    }

    [SetUp]
    public async Task SetupTest()
    {
      await TestFirestoreDbHelper.ClearEmulatedDatabase();
    }

    #endregion Public Methods
  }
}