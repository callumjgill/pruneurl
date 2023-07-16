using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.DbQuery
{
  /// <summary>
  /// Defines a factory for creating <see cref="IDbQuery{T}" /> instances.
  /// </summary>
  public interface IDbQueryFactory
  {
    #region Public Methods

    /// <summary>
    /// Creates a new <see cref="IDbQuery{T}" /> instance.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IEntity" /> the query is concerned with. </typeparam>
    /// <returns> A new <see cref="IDbQuery{T}" /> instance. </returns>
    IDbQuery<T> Create<T>() where T : IEntity;

    #endregion Public Methods
  }
}