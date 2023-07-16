namespace PruneUrl.Backend.Domain.Entities
{
  /// <summary>
  /// An <see cref="IEntity" /> encapsulating a "sequence id". This is an integer value which is
  /// updated each time a new short url is created.
  /// </summary>
  /// <param name="Id"> The unique identifier for the sequence id entity. </param>
  /// <param name="Value">
  /// The actual value of the sequence id entity, i.e. the actual "sequence id".
  /// </param>
  public sealed record SequenceId(string Id, int Value) : IEntity
  {
  }
}