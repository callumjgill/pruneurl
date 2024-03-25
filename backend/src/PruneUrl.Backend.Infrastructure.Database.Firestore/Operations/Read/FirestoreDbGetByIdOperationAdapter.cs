using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// An adapter for <see cref="IDbGetByIdOperation{T}" /> which converts a <typeparamref
/// name="TFirestoreEntity" /> result into a <typeparamref name="TEntity" /> that can be used by
/// the wider application
/// </summary>
/// <typeparam name="TEntity"> The <see cref="IEntity" /> the app will use. </typeparam>
/// <typeparam name="TFirestoreEntity">
/// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbGetByIdOperation{T}" /> uses.
/// </typeparam>
/// <param name="firestoreDbGetByIdOperation">
/// The adaptee <see cref="IDbGetByIdOperation{T}" />.
/// </param>
/// <param name="mapper">
/// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
/// />'s and the core <see cref="IEntity" />'s.
/// </param>
internal sealed class FirestoreDbGetByIdOperationAdapter<TEntity, TFirestoreEntity>(
  IDbGetByIdOperation<TFirestoreEntity> firestoreDbGetByIdOperation,
  IMapper mapper
) : IDbGetByIdOperation<TEntity>
  where TEntity : IEntity
  where TFirestoreEntity : FirestoreEntityDTO
{
  private readonly IDbGetByIdOperation<TFirestoreEntity> firestoreDbGetByIdOperation =
    firestoreDbGetByIdOperation;
  private readonly IMapper mapper = mapper;

  /// <inheritdoc cref="IDbGetByIdOperation{T}.GetByIdAsync(string, CancellationToken)" />
  public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
  {
    TFirestoreEntity? firestoreEntity = await firestoreDbGetByIdOperation.GetByIdAsync(
      id,
      cancellationToken
    );
    return firestoreEntity != null
      ? mapper.Map<TFirestoreEntity, TEntity>(firestoreEntity)
      : default;
  }
}
