using Google.Api.Gax;
using Google.Cloud.Firestore;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;

/// <summary>
/// A configuration DTO intended to be used in the options pattern. This is injected into classes
/// via the options pattern. The configuration is used for setting up a <see cref="FirestoreDb" />.
/// </summary>
public sealed class FirestoreDbOptions
{
  /// <summary>
  /// Specifies how the builder responds to the presence of the FIRESTORE_EMULATOR_HOST emulator
  /// environment variable.
  /// </summary>
  public EmulatorDetection EmulatorDetection { get; set; }

  /// <summary>
  /// The ID of the Google Cloud Platform project that contains the database.
  /// </summary>
  public string ProjectId { get; set; } = string.Empty;
}
