namespace PruneUrl.Backend.Application.Exceptions.Database;

/// <summary>
/// An exception to be thrown when a particular entity must be found in the database but it wasn't.
/// </summary>
/// <param name="entityType"> The type of entity that wasn't found. </param>
/// <param name="id"> The id of the entity that wasn't found. </param>
public sealed class EntityNotFoundException(Type entityType, string id)
  : Exception($"Entity of type {entityType} with id {id} was not found!") { }
