using AutoMapper;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// A Firestore specific factory for creating <see cref="IDbTransaction{T}" /> instances.
/// </summary>
/// <param name="firestoreDb"> The underlying Firestore Database client. </param>
/// <param name="mapper"> A service being used to map DTOs to Entities and vice versa. </param>
public sealed class FirestoreDbTransactionFactory(FirestoreDb firestoreDb, IMapper mapper)
  : IFirestoreDbTransactionFactory
{
  private readonly FirestoreDb firestoreDb = firestoreDb;
  private readonly IMapper mapper = mapper;

  /// <inheritdoc cref="IFirestoreDbTransactionFactory.Create{T}(Transaction)" />
  public IDbTransaction<T> Create<T>(Transaction transaction)
    where T : IEntity
  {
    switch (typeof(T))
    {
      case Type sequenceIdType when sequenceIdType == typeof(SequenceId):
        return Create<T, SequenceIdDTO>(transaction);

      case Type sequenceIdType when sequenceIdType == typeof(ShortUrl):
        return Create<T, ShortUrlDTO>(transaction);

      default:
        throw new InvalidEntityTypeMapException(typeof(T));
    }
  }

  private IDbTransaction<TEntity> Create<TEntity, TFirestoreEntity>(Transaction transaction)
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    CollectionReference collection = firestoreDb.Collection(
      CollectionReferenceHelper.GetCollectionPath<TEntity>()
    );
    var firestoreDbTransaction = new FirestoreDbTransaction<TFirestoreEntity>(
      collection,
      transaction
    );
    return new FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity>(
      firestoreDbTransaction,
      mapper
    );
  }
}
