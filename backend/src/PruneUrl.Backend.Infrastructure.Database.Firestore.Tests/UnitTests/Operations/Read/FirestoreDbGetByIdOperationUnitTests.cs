using Google.Cloud.Firestore;
using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.UnitTests.Operations.Read
{
  [TestFixture]
  [Parallelizable]
  [Description("These tests require communicating with the FirestoreDb emulator, which is in-memory and so this can be considered a unit test.")]
  public sealed class FirestoreDbGetByIdOperationUnitTests
  {
    #region Public Methods

    [Test]
    public async Task GetByIdAsyncTest_DocumentInDb()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = TestFirestoreDbHelper.GetTestCollectionReference(testFirestoreDb);
      DocumentReference testDocumentReference = testCollectionReference.Document(testId);
      var stubEntity = new StubFirestoreEntity(testId);
      await testDocumentReference.CreateAsync(stubEntity);

      // Test
      var dbGetByIdOperation = new FirestoreDbGetByIdOperation<StubFirestoreEntity>(testCollectionReference);
      StubFirestoreEntity? result = await dbGetByIdOperation.GetByIdAsync(testId);
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Id, Is.EqualTo(testId));
    }

    [Test]
    public async Task GetByIdAsyncTest_DocumentNotInDb()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = TestFirestoreDbHelper.GetTestCollectionReference(testFirestoreDb);

      // Test
      var dbGetByIdOperation = new FirestoreDbGetByIdOperation<StubFirestoreEntity>(testCollectionReference);
      StubFirestoreEntity? result = await dbGetByIdOperation.GetByIdAsync(testId);
      Assert.That(result, Is.Null);
    }

    #endregion Public Methods
  }
}