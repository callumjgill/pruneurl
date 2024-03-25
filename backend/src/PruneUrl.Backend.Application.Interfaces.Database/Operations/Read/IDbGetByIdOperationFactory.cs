using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.Operations.Read
{
  /// <summary>
  /// Defines a factory for creating <see cref="IDbGetByIdOperation{T}" /> instances.
  /// </summary>
  public interface IDbGetByIdOperationFactory
  {
    /// <summary>
    /// Creates a new <see cref="IDbGetByIdOperation{T}" /> instance.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IEntity" /> the operation is concerned with. </typeparam>
    /// <returns> A new <see cref="IDbGetByIdOperation{T}" /> instance. </returns>
    IDbGetByIdOperation<T> Create<T>()
      where T : IEntity;
  }
}
