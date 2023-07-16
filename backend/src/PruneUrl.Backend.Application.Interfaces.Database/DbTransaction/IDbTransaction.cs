using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.DbTransaction
{
  /// <summary>
  /// Defines a "transaction", or write-only, operation which can be performed on an underlying
  /// database. This allows for multiple mutations to be applied in a single commit to the
  /// underlying database, rather than individually.
  /// </summary>
  /// <typeparam name="T"> The <see cref="IEntity" /> the transaction is concerned with. </typeparam>
  public interface IDbTransaction<T> where T : IEntity
  {
    #region Public Methods

    /// <summary>
    /// Aschronously commit the transaction on the underlying database.
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token for the asynchronous operation. </param>
    /// <returns>
    /// A task representing the asychronous operation of commiting the transaction to the server.
    /// </returns>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds an operation to create a document in this transaction.
    /// </summary>
    /// <param name="entity"> The <typeparamref name="T" /> entity to create. </param>
    /// <returns> A <see cref="IDbTransaction{T}" /> with the delete operation applied. </returns>
    IDbTransaction<T> Create(T entity);

    /// <summary>
    /// Adds an operation to delete a document in this transaction.
    /// </summary>
    /// <param name="id"> The unique identifier of the <typeparamref name="T" /> entity to delete. </param>
    /// <returns> A <see cref="IDbTransaction{T}" /> with the delete operation applied. </returns>
    IDbTransaction<T> Delete(string id);

    #endregion Public Methods
  }
}