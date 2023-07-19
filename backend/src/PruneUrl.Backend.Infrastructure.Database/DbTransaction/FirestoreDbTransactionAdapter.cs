using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction
{
  /// <summary>
  /// An adapter which converts a <typeparamref name="TEntity" /> input into a <typeparamref
  /// name="TFirestoreEntity" /> that can be used for writing to the firestore database via the SDK.
  /// </summary>
  /// <typeparam name="TEntity"> The <see cref="IEntity" /> the app uses. </typeparam>
  /// <typeparam name="TFirestoreEntity">
  /// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbTransaction{T}" /> uses.
  /// </typeparam>
  internal class FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity> : IDbTransaction<TEntity>
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly IMapper mapper;
    private IDbTransaction<TFirestoreEntity> firestoreDbTransaction;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbTransactionAdapter{TEntity,
    /// TFirestoreEntity}" /> class.
    /// </summary>
    /// <param name="firestoreDbTransaction"> The adaptee <see cref="IDbTransaction{T}" />. </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbTransactionAdapter(IDbTransaction<TFirestoreEntity> firestoreDbTransaction, IMapper mapper)
    {
      this.firestoreDbTransaction = firestoreDbTransaction;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbTransaction{T}.CommitAsync(CancellationToken)" />
    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
      return firestoreDbTransaction.CommitAsync(cancellationToken);
    }

    /// <inheritdoc cref="IDbTransaction{T}.Create(T)" />
    public IDbTransaction<TEntity> Create(TEntity entity)
    {
      TFirestoreEntity firestoreEntity = mapper.Map<TEntity, TFirestoreEntity>(entity);
      firestoreDbTransaction = firestoreDbTransaction.Create(firestoreEntity);
      return this;
    }

    /// <inheritdoc cref="IDbTransaction{T}.Delete(string)" />
    public IDbTransaction<TEntity> Delete(string id)
    {
      firestoreDbTransaction = firestoreDbTransaction.Delete(id);
      return this;
    }

    #endregion Public Methods
  }
}