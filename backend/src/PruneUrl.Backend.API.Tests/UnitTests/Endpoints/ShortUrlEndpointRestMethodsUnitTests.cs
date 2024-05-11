using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Application.Requests;

namespace PruneUrl.Backend.API.Tests;

[TestFixture]
[Parallelizable]
public sealed class ShortUrlEndpointRestMethodsUnitTests
{
  [Test]
  public async Task PostShortUrlTest_Created()
  {
    const string testShortUrl = "Testing123";
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrlPostRequest shortUrlPostRequest = new(testLongUrl);
    CreateShortUrlCommandResponse commandResponse = new(testShortUrl);

    mediator.Send(Arg.Any<CreateShortUrlCommand>()).Returns(commandResponse);

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(shortUrlPostRequest, mediator);
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<CreatedAtRoute>());
      Assert.That(((CreatedAtRoute)result).RouteName, Is.EqualTo(RouteNames.RedirectRoute));
      Assert.That(((CreatedAtRoute)result).StatusCode, Is.EqualTo(StatusCodes.Status201Created));
      RouteValueDictionary routeValues = ((CreatedAtRoute)result).RouteValues;
      Assert.That(routeValues, Has.Count.EqualTo(1));
      Assert.That(routeValues.First().Value, Is.EqualTo(testShortUrl));
    });

    await mediator.Received(1).Send(Arg.Any<CreateShortUrlCommand>());
    await mediator.Received(1).Send(Arg.Is<CreateShortUrlCommand>(x => x.LongUrl == testLongUrl));
  }

  [Test]
  public async Task PostShortUrlTest_HandlesError_CreateShortUrlCommandThrows_Exception_StatusCode500()
  {
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrlPostRequest shortUrlPostRequest = new(testLongUrl);

    Exception exception = new("This is an error.");
    mediator.When(x => x.Send(Arg.Any<CreateShortUrlCommand>())).Do(_ => throw exception);

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(shortUrlPostRequest, mediator);
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<ProblemHttpResult>());
      Assert.That(((ProblemHttpResult)result).StatusCode, Is.EqualTo(500));
      Assert.That(((ProblemHttpResult)result).ProblemDetails.Detail, Is.EqualTo(exception.Message));
    });

    await mediator.Received(1).Send(Arg.Any<CreateShortUrlCommand>());
    await mediator.Received(1).Send(Arg.Is<CreateShortUrlCommand>(x => x.LongUrl == testLongUrl));
  }

  [Test]
  public async Task PostShortUrlTest_HandlesError_CreateShortUrlCommandThrows_InvalidRequestException_StatusCode400()
  {
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrlPostRequest shortUrlPostRequest = new(testLongUrl);

    InvalidRequestException exception = new([]);
    mediator.When(x => x.Send(Arg.Any<CreateShortUrlCommand>())).Do(_ => throw exception);

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(shortUrlPostRequest, mediator);
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<ProblemHttpResult>());
      Assert.That(((ProblemHttpResult)result).StatusCode, Is.EqualTo(400));
      Assert.That(((ProblemHttpResult)result).ProblemDetails.Detail, Is.EqualTo(exception.Message));
    });

    await mediator.Received(1).Send(Arg.Any<CreateShortUrlCommand>());
    await mediator.Received(1).Send(Arg.Is<CreateShortUrlCommand>(x => x.LongUrl == testLongUrl));
  }
}
