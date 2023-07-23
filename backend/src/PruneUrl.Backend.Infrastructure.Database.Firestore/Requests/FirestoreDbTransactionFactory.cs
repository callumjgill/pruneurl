using AutoMapper;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Interfaces;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Requests
{
  /// <summary>
  /// A Firestore specific factory for creating <see cref="IDbTransaction{T}" /> instances.
  /// </summary>
  public sealed class FirestoreDbTransactionFactory : IFirestoreDbTransactionFactory
  {
    #region Private Fields

    private readonly FirestoreDb firestoreDb;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbTransactionFactory" /> instance.
    /// </summary>
    /// <param name="firestoreDb"> The underlying Firestore Database client. </param>
    /// <param name="mapper"> A service being used to map DTOs to Entities and vice versa. </param>
    public FirestoreDbTransactionFactory(FirestoreDb firestoreDb, IMapper mapper)
    {
      this.firestoreDb = firestoreDb;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IFirestoreDbTransactionFactory.Create{T}(Transaction)" />
    public IDbTransaction<T> Create<T>(Transaction transaction) where T : IEntity
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

    #endregion Public Methods

    #region Private Methods

    private IDbTransaction<TEntity> Create<TEntity, TFirestoreEntity>(Transaction transaction)
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      CollectionReference collection = firestoreDb.Collection(CollectionReferenceHelper.GetCollectionPath<TEntity>());
      var firestoreDbTransaction = new FirestoreDbTransaction<TFirestoreEntity>(collection, transaction);
      return new FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity>(firestoreDbTransaction, mapper);
    }

    #endregion Private Methods
  }
}