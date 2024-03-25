using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Exceptions
{
  /// <summary>
  /// An exception which is thrown when mappings between <see cref="IEntity" /> and <see
  /// cref="FirestoreEntityDTO" /> are invalid.
  /// </summary>
  /// <param name="invalidType"> The type of <see cref="IEntity" /> which is invalid. </param>
  public sealed class InvalidEntityTypeMapException(Type invalidType)
    : Exception($"No mapping exists for the type {invalidType}!") { }
}
