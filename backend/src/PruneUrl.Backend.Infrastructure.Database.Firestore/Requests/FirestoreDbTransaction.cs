using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Write;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Requests
{
  /// <summary>
  /// A Firestore specific implementation of <see cref="IDbTransaction{T}" />. This is a wrapper
  /// around <see cref="Transaction" />
  /// </summary>
  /// <typeparam name="T">
  /// The <see cref="FirestoreEntityDTO" /> the transaction is concerned with.
  /// </typeparam>
  internal sealed class FirestoreDbTransaction<T> : IDbTransaction<T> where T : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly CollectionReference collection;
    private readonly Transaction transaction;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbTransaction{T}" />.
    /// </summary>
    /// <param name="collection">
    /// A reference to the collection in the Firestore database corresponding to the <typeparamref
    /// name="T" /> type.
    /// </param>
    /// <param name="transaction"> The <see cref="Transaction" /> to wrap. </param>
    public FirestoreDbTransaction(CollectionReference collection, Transaction transaction)
    {
      this.collection = collection;
      this.transaction = transaction;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbGetByIdOperation{T}.GetByIdAsync(string, CancellationToken)" />
    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
      DocumentReference document = collection.Document(id);
      DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(document, cancellationToken);
      return snapshot.ConvertTo<T>();
    }

    /// <inheritdoc cref="IDbUpdateOperation{T}.Update(T)" />
    public void Update(T entity)
    {
      DocumentReference document = collection.Document(entity.Id);
      transaction.Set(document, entity);
    }

    #endregion Public Methods
  }
}