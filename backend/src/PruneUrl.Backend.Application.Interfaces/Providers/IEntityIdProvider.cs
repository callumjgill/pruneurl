using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Providers
{
  /// <summary>
  /// Defines a provider for unique identifiers for <see cref="IEntity" />'s.
  /// </summary>
  public interface IEntityIdProvider
  {
    #region Public Methods

    /// <summary>
    /// Creates and returns a new unique identifier.
    /// </summary>
    /// <returns> A <see cref="string" /> representing the unqiue identifier. </returns>
    string NewId();

    #endregion Public Methods
  }
}