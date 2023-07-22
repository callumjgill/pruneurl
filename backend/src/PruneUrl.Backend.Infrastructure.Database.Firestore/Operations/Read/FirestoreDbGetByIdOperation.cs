using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read
{
  /// <summary>
  /// An implementation of <see cref="IDbGetByIdOperation{T}" /> specific to Firestore.
  /// </summary>
  /// <typeparam name="T">
  /// The <see cref="FirestoreEntityDTO" /> the operation is concerned with.
  /// </typeparam>
  internal sealed class FirestoreDbGetByIdOperation<T> : IDbGetByIdOperation<T> where T : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly CollectionReference collection;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbGetByIdOperation{T}" /> class.
    /// </summary>
    /// <param name="collectionReference">
    /// The reference to the collection in the Firestore database corresponding to the <typeparamref
    /// name="T" /> type.
    /// </param>
    public FirestoreDbGetByIdOperation(CollectionReference collection)
    {
      this.collection = collection;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbGetByIdOperation{T}.GetByIdAsync(string, CancellationToken)" />
    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
      DocumentReference document = collection.Document(id);
      DocumentSnapshot snapshot = await document.GetSnapshotAsync(cancellationToken);
      return snapshot.ConvertTo<T>();
    }

    #endregion Public Methods
  }
}