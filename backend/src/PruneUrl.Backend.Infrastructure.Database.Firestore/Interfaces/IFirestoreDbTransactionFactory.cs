using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// Defines a firestore specific factory for creating <see cref="IDbTransaction{T}" /> instances.
/// </summary>
public interface IFirestoreDbTransactionFactory
{
  /// <summary>
  /// Creates a new instance of a <see cref="IDbTransaction{T}" /> implementation using a <see
  /// cref="Transaction" />.
  /// </summary>
  /// <typeparam name="T"> The <see cref="IEntity" /> type for the transaction. </typeparam>
  /// <param name="transaction">
  /// The <see cref="Transaction" /> instance to use when creating a <see cref="IDbTransaction{T}"
  /// /> instance.
  /// </param>
  /// <returns> A new instance of a <see cref="IDbTransaction{T}" />. </returns>
  IDbTransaction<T> Create<T>(Transaction transaction)
    where T : IEntity;
}
