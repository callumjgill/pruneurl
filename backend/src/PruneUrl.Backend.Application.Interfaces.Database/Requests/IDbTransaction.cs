using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Write;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.Requests
{
  /// <summary>
  /// Defines a "transaction" which is a "batch" of read and write operations which can be performed
  /// on the database. The read operations can only be performed BEFORE the write operations. This
  /// is useful for situations where you need to read a value to perform a corresponding write operation.
  /// </summary>
  /// <typeparam name="T"> The <see cref="IEntity" /> the transaction is concerned with. </typeparam>
  public interface IDbTransaction<T> : IDbGetByIdOperation<T>, IDbUpdateOperation<T> where T : IEntity
  {
  }
}