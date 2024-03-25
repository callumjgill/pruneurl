namespace PruneUrl.Backend.Domain.Entities
{
  /// <summary>
  /// Defines an "entity" which represets a core concept within the backend application. This is a
  /// pure data structure, and implementations should be immutable. Each entity is like a
  /// "reference" to a thing which is primarily referenced by its id. If multiple entities of the
  /// same type with the same id are found in memory, they're refering to the same encapsualted domain.
  /// </summary>
  public interface IEntity
  {
    /// <summary>
    /// The unique identifier for the <see cref="IEntity" />.
    /// </summary>
    public string Id { get; }
  }
}
