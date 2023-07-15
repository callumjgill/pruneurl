﻿namespace PruneUrl.Backend.Application.Interfaces.Providers
{
  /// <summary>
  /// Defines a provider of <see cref="DateTime" /> values. This is used to assist with testing of
  /// <see cref="DateTime" /> which are hard to verify.
  /// </summary>
  public interface IDateTimeProvider
  {
    #region Public Methods

    /// <summary>
    /// Returns the current <see cref="DateTime.Now" /> value.
    /// </summary>
    /// <returns> The current <see cref="DateTime.Now" /> value. </returns>
    DateTime GetNow();

    #endregion Public Methods
  }
}