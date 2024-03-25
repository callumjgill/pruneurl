using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using PruneUrl.Backend.Application.Configuration.Exceptions;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;

/// <summary>
/// Extensions for the <see cref="IConfiguration" /> interface to allow retriving the options from
/// </summary>
public static class ConfigurationExtensions
{
  private const string FirestoreDbOptionsSectionName = nameof(FirestoreDbOptions);

  /// <summary>
  /// Retrieves a <see cref="FirestoreDbOptions" /> instance from the underlying configuration.
  /// </summary>
  /// <param name="configuration"> This <see cref="IConfiguration" />. </param>
  /// <returns> The </returns>
  /// <exception cref="InvalidOperationException">
  /// Thrown when the configuration being read is missing.
  /// </exception>
  /// <exception cref="InvalidConfigurationException">
  /// Thrown when the configuration being read is invalid.
  /// </exception>
  public static FirestoreDbOptions GetFirestoreDbOptions(this IConfiguration configuration)
  {
    FirestoreDbOptions? firestoreDbOptions = configuration
      .GetSection(FirestoreDbOptionsSectionName)
      .Get<FirestoreDbOptions>();
    if (TryGetErrorMessageForFirestoreDbOptions(firestoreDbOptions, out string errorMessage))
    {
      throw new InvalidConfigurationException(FirestoreDbOptionsSectionName, errorMessage);
    }

    return firestoreDbOptions;
  }

  private static bool TryGetErrorMessageForFirestoreDbOptions(
    [NotNullWhen(false)] FirestoreDbOptions? firestoreDbOptions,
    out string errorMessage
  )
  {
    errorMessage = string.Empty;
    if (firestoreDbOptions == null)
    {
      errorMessage = $"Could not bind the section to the type '{typeof(FirestoreDbOptions)}'.";
    }
    else if (string.IsNullOrWhiteSpace(firestoreDbOptions.ProjectId))
    {
      errorMessage = $"Missing '{nameof(firestoreDbOptions.ProjectId)}' property!";
    }

    return !string.IsNullOrWhiteSpace(errorMessage);
  }
}
