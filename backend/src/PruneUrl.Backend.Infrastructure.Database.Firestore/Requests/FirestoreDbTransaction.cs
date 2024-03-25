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
  /// <param name="collection">
  /// A reference to the collection in the Firestore database corresponding to the <typeparamref
  /// name="T" /> type.
  /// </param>
  /// <param name="transaction"> The <see cref="Transaction" /> to wrap. </param>
  internal sealed class FirestoreDbTransaction<T>(
    CollectionReference collection,
    Transaction transaction
  ) : IDbTransaction<T>
    where T : FirestoreEntityDTO
  {
    private readonly CollectionReference collection = collection;
    private readonly Transaction transaction = transaction;

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
  }
}
