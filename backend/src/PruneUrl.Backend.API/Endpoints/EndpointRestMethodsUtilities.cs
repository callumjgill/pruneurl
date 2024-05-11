using PruneUrl.Backend.Application.Requests;

namespace PruneUrl.Backend.API;

/// <summary>
/// Static class containing useful utilities to be used by the REST endpoint methods.
/// </summary>
internal static class EndpointRestMethodsUtilities
{
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
    catch (Exception ex)
    {
      int statusCode =
        ex is InvalidRequestException
          ? StatusCodes.Status400BadRequest
          : StatusCodes.Status500InternalServerError;
      return Results.Problem(ex.Message, statusCode: statusCode);
    }
  }
}
