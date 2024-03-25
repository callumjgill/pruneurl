using System.Diagnostics.CodeAnalysis;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs
{
  /// <summary>
  /// An <see cref="IEntity" /> DTO specific to the read/write values to the Firestore DB.
  /// </summary>
  internal abstract class FirestoreEntityDTO : IEntity
  {
    /// <inheritdoc cref="IEntity.Id" />
    [FirestoreDocumentId]
    [AllowNull]
    public string Id { get; set; }
  }
}
