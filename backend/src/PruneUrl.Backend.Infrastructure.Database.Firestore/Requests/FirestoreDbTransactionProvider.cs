using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Interfaces;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;

/// <summary>
/// <para>
/// A Firestore specific provider for <see cref="IDbTransaction{T}" /> via the injection of an
/// asynchronous callback function whose argument is a <see cref="IDbTransaction{T}" /> to be ran
/// by the provider. The callback is ran as many times as needed to ensure the transaction is
/// executed successfully, and if it fails it will rerun the callback as many times as indicated
/// by <see cref="MaxAttempts" />.
/// </para>
/// <para>
/// This acts as a wrapper/facade around <see cref="Transaction" /> and <see cref="FirestoreDb" />.
/// </para>
/// </summary>
/// <param name="firestoreDb">
/// The <see cref="FirestoreDb" /> instance for creating transactions against.
/// </param>
/// <param name="firestoreDbTransactionFactory">
/// The factory for creating <see cref="IDbTransaction{T}" /> instances.
/// </param>
/// <param name="firestoreTransactionOptions">
/// The <see cref="IOptions{TOptions}" /> used to determine the <see cref="MaxAttempts" /> by
/// retrieving a configured <see cref="FirestoreTransactionOptions" />
/// </param>
public sealed class FirestoreDbTransactionProvider(
  FirestoreDb firestoreDb,
  IFirestoreDbTransactionFactory firestoreDbTransactionFactory,
  IOptions<FirestoreTransactionOptions> firestoreTransactionOptions
) : IDbTransactionProvider
{
  private readonly FirestoreDb firestoreDb = firestoreDb;
  private readonly IFirestoreDbTransactionFactory firestoreDbTransactionFactory =
    firestoreDbTransactionFactory;

  /// <inheritdoc cref="IDbTransactionProvider.MaxAttempts" />
  public int MaxAttempts { get; } = firestoreTransactionOptions.Value.MaxAttempts;

  /// <inheritdoc cref="IDbTransactionProvider.RunTransactionAsync{T}(Func{IDbTransaction{T}, Task}, CancellationToken)" />
  public Task<T> RunTransactionAsync<T>(
    Func<IDbTransaction<T>, Task<T>> callback,
    CancellationToken cancellationToken = default
  )
    where T : IEntity
  {
    TransactionOptions transactionOptions = TransactionOptions.ForMaxAttempts(MaxAttempts);
    return firestoreDb.RunTransactionAsync(
      transaction =>
      {
        IDbTransaction<T> dbTransaction = firestoreDbTransactionFactory.Create<T>(transaction);
        return callback(dbTransaction);
      },
      transactionOptions,
      cancellationToken
    );
  }
}
