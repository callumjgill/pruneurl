using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction
{
  /// <summary>
  /// An implementation of <see cref="IDbTransaction{T}" /> specific to Firestore.
  /// </summary>
  /// <typeparam name="T">
  /// The <see cref="FirestoreEntityDTO" /> the transaction is concerned with.
  /// </typeparam>
  internal sealed class FirestoreDbTransaction<T> : IDbTransaction<T> where T : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly CollectionReference collection;

    private WriteBatch writeBatch;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbTransaction{T}" /> class.
    /// </summary>
    /// <param name="collection">
    /// A reference to the collection in the Firestore database corresponding to the <typeparamref
    /// name="T" /> type.
    /// </param>
    /// <param name="writeBatch"> A batch of write operations, to be performed in a single commit. </param>
    public FirestoreDbTransaction(CollectionReference collection, WriteBatch writeBatch)
    {
      this.collection = collection;
      this.writeBatch = writeBatch;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbTransaction{T}.CommitAsync(CancellationToken)" />
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
      await writeBatch.CommitAsync(cancellationToken);
    }

    /// <inheritdoc cref="IDbTransaction{T}.Create(T)" />
    public IDbTransaction<T> Create(T entity)
    {
      DocumentReference documentToCreate = collection.Document(entity.Id);
      writeBatch = writeBatch.Create(documentToCreate, entity);
      return this;
    }

    /// <inheritdoc cref="IDbTransaction{T}.Delete(string)" />
    public IDbTransaction<T> Delete(string id)
    {
      DocumentReference documentToDelete = collection.Document(id);
      writeBatch = writeBatch.Delete(documentToDelete);
      return this;
    }

    #endregion Public Methods
  }
}