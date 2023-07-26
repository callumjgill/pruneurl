using PruneUrl.Backend.Application.Interfaces.Providers;
using System;

namespace PruneUrl.Backend.Application.Implementation.Providers
{
  /// <summary>
  /// A implementation of <see cref="IEntityIdProvider" /> which produces <see cref="Guid" /> id
  /// string values.
  /// </summary>
  public sealed class GuidEntityIdProvider : IEntityIdProvider
  {
    #region Public Methods

    /// <inheritdoc cref="IEntityIdProvider.NewId()" />
    public string NewId()
    {
      return Guid.NewGuid().ToString();
    }

    #endregion Public Methods
  }
}