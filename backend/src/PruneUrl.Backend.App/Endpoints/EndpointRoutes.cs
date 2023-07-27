using Microsoft.OpenApi.Models;
using PruneUrl.Backend.App.Endpoints.Models;

namespace PruneUrl.Backend.App.Endpoints
{
  /// <summary>
  /// Provides API REST routes for the app.
  /// </summary>
  internal static class EndpointRoutes
  {
    #region Private Fields

    private const string ApiUrlResourceGroup = "/api";
    private const string ShortUrlResourceGroup = "/shortUrls";

    #endregion Private Fields

    #region Public Methods

    /// <summary>
    /// Maps all the REST endpoint routes.
    /// </summary>
    /// <param name="routeBuilder"> </param>
    /// <returns> </returns>
    public static IEndpointRouteBuilder MapEndpointRoutes(this IEndpointRouteBuilder routeBuilder)
    {
      routeBuilder.MapRedirectRoutes();

      RouteGroupBuilder apiResourceGroup = routeBuilder.MapGroup(ApiUrlResourceGroup);
      apiResourceGroup.MapShortUrlRoutes();

      return routeBuilder;
    }

    #endregion Public Methods

    #region Private Methods

    private static IEndpointRouteBuilder MapRedirectRoutes(this IEndpointRouteBuilder routeBuilder)
    {
      routeBuilder.MapGet("/{shortUrl}", RedirectEndpointRestMethods.GetShortUrl)
                  .WithName(RouteNames.RedirectRoute)
                  .WithOpenApi(generatedOperation =>
                  {
                    OpenApiParameter parameter = generatedOperation.Parameters[0];
                    parameter.Description = "The short url to use for redirection to the corresponding long url.";

                    generatedOperation.Summary = "Redirects the caller to the equivalent long url.";
                    generatedOperation.Description = "Redirects the caller to the equivalent long url if the short url is found and valid.";
                    return generatedOperation;
                  })
                  .Produces(StatusCodes.Status307TemporaryRedirect)
                  .Produces(StatusCodes.Status404NotFound)
                  .Produces(StatusCodes.Status500InternalServerError);

      return routeBuilder;
    }

    private static IEndpointRouteBuilder MapShortUrlRoutes(this IEndpointRouteBuilder routeBuilder)
    {
      RouteGroupBuilder shortUrlResourceGroup = routeBuilder.MapGroup(ShortUrlResourceGroup);

      shortUrlResourceGroup.MapPost("/", ShortUrlEndpointRestMethods.PostShortUrl)
                           .WithName(RouteNames.PostShortUrlRoute)
                           .WithOpenApi(generatedOperation =>
                           {
                             generatedOperation.Summary = "Creates a new 'short url' resource.";
                             generatedOperation.Description = "Creates a new 'short url' resource and returns its location if successfully created.";
                             return generatedOperation;
                           })
                           .Accepts<ShortUrlPostRequest>("application/json")
                           .Produces(StatusCodes.Status201Created)
                           .Produces(StatusCodes.Status500InternalServerError);

      return routeBuilder;
    }

    #endregion Private Methods
  }
}