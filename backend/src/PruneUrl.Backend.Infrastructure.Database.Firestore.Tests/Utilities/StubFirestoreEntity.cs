using Google.Cloud.Firestore;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.Utilities
{
  /// <summary>
  /// A test, or "stub", version of a "firestore" entity.
  /// </summary>
  [FirestoreData]
  internal sealed class StubFirestoreEntity : FirestoreEntityDTO
  {
    /// <summary>
    /// Constructor used by firestore in deserialization
    /// </summary>
    public StubFirestoreEntity() { }

    /// <summary>
    /// Constructor for creating a new instance
    /// </summary>
    /// <param name="id"> The id of the entity </param>
    /// <param name="testData"> The test data for the entity. </param>
    internal StubFirestoreEntity(string id, string? testData = null)
    {
      Id = id;
      TestData = testData;
    }

    [FirestoreProperty]
    public string? TestData { get; set; }
  }
}
