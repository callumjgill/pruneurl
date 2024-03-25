using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// An adapter for <see cref="IDbWriteBatch{T}" /> which converts a <typeparamref name="TEntity"
/// /> input into a <typeparamref name="TFirestoreEntity" /> that can be used for writing to the
/// firestore database via the SDK.
/// </summary>
/// <typeparam name="TEntity"> The <see cref="IEntity" /> the app uses. </typeparam>
/// <typeparam name="TFirestoreEntity">
/// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbWriteBatch{T}" /> uses.
/// </typeparam>
/// <param name="firestoreDbWriteBatch"> The adaptee <see cref="IDbWriteBatch{T}" />. </param>
/// <param name="mapper">
/// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
/// />'s and the core <see cref="IEntity" />'s.
/// </param>
internal sealed class FirestoreDbWriteBatchAdapter<TEntity, TFirestoreEntity>(
  IDbWriteBatch<TFirestoreEntity> firestoreDbWriteBatch,
  IMapper mapper
) : IDbWriteBatch<TEntity>
  where TEntity : IEntity
  where TFirestoreEntity : FirestoreEntityDTO
{
  private readonly IDbWriteBatch<TFirestoreEntity> firestoreDbWriteBatch = firestoreDbWriteBatch;
  private readonly IMapper mapper = mapper;

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
}
