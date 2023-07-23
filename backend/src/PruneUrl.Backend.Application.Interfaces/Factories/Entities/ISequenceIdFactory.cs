using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Factories.Entities
{
  /// <summary>
  /// Defines a factory for creating <see cref="SequenceId" /> instances.
  /// </summary>
  public interface ISequenceIdFactory
  {
    #region Public Methods

    /// <summary>
    /// Creates a new <see cref="SequenceId" /> instance given a <paramref name="sequenceId" />.
    /// </summary>
    /// <param name="sequenceId">
    /// The sequence id value the <see cref="SequenceId" /> instance will encapsulate.
    /// </param>
    /// <returns>
    /// A new <see cref="SequenceId" /> instance. This will have a new unique ID and not reference
    /// an existing "sequence id".
    /// </returns>
    SequenceId Create(int sequenceId);

    /// <summary>
    /// Creates a new <see cref="SequenceId" /> instance given an existing <paramref
    /// name="sequenceId" /> and a <paramref name="newSequenceIdValue" /> to set.
    /// </summary>
    /// <param name="sequenceId">
    /// The sequence id value the <see cref="SequenceId" /> instance will encapsulate.
    /// </param>
    /// <param name="newSequenceIdValue"> </param>
    /// <returns>
    /// A new <see cref="SequenceId" /> instance. This will have a the same unique ID as the
    /// existing one.
    /// </returns>
    SequenceId CreateFromExisting(SequenceId sequenceId, int newSequenceIdValue);

    #endregion Public Methods
  }
}