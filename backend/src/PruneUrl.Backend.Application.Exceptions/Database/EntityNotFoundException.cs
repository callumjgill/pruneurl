using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Exceptions;

/// <summary>
/// An exception to be thrown when a particular entity must be found in the database but it wasn't.
/// </summary>
/// <param name="criteria">The criteria upon which the entity was not found</param>
public sealed class EntityNotFoundException<TEntity>(string criteria)
  : Exception($"Entity of type {typeof(TEntity)} was not found! {criteria}")
  where TEntity : Entity { }
