using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Factories.Entities;

/// <summary>
/// Defines a factory for creating <see cref="SequenceId" /> instances.
/// </summary>
public interface ISequenceIdFactory
{
  /// <summary>
  /// Creates a new <see cref="SequenceId" /> instance given a <paramref name="sequenceId" />.
  /// </summary>
  /// <param name="id"> The unique identifier for the <see cref="SequenceId" /> to use. </param>
  /// <param name="sequenceId">
  /// The sequence id value the <see cref="SequenceId" /> instance will encapsulate.
  /// </param>
  /// <returns>
  /// A new <see cref="SequenceId" /> instance. This will have a new unique ID and not reference
  /// an existing "sequence id".
  /// </returns>
  SequenceId Create(string id, int sequenceId);
}
