using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Implementation.Factories.Entities
{
  /// <summary>
  /// A factory for creating <see cref="SequenceId" /> instances.
  /// </summary>
  public sealed class SequenceIdFactory : ISequenceIdFactory
  {
    #region Private Fields

    private readonly IEntityIdProvider entityIdProvider;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="SequenceIdFactory" /> class.
    /// </summary>
    /// <param name="entityIdProvider">
    /// The provider for unique identifiers for <see cref="SequenceId" />'s.
    /// </param>
    public SequenceIdFactory(IEntityIdProvider entityIdProvider)
    {
      this.entityIdProvider = entityIdProvider;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="ISequenceIdFactory.Create(int)" />
    public SequenceId Create(int sequenceId)
    {
      AssertSequenceId(sequenceId);
      string id = entityIdProvider.NewId();
      return new SequenceId(id, sequenceId);
    }

    /// <inheritdoc cref="ISequenceIdFactory.Create(SequenceId, int)" />
    public SequenceId CreateFromExisting(SequenceId sequenceId, int newSequenceIdValue)
    {
      AssertSequenceId(newSequenceIdValue);
      return new SequenceId(sequenceId.Id, newSequenceIdValue);
    }

    #endregion Public Methods

    #region Private Methods

    private void AssertSequenceId(int sequenceId)
    {
      if (sequenceId < 0)
      {
        throw new ArgumentException("Sequence Id cannot be less than 0!", nameof(sequenceId));
      }
    }

    #endregion Private Methods
  }
}