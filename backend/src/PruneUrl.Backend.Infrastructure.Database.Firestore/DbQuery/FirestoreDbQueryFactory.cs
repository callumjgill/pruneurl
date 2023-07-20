using AutoMapper;
using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.DbQuery;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbQuery
{
  /// <summary>
  /// Defines a factory for creating <see cref="IDbQuery{T}" /> ( <see cref="FirestoreDbQuery{T}"
  /// />) instances.
  /// </summary>
  public sealed class FirestoreDbQueryFactory : IDbQueryFactory
  {
    #region Private Fields

    private readonly FirestoreDb firestoreDb;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbQueryFactory" /> class.
    /// </summary>
    /// <param name="firestoreDb">
    /// The <see cref="FirestoreDb" /> instance for creating queries against.
    /// </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbQueryFactory(FirestoreDb firestoreDb, IMapper mapper)
    {
      this.firestoreDb = firestoreDb;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbQueryFactory.Create{T}" />
    public IDbQuery<T> Create<T>() where T : IEntity
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

    private IDbQuery<TEntity> Create<TEntity, TFirestoreEntity>()
      where TEntity : IEntity
      where TFirestoreEntity : FirestoreEntityDTO
    {
      CollectionReference collection = firestoreDb.Collection(CollectionReferenceHelper.GetCollectionPath<TEntity>());
      var firestoreDbQuery = new FirestoreDbQuery<TFirestoreEntity>(collection);
      return new FirestoreDbQueryAdapter<TEntity, TFirestoreEntity>(firestoreDbQuery, mapper);
    }

    #endregion Private Methods
  }
}