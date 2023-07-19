using Google.Cloud.Firestore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.DbTransaction
{
  [TestFixture]
  [Description("These tests require communicating with the FirestoreDb emulator, which is in-memory and so this can be considered a unit test.")]
  public sealed class FirestoreDbTransactionUnitTests
  {
    #region Private Fields

    private const string testCollectionPath = "TransactionTest";

    #endregion Private Fields

    #region Public Methods

    [Test]
    public async Task CommitAsyncTest_CreateAndDeleteCommitted()
    {
      // Setup database for test
      string initialTestId = Guid.NewGuid().ToString();
      string newTestId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      DocumentReference testDocumentReference = testCollectionReference.Document(initialTestId);
      var stubEntity = new StubFirestoreEntity(initialTestId);
      await testDocumentReference.CreateAsync(stubEntity);
      List<DocumentReference> beforeCommitDocuments = testCollectionReference.ListDocumentsAsync().ToBlockingEnumerable().ToList();
      WriteBatch testWriteBatch = testFirestoreDb.StartBatch();
      DocumentReference testDocumentReferenceToCreate = testCollectionReference.Document(newTestId);

      // Test
      var dbTransaction = new FirestoreDbTransaction<StubFirestoreEntity>(testCollectionReference, testWriteBatch);
      var newStubEntity = new StubFirestoreEntity(newTestId);
      dbTransaction.Create(newStubEntity)
                   .Delete(initialTestId);
      await dbTransaction.CommitAsync();
      CollectionReference afterCommitTestCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      IEnumerable<DocumentReference> afterCommitDocuments = afterCommitTestCollectionReference.ListDocumentsAsync().ToBlockingEnumerable();

      Assert.That(afterCommitDocuments, Is.Not.EquivalentTo(beforeCommitDocuments));
      Assert.That(afterCommitDocuments, Is.EquivalentTo(new[] { testDocumentReferenceToCreate }));
    }

    [Test]
    public async Task CommitAsyncTest_NothingToCommitDoesNothing()
    {
      // Setup database for test
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      DocumentReference testDocumentReference = testCollectionReference.Document(testId);
      var stubEntity = new StubFirestoreEntity(testId);
      await testDocumentReference.CreateAsync(stubEntity);
      List<DocumentReference> beforeCommitDocuments = testCollectionReference.ListDocumentsAsync().ToBlockingEnumerable().ToList();
      WriteBatch testWriteBatch = testFirestoreDb.StartBatch();

      // Test
      var dbTransaction = new FirestoreDbTransaction<StubFirestoreEntity>(testCollectionReference, testWriteBatch);
      await dbTransaction.CommitAsync();
      CollectionReference afterCommitTestCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      IEnumerable<DocumentReference> afterCommitDocuments = afterCommitTestCollectionReference.ListDocumentsAsync().ToBlockingEnumerable();

      Assert.That(afterCommitDocuments, Is.EquivalentTo(beforeCommitDocuments));
    }

    [Test]
    public void CreateTest_ReturnsSameTransaction()
    {
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      WriteBatch testWriteBatch = testFirestoreDb.StartBatch();

      var dbTransaction = new FirestoreDbTransaction<StubFirestoreEntity>(testCollectionReference, testWriteBatch);
      var newStubEntity = new StubFirestoreEntity(testId);
      IDbTransaction<StubFirestoreEntity> dbTransactionFollowingCreate = dbTransaction.Create(newStubEntity);
      Assert.That(dbTransactionFollowingCreate, Is.EqualTo(dbTransaction));
    }

    [Test]
    public void DeleteTest_ReturnsDifferentTransaction()
    {
      string testId = Guid.NewGuid().ToString();
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      WriteBatch testWriteBatch = testFirestoreDb.StartBatch();

      var dbTransaction = new FirestoreDbTransaction<StubFirestoreEntity>(testCollectionReference, testWriteBatch);
      IDbTransaction<StubFirestoreEntity> dbTransactionFollowingDelete = dbTransaction.Delete(testId);
      Assert.That(dbTransactionFollowingDelete, Is.EqualTo(dbTransaction));
    }

    [SetUp]
    public async Task SetupTest()
    {
      await TestFirestoreDbHelper.ClearEmulatedDatabase();
    }

    #endregion Public Methods
  }
}