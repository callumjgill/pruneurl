using Google.Cloud.Firestore;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests;

[TestFixture]
[Parallelizable]
[Description(
  "These tests require communicating with the FirestoreDb emulator, which is in-memory and so this can be considered a unit test."
)]
public sealed class FirestoreDbWriteBatchUnitTests
{
  [Test]
  public async Task CommitAsyncTest_CreateAndDeleteCommitted()
  {
    // Setup database for test
    string initialTestId = Guid.NewGuid().ToString();
    string newTestId = Guid.NewGuid().ToString();
    FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    CollectionReference testCollectionReference = TestFirestoreDbHelper.GetTestCollectionReference(
      testFirestoreDb
    );
    DocumentReference testDocumentReference = testCollectionReference.Document(initialTestId);
    var stubEntity = new StubFirestoreEntity(initialTestId);
    await testDocumentReference.CreateAsync(stubEntity);
    List<DocumentReference> beforeCommitDocuments = testCollectionReference
      .ListDocumentsAsync()
      .ToBlockingEnumerable()
      .ToList();
    WriteBatch testWriteBatch = testFirestoreDb.StartBatch();
    DocumentReference testDocumentReferenceToCreate = testCollectionReference.Document(newTestId);

    // Test
    var dbWriteBatch = new FirestoreDbWriteBatch<StubFirestoreEntity>(
      testCollectionReference,
      testWriteBatch
    );
    var newStubEntity = new StubFirestoreEntity(newTestId);
    dbWriteBatch.Create(newStubEntity);
    dbWriteBatch.Delete(initialTestId);
    await dbWriteBatch.CommitAsync();
    IEnumerable<DocumentReference> afterCommitDocuments = testCollectionReference
      .ListDocumentsAsync()
      .ToBlockingEnumerable();

    Assert.That(afterCommitDocuments, Is.Not.EquivalentTo(beforeCommitDocuments));
    Assert.That(afterCommitDocuments, Is.EquivalentTo(new[] { testDocumentReferenceToCreate }));
  }

  [Test]
  public async Task CommitAsyncTest_NothingToCommitDoesNothing()
  {
    // Setup database for test
    string testId = Guid.NewGuid().ToString();
    FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    CollectionReference testCollectionReference = TestFirestoreDbHelper.GetTestCollectionReference(
      testFirestoreDb
    );
    DocumentReference testDocumentReference = testCollectionReference.Document(testId);
    var stubEntity = new StubFirestoreEntity(testId);
    await testDocumentReference.CreateAsync(stubEntity);
    List<DocumentReference> beforeCommitDocuments = testCollectionReference
      .ListDocumentsAsync()
      .ToBlockingEnumerable()
      .ToList();
    WriteBatch testWriteBatch = testFirestoreDb.StartBatch();

    // Test
    var dbWriteBatch = new FirestoreDbWriteBatch<StubFirestoreEntity>(
      testCollectionReference,
      testWriteBatch
    );
    await dbWriteBatch.CommitAsync();
    IEnumerable<DocumentReference> afterCommitDocuments = testCollectionReference
      .ListDocumentsAsync()
      .ToBlockingEnumerable();

    Assert.That(afterCommitDocuments, Is.EquivalentTo(beforeCommitDocuments));
  }
}
