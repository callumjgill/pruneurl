namespace PruneUrl.Backend.Application.Configuration.Exceptions
{
  /// <summary>
  /// An exception which is thrown when the configuration is invalid.
  /// </summary>
  public sealed class InvalidConfigurationException : Exception
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="InvalidConfigurationException" /> class.
    /// </summary>
    /// <param name="sectionName"> The name of the configuration section which is invalid. </param>
    public InvalidConfigurationException(string sectionName, string error) : base($"The section '{sectionName}' is invalid! {error}")
    {
    }

    #endregion Public Constructors
  }
}