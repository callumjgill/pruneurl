namespace PruneUrl.Backend.Application.Configuration.Exceptions;

/// <summary>
/// An exception which is thrown when the configuration is invalid.
/// </summary>
/// <param name="sectionName"> The name of the configuration section which is invalid. </param>
public sealed class InvalidConfigurationException(string sectionName, string error)
  : Exception($"The section '{sectionName}' is invalid! {error}") { }
