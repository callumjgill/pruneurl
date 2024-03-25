using System.Diagnostics.CodeAnalysis;
using PruneUrl.Backend.Application.Interfaces.Providers;

namespace PruneUrl.Backend.Application.Implementation.Providers;

/// <summary>
/// A provider of <see cref="DateTime" /> values.
/// </summary>
[ExcludeFromCodeCoverage(
  Justification = "Implements an abstraction to aid with testing, so is itself hard to test!"
)]
public sealed class DateTimeProvider : IDateTimeProvider
{
  /// <inheritdoc cref="IDateTimeProvider.GetNow" />
  public DateTime GetNow()
  {
    return DateTime.Now;
  }
}
