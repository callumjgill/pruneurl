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

    mediator.When(x => x.Send(Arg.Any<CreateShortUrlCommand>())).Do(_ => throw new Exception());

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(shortUrlPostRequest, mediator);
    Assert.That(result, Is.TypeOf<StatusCodeHttpResult>());
    Assert.That(((StatusCodeHttpResult)result).StatusCode, Is.EqualTo(500));

    await mediator.Received(1).Send(Arg.Any<CreateShortUrlCommand>());
    await mediator.Received(1).Send(Arg.Is<CreateShortUrlCommand>(x => x.LongUrl == testLongUrl));
  }

  [Test]
  public async Task PostShortUrlTest_HandlesError_CreateShortUrlCommandThrows_InvalidRequestException_StatusCode400()
  {
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrlPostRequest shortUrlPostRequest = new(testLongUrl);

    mediator
      .When(x => x.Send(Arg.Any<CreateShortUrlCommand>()))
      .Do(_ => throw new InvalidRequestException([]));

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(shortUrlPostRequest, mediator);
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<BadRequest<string>>());
      Assert.That(((BadRequest<string>)result).StatusCode, Is.EqualTo(400));
    });

    await mediator.Received(1).Send(Arg.Any<CreateShortUrlCommand>());
    await mediator.Received(1).Send(Arg.Is<CreateShortUrlCommand>(x => x.LongUrl == testLongUrl));
  }
}
