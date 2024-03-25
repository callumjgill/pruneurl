using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.Requests
{
  /// <summary>
  /// Defines a provider for <see cref="IDbTransaction{T}" /> via the injection of an asynchronous
  /// callback function whose argument is a <see cref="IDbTransaction{T}" /> to be ran by the
  /// provider. This allows the provider to run the callback as many times as needed to ensure the
  /// transaction is executed successfully, and if it fails it will rerun the callback as many times
  /// as indicated by <see cref="MaxAttempts" />.
  /// </summary>
  public interface IDbTransactionProvider
  {
    /// <summary>
    /// The number of times the transaction will be attempted before failing.
    /// </summary>
    int MaxAttempts { get; }

    /// <summary>
    /// Runs a transaction asychronously, with an asynchronous callback with no return value. The
    /// specified callback is executed for a newly-created transaction. If committing the
    /// transaction fails, the whole operation is retried based on <see cref="MaxAttempts" />.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IEntity" /> </typeparam>
    /// <param name="callback"> The callback to execute. </param>
    /// <param name="cancellationToken"> A cancellation token for the operation. </param>
    /// <returns>
    /// A task which completes when the transaction has committed with the result being a
    /// <typeparamref name="T" /> type.
    /// </returns>
    Task<T> RunTransactionAsync<T>(
      Func<IDbTransaction<T>, Task<T>> callback,
      CancellationToken cancellationToken = default
    )
      where T : IEntity;
  }
}
