using PruneUrl.Backend.Application.Interfaces.Providers;
using System.Diagnostics.CodeAnalysis;

namespace PruneUrl.Backend.Application.Implementation.Providers
{
  /// <summary>
  /// A provider of <see cref="DateTime" /> values.
  /// </summary>
  [ExcludeFromCodeCoverage(Justification = "Implements an abstraction to aid with testing, so is itself hard to test!")]
  public sealed class DateTimeProvider : IDateTimeProvider
  {
    #region Public Methods

    /// <inheritdoc cref="IDateTimeProvider.GetNow" />
    public DateTime GetNow()
    {
      return DateTime.Now;
    }

    #endregion Public Methods
  }
}