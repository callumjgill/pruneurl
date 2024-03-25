using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database;

/// <summary>
/// Defines a "batch" of write-only operations which can be performed on an underlying database.
/// This allows for multiple mutations to be applied in a single commit to the underlying
/// database, rather than individually.
/// </summary>
/// <typeparam name="T"> The <see cref="IEntity" /> the batch is concerned with. </typeparam>
public interface IDbWriteBatch<T> : IDbCreateOperation<T>, IDbDeleteOperation
  where T : IEntity
{
  /// <summary>
  /// Aschronously commit the batch on the underlying database.
  /// </summary>
  /// <param name="cancellationToken"> The cancellation token for the asynchronous operation. </param>
  /// <returns>
  /// A task representing the asychronous operation of commiting the batch to the server.
  /// </returns>
  Task CommitAsync(CancellationToken cancellationToken = default);
}
