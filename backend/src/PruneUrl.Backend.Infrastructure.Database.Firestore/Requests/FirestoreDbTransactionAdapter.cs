using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Write;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Requests
{
  /// <summary>
  /// An adapter for <see cref="IDbTransaction{T}" /> which converts a <typeparamref name="TEntity"
  /// /> input into a <typeparamref name="TFirestoreEntity" /> that can be used for writing to the
  /// firestore database via the SDK.
  /// </summary>
  /// <typeparam name="TEntity"> The <see cref="IEntity" /> the app uses. </typeparam>
  /// <typeparam name="TFirestoreEntity">
  /// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbTransaction{T}" /> uses.
  /// </typeparam>
  internal sealed class FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity> : IDbTransaction<TEntity>
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly IDbTransaction<TFirestoreEntity> firestoreDbTransaction;
    private readonly IMapper mapper;

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

    /// <inheritdoc cref="IDbGetByIdOperation{T}.GetByIdAsync(string, CancellationToken)" />
    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
      TFirestoreEntity? firestoreEntity = await firestoreDbTransaction.GetByIdAsync(id, cancellationToken);
      return firestoreEntity != null ? mapper.Map<TFirestoreEntity, TEntity>(firestoreEntity) : default;
    }

    /// <inheritdoc cref="IDbUpdateOperation{T}.Update(T)" />
    public void Update(TEntity entity)
    {
      TFirestoreEntity firestoreEntity = mapper.Map<TEntity, TFirestoreEntity>(entity);
      firestoreDbTransaction.Update(firestoreEntity);
    }

    #endregion Public Methods
  }
}