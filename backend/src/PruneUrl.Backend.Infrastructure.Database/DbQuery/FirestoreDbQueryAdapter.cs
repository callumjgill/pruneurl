using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database.DbQuery;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbQuery
{
  /// <summary>
  /// An adapter which converts a <typeparamref name="TFirestoreEntity" /> result into a
  /// <typeparamref name="TEntity" /> that can be used by the wider application
  /// </summary>
  /// <typeparam name="TEntity"> The <see cref="IEntity" /> the app will use. </typeparam>
  /// <typeparam name="TFirestoreEntity">
  /// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbQuery{T}" /> uses.
  /// </typeparam>
  internal sealed class FirestoreDbQueryAdapter<TEntity, TFirestoreEntity> : IDbQuery<TEntity>
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly IDbQuery<TFirestoreEntity> firestoreDbQuery;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbQueryAdapter{TEntity,
    /// TFirestoreEntity}" /> class.
    /// </summary>
    /// <param name="firestoreDbQuery"> The adaptee <see cref="IDbQuery{T}" />. </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbQueryAdapter(IDbQuery<TFirestoreEntity> firestoreDbQuery, IMapper mapper)
    {
      this.firestoreDbQuery = firestoreDbQuery;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbQuery{T}.GetByIdAsync(string, CancellationToken)" />
    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
      TFirestoreEntity? firestoreEntity = await firestoreDbQuery.GetByIdAsync(id, cancellationToken);
      return firestoreEntity != null ? mapper.Map<TFirestoreEntity, TEntity>(firestoreEntity) : default;
    }

    #endregion Public Methods
  }
}