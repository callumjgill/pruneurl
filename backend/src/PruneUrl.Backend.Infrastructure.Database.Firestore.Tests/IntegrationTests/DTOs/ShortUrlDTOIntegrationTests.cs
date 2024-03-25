using Google.Cloud.Firestore;
using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.IntegrationTests.DTOs;

[TestFixture]
public sealed class ShortUrlDTOIntegrationTests
{
  private const string testCollectionPath = "ShortUrlDTOIntegrationTests";

  [Test]
  public async Task ConvertToFirestoreDocumentReferenceTest()
  {
    // Setup database for test
    var testEntity = new ShortUrlDTO()
    {
      Id = Guid.NewGuid().ToString(),
      LongUrl = "https://www.youtube.com",
      Url = "pruneurl.com/ws"
    };
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
      Assert.That(snapshot.CreateTime?.ToDateTime(), Is.Not.Null);
      Assert.That(snapshot.ContainsField(nameof(testEntity.LongUrl)), Is.True);
      Assert.That(
        snapshot.GetValue<string?>(nameof(testEntity.LongUrl)),
        Is.EqualTo(testEntity.LongUrl)
      );
      Assert.That(snapshot.ContainsField(nameof(testEntity.Url)), Is.True);
      Assert.That(snapshot.GetValue<string?>(nameof(testEntity.Url)), Is.EqualTo(testEntity.Url));
    });
    ShortUrlDTO? result = snapshot.ConvertTo<ShortUrlDTO>();
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Id, Is.EqualTo(testEntity.Id));
      Assert.That(result.Created, Is.EqualTo(snapshot.CreateTime?.ToDateTime()));
      Assert.That(result.LongUrl, Is.EqualTo(testEntity.LongUrl));
      Assert.That(result.Url, Is.EqualTo(testEntity.Url));
    });
  }

  [SetUp]
  public async Task SetupTest()
  {
    await TestFirestoreDbHelper.ClearEmulatedDatabase();
  }
}
