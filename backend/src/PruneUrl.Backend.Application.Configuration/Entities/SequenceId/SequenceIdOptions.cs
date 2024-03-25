namespace PruneUrl.Backend.Application.Configuration.Entities.SequenceId
{
  /// <summary>
  /// A configuration DTO intended to be used in the options pattern. This is injected into classes
  /// via the options pattern. The configuration is specific to the "sequence id" entity.
  /// </summary>
  public sealed class SequenceIdOptions
  {
    /// <summary>
    /// The unique identifier used for retrieving a sequence id entity from the database.
    /// </summary>
    public string Id { get; set; } = string.Empty;
  }
}
