using AutoMapper;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// A factory for creating <see cref="IDbWriteBatch{T}" /> ( <see cref="FirestoreDbWriteBatch{T}"
/// />) instances.
/// </summary>
/// <param name="firestoreDb">
/// The <see cref="FirestoreDb" /> instance for creating batches against.
/// </param>
/// <param name="mapper">
/// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
/// />'s and the core <see cref="IEntity" />'s.
/// </param>
public sealed class FirestoreDbWriteBatchFactory(FirestoreDb firestoreDb, IMapper mapper)
  : IDbWriteBatchFactory
{
  private readonly FirestoreDb firestoreDb = firestoreDb;
  private readonly IMapper mapper = mapper;

  /// <inheritdoc cref="IDbWriteBatchFactory.Create{T}" />
  public IDbWriteBatch<T> Create<T>()
    where T : IEntity
  {
    switch (typeof(T))
    {
      case Type sequenceIdType when sequenceIdType == typeof(SequenceId):
        return Create<T, SequenceIdDTO>();

      case Type sequenceIdType when sequenceIdType == typeof(ShortUrl):
        return Create<T, ShortUrlDTO>();

      default:
        throw new InvalidEntityTypeMapException(typeof(T));
    }
  }

  private IDbWriteBatch<TEntity> Create<TEntity, TFirestoreEntity>()
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    CollectionReference collection = firestoreDb.Collection(
      CollectionReferenceHelper.GetCollectionPath<TEntity>()
    );
    WriteBatch writeBatch = firestoreDb.StartBatch();
    var firestoreDbWriteBatch = new FirestoreDbWriteBatch<TFirestoreEntity>(collection, writeBatch);
    return new FirestoreDbWriteBatchAdapter<TEntity, TFirestoreEntity>(
      firestoreDbWriteBatch,
      mapper
    );
  }
}
