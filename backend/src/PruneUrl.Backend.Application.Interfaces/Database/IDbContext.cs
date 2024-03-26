using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces;

/// <summary>
/// Defines the <see cref="DbContext" which will be used to interact with the underlying database.
/// </summary>
public interface IDbContext
{
  /// <summary>
  /// The <see cref="DbSet{TEntity}"/> for <see cref="ShortUrl"/>'s.
  /// </summary>
  DbSet<ShortUrl> ShortUrls { get; set; }

  /// <summary>
  /// Begins tracking the given entity and entries reachable from the given entity using the <see
  /// cref="EntityState.Unchanged" /> state by default, but see below for cases when a different
  /// state will be used.
  /// </summary>
  /// <typeparam name="TEntity"> The type of the entity. </typeparam>
  /// <param name="entity"> The entity to attach. </param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}" /> for the entity. The entry provides access to change
  /// tracking information and operations for the entity.
  /// </returns>
  /// <remarks>
  /// <para>
  /// Generally, no database interaction will be performed until <see cref="SaveChangesAsync" />
  /// is called.
  /// </para>
  /// <para>
  /// A recursive search of the navigation properties will be performed to find reachable entities
  /// that are not already being tracked by the context. All entities found will be tracked by the context.
  /// </para>
  /// <para>
  /// For entity types with generated keys if an entity has its primary key value set then it will
  /// be tracked in the <see cref="EntityState.Unchanged" /> state. If the primary key value is
  /// not set then it will be tracked in the <see cref="EntityState.Added" /> state. This helps
  /// ensure only new entities will be inserted. An entity is considered to have its primary key
  /// value set if the primary key property is set to anything other than the CLR default for the
  /// property type.
  /// </para>
  /// <para>
  /// For entity types without generated keys, the state set is always <see
  /// cref="EntityState.Unchanged" />.
  /// </para>
  /// </remarks>
  public EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
    where TEntity : class;

  /// <summary>
  /// Saves all changes made in this context to the database asynchronously.
  /// </summary>
  /// <param name="cancellationToken">
  /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
  /// </param>
  /// <returns>
  /// A task that represents the asynchronous save operation. The task result contains the number
  /// of state entries written to the database.
  /// </returns>
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
