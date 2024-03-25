using Google.Cloud.Firestore;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs
{
  /// <summary>
  /// An <see cref="FirestoreEntityDTO" /> encapsulating a "sequence id". This is an integer value
  /// which is updated each time a new short url is created.
  /// </summary>
  [FirestoreData]
  internal sealed class SequenceIdDTO : FirestoreEntityDTO
  {
    /// <summary>
    /// The actual value of the sequence id entity, i.e. the actual "sequence id".
    /// </summary>
    [FirestoreProperty]
    public int? Value { get; set; }
  }
}
