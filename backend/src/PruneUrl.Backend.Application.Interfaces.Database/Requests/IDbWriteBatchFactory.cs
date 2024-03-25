using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.Requests;

/// <summary>
/// Defines a factory for creating <see cref="IDbWriteBatch{T}" /> instances.
/// </summary>
public interface IDbWriteBatchFactory
{
  /// <summary>
  /// Creates a new <see cref="IDbWriteBatch{T}" /> instance.
  /// </summary>
  /// <typeparam name="T"> The <see cref="IEntity" /> the batch is concerned with. </typeparam>
  /// <returns> A new <see cref="IDbWriteBatch{T}" /> instance. </returns>
  IDbWriteBatch<T> Create<T>()
    where T : IEntity;
}
