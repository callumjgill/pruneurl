using Google.Cloud.Firestore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Requests
{
  [TestFixture]
  [Parallelizable]
  [Description("These tests require communicating with the FirestoreDb emulator, which is in-memory and so this can be considered a unit test.")]
  public sealed class FirestoreDbTransactionUnitTests
  {
    #region Public Methods

    [Test]
    public async Task TransactionCommitTest_NothingToCommitDoesNothing()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = TestFirestoreDbHelper.GetTestCollectionReference(testFirestoreDb);
      DocumentReference testDocumentReference = testCollectionReference.Document(testId);
      var stubEntity = new StubFirestoreEntity(testId);
      await testDocumentReference.CreateAsync(stubEntity);
      List<DocumentReference> beforeCommitDocuments = testCollectionReference.ListDocumentsAsync().ToBlockingEnumerable().ToList();
      await testFirestoreDb.RunTransactionAsync(async transaction =>
      {
        // Test
        var dbTransaction = new FirestoreDbTransaction<StubFirestoreEntity>(testCollectionReference, transaction);
        StubFirestoreEntity? actualStubEntity = await dbTransaction.GetByIdAsync(testId);
        Assert.That(actualStubEntity, Is.Not.Null);
        Assert.That(actualStubEntity.Id, Is.EqualTo(testId));
      });

      IEnumerable<DocumentReference> afterCommitDocuments = testCollectionReference.ListDocumentsAsync().ToBlockingEnumerable();
      Assert.That(afterCommitDocuments, Is.EquivalentTo(beforeCommitDocuments));
    }

    [Test]
    public async Task TransactionCommitTest_ReadSuccessThenUpdateCommitted()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      string initialTestData = "Testing123";
      string newTestData = "NewTesting123";
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = TestFirestoreDbHelper.GetTestCollectionReference(testFirestoreDb);
      DocumentReference testDocumentReference = testCollectionReference.Document(testId);
      var stubEntity = new StubFirestoreEntity(testId, initialTestData);
      await testDocumentReference.CreateAsync(stubEntity);
      DocumentReference testDocumentReferenceToCreate = testCollectionReference.Document(testId);
      await testFirestoreDb.RunTransactionAsync(async transaction =>
      {
        // Test
        var dbTransaction = new FirestoreDbTransaction<StubFirestoreEntity>(testCollectionReference, transaction);
        var newStubEntity = new StubFirestoreEntity(testId, newTestData);
        StubFirestoreEntity? actualStubEntity = await dbTransaction.GetByIdAsync(testId);
        Assert.That(actualStubEntity, Is.Not.Null);
        Assert.That(actualStubEntity.Id, Is.EqualTo(testId));
        Assert.That(actualStubEntity.TestData, Is.EqualTo(initialTestData));
        dbTransaction.Update(newStubEntity);
      });

      DocumentSnapshot dbSnapshot = await testDocumentReference.GetSnapshotAsync();
      StubFirestoreEntity? actualStubFirestoreEntity = dbSnapshot.ConvertTo<StubFirestoreEntity>();
      Assert.That(actualStubFirestoreEntity, Is.Not.Null);
      Assert.That(actualStubFirestoreEntity.Id, Is.EqualTo(testId));
      Assert.That(actualStubFirestoreEntity.TestData, Is.EqualTo(newTestData));
    }

    [Test]
    public async Task TransactionCommitTest_WriteFirstThenReadThrowsException()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = TestFirestoreDbHelper.GetTestCollectionReference(testFirestoreDb);
      DocumentReference testDocumentReference = testCollectionReference.Document(testId);
      var stubEntity = new StubFirestoreEntity(testId);
      await testDocumentReference.CreateAsync(stubEntity);
      List<DocumentReference> beforeCommitDocuments = testCollectionReference.ListDocumentsAsync().ToBlockingEnumerable().ToList();
      DocumentReference testDocumentReferenceToCreate = testCollectionReference.Document(testId);
      Assert.That(async () => await testFirestoreDb.RunTransactionAsync(async transaction =>
      {
        // Test
        var dbTransaction = new FirestoreDbTransaction<StubFirestoreEntity>(testCollectionReference, transaction);
        var newStubEntity = new StubFirestoreEntity(testId);
        dbTransaction.Update(newStubEntity);
        await dbTransaction.GetByIdAsync(testId);
      }), Throws.Exception);
    }

    #endregion Public Methods
  }
}