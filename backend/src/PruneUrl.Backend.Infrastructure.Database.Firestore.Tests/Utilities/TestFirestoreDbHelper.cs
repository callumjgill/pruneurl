using Google.Api.Gax;
using Google.Cloud.Firestore;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.Utilities
{
  /// <summary>
  /// A helper for working with instances of <see cref="FirestoreDb" /> for testing.
  /// </summary>
  internal static class TestFirestoreDbHelper
  {
    #region Private Fields

    private const string emulatorEnviromentVariable = "FIRESTORE_EMULATOR_HOST";
    private const string projectIdEnviromentVariable = "FIRESTORE_EMULATOR_PROJECT_ID";

    #endregion Private Fields

    #region Public Methods

    /// <summary>
    /// Clears the emulated database for the next test run.
    /// </summary>
    /// <returns> A task representing the asynchronous operation of clearing the database. </returns>
    /// <remarks>
    /// It is important that tests are run in isolation, so they cannot be ran in parallel with
    /// other tests which alter the state of the emulator.
    /// </remarks>
    public static async Task ClearEmulatedDatabase()
    {
      var httpClient = new HttpClient();
      (string emulatorHost, string projectId) = GetTestFirestoreEnviromentVariables();
      var requestUri = new Uri($"http://{emulatorHost}/emulator/v1/projects/{projectId}/databases/(default)/documents");
      using HttpResponseMessage response = await httpClient.DeleteAsync(requestUri);
      response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Creates a test <see cref="CollectionReference" /> instance from a <see cref="FirestoreDb" />
    /// instance. Ensures it is unique for the test so that they can be ran in parallel.
    /// </summary>
    /// <param name="firestoreDb"> The test <see cref="FirestoreDb" /> instance. </param>
    /// <returns> The test <see cref="CollectionReference" /> instance. </returns>
    public static CollectionReference GetTestCollectionReference(FirestoreDb firestoreDb)
    {
      return firestoreDb.Collection(Guid.NewGuid().ToString());
    }

    /// <summary>
    /// Creates a test <see cref="FirestoreDb" /> instance.
    /// </summary>
    /// <returns> The test <see cref="FirestoreDb" /> instance. </returns>
    public static FirestoreDb GetTestFirestoreDb()
    {
      (_, string projectId) = GetTestFirestoreEnviromentVariables();
      var builder = new FirestoreDbBuilder()
      {
        EmulatorDetection = EmulatorDetection.EmulatorOnly,
        ProjectId = projectId
      };
      return builder.Build();
    }

    #endregion Public Methods

    #region Private Methods

    private static (string, string) GetTestFirestoreEnviromentVariables()
    {
      string? emulatorHost = Environment.GetEnvironmentVariable(emulatorEnviromentVariable);
      if (emulatorHost == null)
      {
        throw new InvalidOperationException($"The enviroment variable '{emulatorEnviromentVariable}' has not been set!");
      }

      string? emulatorProjectId = Environment.GetEnvironmentVariable(projectIdEnviromentVariable);
      if (emulatorProjectId == null)
      {
        throw new InvalidOperationException($"The enviroment variable '{projectIdEnviromentVariable}' has not been set!");
      }

      return (emulatorHost, emulatorProjectId);
    }

    #endregion Private Methods
  }
}