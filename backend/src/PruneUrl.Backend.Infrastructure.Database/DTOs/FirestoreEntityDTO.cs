using Google.Cloud.Firestore;
using PruneUrl.Backend.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs
{
  /// <summary>
  /// An <see cref="IEntity" /> DTO specific to the read/write values to the Firestore DB.
  /// </summary>
  public abstract class FirestoreEntityDTO : IEntity
  {
    #region Public Properties

    /// <inheritdoc cref="IEntity.Id" />
    [FirestoreDocumentId]
    [AllowNull]
    public string Id { get; set; }

    #endregion Public Properties
  }
}