using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// An adapter for <see cref="IDbTransaction{T}" /> which converts a <typeparamref name="TEntity"
/// /> input into a <typeparamref name="TFirestoreEntity" /> that can be used for writing to the
/// firestore database via the SDK.
/// </summary>
/// <typeparam name="TEntity"> The <see cref="IEntity" /> the app uses. </typeparam>
/// <typeparam name="TFirestoreEntity">
/// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbTransaction{T}" /> uses.
/// </typeparam>
/// <param name="firestoreDbTransaction"> The adaptee <see cref="IDbTransaction{T}" />. </param>
/// <param name="mapper">
/// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
/// />'s and the core <see cref="IEntity" />'s.
/// </param>
internal sealed class FirestoreDbTransactionAdapter<TEntity, TFirestoreEntity>(
  IDbTransaction<TFirestoreEntity> firestoreDbTransaction,
  IMapper mapper
) : IDbTransaction<TEntity>
  where TEntity : IEntity
  where TFirestoreEntity : FirestoreEntityDTO
{
  private readonly IDbTransaction<TFirestoreEntity> firestoreDbTransaction = firestoreDbTransaction;
  private readonly IMapper mapper = mapper;

  /// <inheritdoc cref="IDbGetByIdOperation{T}.GetByIdAsync(string, CancellationToken)" />
  public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
  {
    TFirestoreEntity? firestoreEntity = await firestoreDbTransaction.GetByIdAsync(
      id,
      cancellationToken
    );
    return firestoreEntity != null
      ? mapper.Map<TFirestoreEntity, TEntity>(firestoreEntity)
      : default;
  }

  /// <inheritdoc cref="IDbUpdateOperation{T}.Update(T)" />
  public void Update(TEntity entity)
  {
    TFirestoreEntity firestoreEntity = mapper.Map<TEntity, TFirestoreEntity>(entity);
    firestoreDbTransaction.Update(firestoreEntity);
  }
}
