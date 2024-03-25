using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Implementation.Factories.Entities
{
  /// <summary>
  /// A factory for creating <see cref="SequenceId" /> instances.
  /// </summary>
  public sealed class SequenceIdFactory : ISequenceIdFactory
  {
    /// <inheritdoc cref="ISequenceIdFactory.Create(string, int)" />
    public SequenceId Create(string id, int sequenceId)
    {
      AssertSequenceId(sequenceId);
      return new SequenceId(id, sequenceId);
    }

    private void AssertSequenceId(int sequenceId)
    {
      if (sequenceId < 0)
      {
        throw new ArgumentException("Sequence Id cannot be less than 0!", nameof(sequenceId));
      }
    }
  }
}
