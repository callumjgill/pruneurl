using AutoMapper;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Requests
{
  /// <summary>
  /// A factory for creating <see cref="IDbWriteBatch{T}" /> ( <see cref="FirestoreDbWriteBatch{T}"
  /// />) instances.
  /// </summary>
  public sealed class FirestoreDbWriteBatchFactory : IDbWriteBatchFactory
  {
    #region Private Fields

    private readonly FirestoreDb firestoreDb;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbWriteBatchFactory" /> class.
    /// </summary>
    /// <param name="firestoreDb">
    /// The <see cref="FirestoreDb" /> instance for creating batches against.
    /// </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbWriteBatchFactory(FirestoreDb firestoreDb, IMapper mapper)
    {
      this.firestoreDb = firestoreDb;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbWriteBatchFactory.Create{T}" />
    public IDbWriteBatch<T> Create<T>() where T : IEntity
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

    private IDbWriteBatch<TEntity> Create<TEntity, TFirestoreEntity>()
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      CollectionReference collection = firestoreDb.Collection(CollectionReferenceHelper.GetCollectionPath<TEntity>());
      WriteBatch writeBatch = firestoreDb.StartBatch();
      var firestoreDbWriteBatch = new FirestoreDbWriteBatch<TFirestoreEntity>(collection, writeBatch);
      return new FirestoreDbWriteBatchAdapter<TEntity, TFirestoreEntity>(firestoreDbWriteBatch, mapper);
    }

    #endregion Private Methods
  }
}