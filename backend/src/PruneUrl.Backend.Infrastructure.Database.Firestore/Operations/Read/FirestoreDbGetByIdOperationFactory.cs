using AutoMapper;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read
{
  /// <summary>
  /// Defines a factory for creating <see cref="IDbGetByIdOperation{T}" /> ( <see
  /// cref="FirestoreDbGetByIdOperation{T}" />) instances.
  /// </summary>
  /// <param name="firestoreDb">
  /// The <see cref="FirestoreDb" /> instance for creating the operations against.
  /// </param>
  /// <param name="mapper">
  /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
  /// />'s and the core <see cref="IEntity" />'s.
  /// </param>
  public sealed class FirestoreDbGetByIdOperationFactory(FirestoreDb firestoreDb, IMapper mapper)
    : IDbGetByIdOperationFactory
  {
    private readonly FirestoreDb firestoreDb = firestoreDb;
    private readonly IMapper mapper = mapper;

    /// <inheritdoc cref="IDbGetByIdOperationFactory.Create{T}" />
    public IDbGetByIdOperation<T> Create<T>()
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

    private IDbGetByIdOperation<TEntity> Create<TEntity, TFirestoreEntity>()
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      CollectionReference collection = firestoreDb.Collection(
        CollectionReferenceHelper.GetCollectionPath<TEntity>()
      );
      var firestoreDbGetByIdOperation = new FirestoreDbGetByIdOperation<TFirestoreEntity>(
        collection
      );
      return new FirestoreDbGetByIdOperationAdapter<TEntity, TFirestoreEntity>(
        firestoreDbGetByIdOperation,
        mapper
      );
    }
  }
}
