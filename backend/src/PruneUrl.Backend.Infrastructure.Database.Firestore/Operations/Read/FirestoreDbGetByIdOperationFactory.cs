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
  public sealed class FirestoreDbGetByIdOperationFactory : IDbGetByIdOperationFactory
  {
    #region Private Fields

    private readonly FirestoreDb firestoreDb;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbGetByIdOperationFactory" /> class.
    /// </summary>
    /// <param name="firestoreDb">
    /// The <see cref="FirestoreDb" /> instance for creating the operations against.
    /// </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbGetByIdOperationFactory(FirestoreDb firestoreDb, IMapper mapper)
    {
      this.firestoreDb = firestoreDb;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbGetByIdOperationFactory.Create{T}" />
    public IDbGetByIdOperation<T> Create<T>() where T : IEntity
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

    #endregion Public Methods

    #region Private Methods

    private IDbGetByIdOperation<TEntity> Create<TEntity, TFirestoreEntity>()
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      CollectionReference collection = firestoreDb.Collection(CollectionReferenceHelper.GetCollectionPath<TEntity>());
      var firestoreDbGetByIdOperation = new FirestoreDbGetByIdOperation<TFirestoreEntity>(collection);
      return new FirestoreDbGetByIdOperationAdapter<TEntity, TFirestoreEntity>(firestoreDbGetByIdOperation, mapper);
    }

    #endregion Private Methods
  }
}