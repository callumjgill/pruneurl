using AutoMapper;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction
{
  /// <summary>
  /// A factory for creating <see cref="IDbTransaction{T}" /> ( <see
  /// cref="FirestoreDbTransaction{T}" />) instances.
  /// </summary>
  public sealed class FirestoreDbTransactionFactory : IDbTransactionFactory
  {
    #region Private Fields

    private readonly FirestoreDb firestoreDb;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbTransactionFactory" /> class.
    /// </summary>
    /// <param name="firestoreDb">
    /// The <see cref="FirestoreDb" /> instance for creating transactions against.
    /// </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbTransactionFactory(FirestoreDb firestoreDb, IMapper mapper)
    {
      this.firestoreDb = firestoreDb;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbTransactionFactory.Create{T}" />
    public IDbTransaction<T> Create<T>() where T : IEntity
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

    private IDbTransaction<TEntity> Create<TEntity, TFirestoreEntity>()
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      CollectionReference collection = firestoreDb.Collection(CollectionReferenceHelper.GetCollectionPath<TEntity>());
      WriteBatch writeBatch = firestoreDb.StartBatch();
      var firestoreDbTransaction = new FirestoreDbTransaction<TFirestoreEntity>(collection, writeBatch);
      return new FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity>(firestoreDbTransaction, mapper);
    }

    #endregion Private Methods
  }
}