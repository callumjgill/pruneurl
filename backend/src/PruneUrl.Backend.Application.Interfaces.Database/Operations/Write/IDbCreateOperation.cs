using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database;

/// <summary>
/// Defines an operation for writing a <typeparamref name="T" /> to the underlying database.
/// </summary>
/// <typeparam name="T"> The <see cref="IEntity" /> the operation is concerned with. </typeparam>
public interface IDbCreateOperation<T>
  where T : IEntity
{
  /// <summary>
  /// Performs the operation of creating a document.
  /// </summary>
  /// <param name="entity"> The <typeparamref name="T" /> entity to create. </param>
  /// <remarks>
  /// It is up to the implementation to either commit this operation immediately or perform it later.
  /// </remarks>
  void Create(T entity);
}
