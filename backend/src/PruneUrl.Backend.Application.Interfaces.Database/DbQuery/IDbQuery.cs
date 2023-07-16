using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.DbQuery
{
  /// <summary>
  /// Defines a "query", or read-only, operation which can be performed on an underlying database.
  /// </summary>
  /// <typeparam name="T"> The <see cref="IEntity" /> the query is concerned with. </typeparam>
  public interface IDbQuery<T> where T : IEntity
  {
    #region Public Methods

    /// <summary>
    /// Asynchronously retrieves a <typeparamref name="T" /> by its unique id.
    /// </summary>
    /// <param name="id"> The unique id. </param>
    /// <param name="cancellationToken"> The cancellation token for the asynchronous operation. </param>
    /// <returns>
    /// A task representing operation of returning a <typeparamref name="T" /> instance if found, or
    /// <c> null </c> if not found.
    /// </returns>
    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    #endregion Public Methods
  }
}