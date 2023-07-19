using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.DbQuery;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbQuery
{
  /// <summary>
  /// An implementation of <see cref="IDbQuery{T}" /> specific to Firestore.
  /// </summary>
  /// <typeparam name="T"> The <see cref="FirestoreEntityDTO" /> the query is concerned with. </typeparam>
  internal sealed class FirestoreDbQuery<T> : IDbQuery<T> where T : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly CollectionReference collection;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbQuery{T}" /> class.
    /// </summary>
    /// <param name="collectionReference">
    /// The reference to the collection in the Firestore database corresponding to the <typeparamref
    /// name="T" /> type.
    /// </param>
    public FirestoreDbQuery(CollectionReference collection)
    {
      this.collection = collection;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbQuery{T}" />
    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
      DocumentReference document = collection.Document(id);
      DocumentSnapshot snapshot = await document.GetSnapshotAsync(cancellationToken);
      return snapshot.ConvertTo<T>();
    }

    #endregion Public Methods
  }
}