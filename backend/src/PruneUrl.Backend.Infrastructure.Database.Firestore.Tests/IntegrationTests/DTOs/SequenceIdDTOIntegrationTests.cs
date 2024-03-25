using Google.Cloud.Firestore;
using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.IntegrationTests.DTOs
{
  [TestFixture]
  public sealed class SequenceIdDTOIntegrationTests
  {
    private const string testCollectionPath = "SequenceIdDTOIntegrationTests";

    [Test]
    public async Task ConvertToFirestoreDocumentReferenceTest()
    {
      // Setup database for test
      var testEntity = new SequenceIdDTO() { Id = Guid.NewGuid().ToString(), Value = 412674231 };
      FirestoreDb testFirestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      CollectionReference testCollectionReference = testFirestoreDb.Collection(testCollectionPath);
      DocumentReference testDocumentReference = testCollectionReference.Document(testEntity.Id);
      await testDocumentReference.CreateAsync(testEntity);

      // Test
      DocumentSnapshot snapshot = await testDocumentReference.GetSnapshotAsync();
      Assert.That(snapshot.Exists, Is.True);
      Assert.Multiple(() =>
      {
        Assert.That(snapshot.Id, Is.EqualTo(testEntity.Id));
        Assert.That(snapshot.ContainsField(nameof(testEntity.Value)), Is.True);
        Assert.That(
          snapshot.GetValue<int?>(nameof(testEntity.Value)),
          Is.EqualTo(testEntity.Value)
        );
      });
      SequenceIdDTO? result = snapshot.ConvertTo<SequenceIdDTO>();
      Assert.Multiple(() =>
      {
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(testEntity.Id));
        Assert.That(result.Value, Is.EqualTo(testEntity.Value));
      });
    }

    [SetUp]
    public async Task SetupTest()
    {
      await TestFirestoreDbHelper.ClearEmulatedDatabase();
    }
  }
}
