using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Requests
{
  /// <summary>
  /// An adapter for <see cref="IDbWriteBatch{T}" /> which converts a <typeparamref name="TEntity"
  /// /> input into a <typeparamref name="TFirestoreEntity" /> that can be used for writing to the
  /// firestore database via the SDK.
  /// </summary>
  /// <typeparam name="TEntity"> The <see cref="IEntity" /> the app uses. </typeparam>
  /// <typeparam name="TFirestoreEntity">
  /// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbWriteBatch{T}" /> uses.
  /// </typeparam>
  internal sealed class FirestoreDbWriteBatchAdapter<TEntity, TFirestoreEntity> : IDbWriteBatch<TEntity>
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly IDbWriteBatch<TFirestoreEntity> firestoreDbWriteBatch;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbWriteBatchAdapter{TEntity,
    /// TFirestoreEntity}" /> class.
    /// </summary>
    /// <param name="firestoreDbWriteBatch"> The adaptee <see cref="IDbWriteBatch{T}" />. </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbWriteBatchAdapter(IDbWriteBatch<TFirestoreEntity> firestoreDbWriteBatch, IMapper mapper)
    {
      this.firestoreDbWriteBatch = firestoreDbWriteBatch;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbWriteBatch{T}.CommitAsync(CancellationToken)" />
    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
      return firestoreDbWriteBatch.CommitAsync(cancellationToken);
    }

    /// <inheritdoc cref="IDbCreateOperation{T}.Create(T)" />
    public void Create(TEntity entity)
    {
      TFirestoreEntity firestoreEntity = mapper.Map<TEntity, TFirestoreEntity>(entity);
      firestoreDbWriteBatch.Create(firestoreEntity);
    }

    /// <inheritdoc cref="IDbDeleteOperation.Delete(string)" />
    public void Delete(string id)
    {
      firestoreDbWriteBatch.Delete(id);
    }

    #endregion Public Methods
  }
}