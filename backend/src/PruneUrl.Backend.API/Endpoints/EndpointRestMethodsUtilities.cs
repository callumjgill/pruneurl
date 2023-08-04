namespace PruneUrl.Backend.App.Endpoints
{
  /// <summary>
  /// Static class containing useful utilities to be used by the REST endpoint methods.
  /// </summary>
  internal static class EndpointRestMethodsUtilities
  {
    #region Public Methods

    /// <summary>
    /// Handles any exceptions thrown by the REST method which itself doesn't handle.
    /// </summary>
    /// <param name="restMethod"> The REST method the exceptions are handled from. </param>
    /// <returns>
    /// A task representing the asynchronous operation of executing the REST method along with any
    /// exception handling needed.
    /// </returns>
    public static async Task<IResult> HandleErrors(Func<Task<IResult>> restMethod)
    {
      try
      {
        IResult result = await restMethod();
        return result;
      }
      catch (Exception)
      {
        return Results.StatusCode(500);
      }
    }

    #endregion Public Methods
  }
}