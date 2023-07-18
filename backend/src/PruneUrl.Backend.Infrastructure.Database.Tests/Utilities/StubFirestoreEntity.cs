using Google.Cloud.Firestore;
using PruneUrl.Backend.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.Utilities
{
  /// <summary>
  /// A test, or "stub", version of a "firestore" entity.
  /// </summary>
  [FirestoreData]
  internal sealed class StubFirestoreEntity : IEntity
  {
    #region Public Constructors

    /// <summary>
    /// Constructor used by firestore in deserialization
    /// </summary>
    public StubFirestoreEntity()
    {
    }

    #endregion Public Constructors

    #region Internal Constructors

    /// <summary>
    /// Constructor for creating a new instance
    /// </summary>
    /// <param name="id"> The id of the entity </param>
    internal StubFirestoreEntity(string id)
    {
      Id = id;
    }

    #endregion Internal Constructors

    #region Public Properties

    /// <inheritdoc cref="IEntity.Id" />
    [FirestoreDocumentId]
    [AllowNull]
    public string Id { get; set; }

    #endregion Public Properties
  }
}