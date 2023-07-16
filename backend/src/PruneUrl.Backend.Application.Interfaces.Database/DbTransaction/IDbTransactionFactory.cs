using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.DbTransaction
{
  /// <summary>
  /// Defines a factory for creating <see cref="IDbTransaction{T}" /> instances.
  /// </summary>
  public interface IDbTransactionFactory
  {
    #region Public Methods

    /// <summary>
    /// Creates a new <see cref="IDbTransaction{T}" /> instance.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IEntity" /> the transaction is concerned with. </typeparam>
    /// <returns> A new <see cref="IDbTransaction{T}" /> instance. </returns>
    IDbTransaction<T> Create<T>() where T : IEntity;

    #endregion Public Methods
  }
}