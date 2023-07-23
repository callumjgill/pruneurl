﻿using AutoMapper;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read
{
  /// <summary>
  /// An adapter for <see cref="IDbGetByIdOperation{T}" /> which converts a <typeparamref
  /// name="TFirestoreEntity" /> result into a <typeparamref name="TEntity" /> that can be used by
  /// the wider application
  /// </summary>
  /// <typeparam name="TEntity"> The <see cref="IEntity" /> the app will use. </typeparam>
  /// <typeparam name="TFirestoreEntity">
  /// The <see cref="FirestoreEntityDTO" /> the internal <see cref="IDbGetByIdOperation{T}" /> uses.
  /// </typeparam>
  internal sealed class FirestoreDbGetByIdOperationAdapter<TEntity, TFirestoreEntity> : IDbGetByIdOperation<TEntity>
    where TEntity : IEntity
    where TFirestoreEntity : FirestoreEntityDTO
  {
    #region Private Fields

    private readonly IDbGetByIdOperation<TFirestoreEntity> firestoreDbGetByIdOperation;
    private readonly IMapper mapper;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbGetByIdOperationAdapter{TEntity,
    /// TFirestoreEntity}" /> class.
    /// </summary>
    /// <param name="firestoreDbGetByIdOperation">
    /// The adaptee <see cref="IDbGetByIdOperation{T}" />.
    /// </param>
    /// <param name="mapper">
    /// The <see cref="IMapper" /> service for converting between the <see cref="FirestoreEntityDTO"
    /// />'s and the core <see cref="IEntity" />'s.
    /// </param>
    public FirestoreDbGetByIdOperationAdapter(IDbGetByIdOperation<TFirestoreEntity> firestoreDbGetByIdOperation, IMapper mapper)
    {
      this.firestoreDbGetByIdOperation = firestoreDbGetByIdOperation;
      this.mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbGetByIdOperation{T}.GetByIdAsync(string, CancellationToken)" />
    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
      TFirestoreEntity? firestoreEntity = await firestoreDbGetByIdOperation.GetByIdAsync(id, cancellationToken);
      return firestoreEntity != null ? mapper.Map<TFirestoreEntity, TEntity>(firestoreEntity) : default;
    }

    #endregion Public Methods
  }
}