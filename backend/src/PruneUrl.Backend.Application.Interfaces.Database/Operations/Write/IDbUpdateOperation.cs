using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.Operations.Write;

/// <summary>
/// Defines an operation for updating a <typeparamref name="T" /> in the underlying database.
/// </summary>
/// <typeparam name="T"> The <see cref="IEntity" /> the operation is concerned with. </typeparam>
public interface IDbUpdateOperation<T>
  where T : IEntity
{
  /// <summary>
  /// Performs the operation of updating a <typeparamref name="T" />.
  /// </summary>
  /// <param name="entity"> The <typeparamref name="T" /> entity to create. </param>
  /// <remarks>
  /// It is up to the implementation to either commit this operation immediately or perform it later.
  /// </remarks>
  void Update(T entity);
}
