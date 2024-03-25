using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read;

/// <summary>
/// An implementation of <see cref="IDbGetByIdOperation{T}" /> specific to Firestore.
/// </summary>
/// <typeparam name="T">
/// The <see cref="FirestoreEntityDTO" /> the operation is concerned with.
/// </typeparam>
/// <param name="collectionReference">
/// The reference to the collection in the Firestore database corresponding to the <typeparamref
/// name="T" /> type.
/// </param>
internal sealed class FirestoreDbGetByIdOperation<T>(CollectionReference collection)
  : IDbGetByIdOperation<T>
  where T : FirestoreEntityDTO
{
  private readonly CollectionReference collection = collection;

  /// <inheritdoc cref="IDbGetByIdOperation{T}.GetByIdAsync(string, CancellationToken)" />
  public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
  {
    DocumentReference document = collection.Document(id);
    DocumentSnapshot snapshot = await document.GetSnapshotAsync(cancellationToken);
    return snapshot.ConvertTo<T>();
  }
}
