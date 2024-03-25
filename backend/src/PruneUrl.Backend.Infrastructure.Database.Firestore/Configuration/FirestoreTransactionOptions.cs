using PruneUrl.Backend.Application.Interfaces.Database;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore;

/// <summary>
/// A configuration DTO intended to be used in the options pattern. This is injected into classes
/// via the options pattern. The configuration is used in conjunction with a <see
/// cref="IDbTransactionProvider" />.
/// </summary>
public sealed class FirestoreTransactionOptions
{
  /// <summary>
  /// The number of times the transaction will be attempted before failing.
  /// </summary>
  public int MaxAttempts { get; set; }
}
