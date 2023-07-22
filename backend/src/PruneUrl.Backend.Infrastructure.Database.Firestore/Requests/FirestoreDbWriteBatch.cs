using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Write;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Requests
{
  /// <summary>
  /// An implementation of <see cref="IDbWriteBatch{T}" /> specific to Firestore. This acts as a
  /// wrapper around <see cref="WriteBatch" />.
  /// </summary>
  /// <typeparam name="T"> The <see cref="FirestoreEntityDTO" /> the batch is concerned with. </typeparam>
  internal sealed class FirestoreDbWriteBatch<T> : IDbWriteBatch<T> where T : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly CollectionReference collection;
    private readonly WriteBatch writeBatch;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbWriteBatch{T}" /> class.
    /// </summary>
    /// <param name="collection">
    /// A reference to the collection in the Firestore database corresponding to the <typeparamref
    /// name="T" /> type.
    /// </param>
    /// <param name="writeBatch"> A batch of write operations, to be performed in a single commit. </param>
    public FirestoreDbWriteBatch(CollectionReference collection, WriteBatch writeBatch)
    {
      this.collection = collection;
      this.writeBatch = writeBatch;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbWriteBatch{T}.CommitAsync(CancellationToken)" />
    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
      return writeBatch.CommitAsync(cancellationToken);
    }

    /// <inheritdoc cref="IDbCreateOperation{T}.Create(T)" />
    public void Create(T entity)
    {
      DocumentReference documentToCreate = collection.Document(entity.Id);
      writeBatch.Create(documentToCreate, entity);
    }

    /// <inheritdoc cref="IDbDeleteOperation.Delete(string)" />
    public void Delete(string id)
    {
      DocumentReference documentToDelete = collection.Document(id);
      writeBatch.Delete(documentToDelete);
    }

    #endregion Public Methods
  }
}