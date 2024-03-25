using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Application.Transactions;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

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
    const int testSequenceId = 124213;
    var mediator = Substitute.For<IMediator>();
    var shortUrlProvider = Substitute.For<IShortUrlProvider>();
    SequenceId testSequenceIdEntity = EntityTestHelper.CreateSequenceId(value: testSequenceId);
    var getAndBumpSequenceIdResponse = new GetAndBumpSequenceIdResponse(testSequenceIdEntity);
    var shortUrlPostRequest = new ShortUrlPostRequest(testLongUrl);

    mediator.Send(Arg.Any<GetAndBumpSequenceIdRequest>()).Returns(getAndBumpSequenceIdResponse);
    mediator.Send(Arg.Any<CreateShortUrlCommand>()).Returns(Task.CompletedTask);

    shortUrlProvider.GetShortUrl(Arg.Any<int>()).Returns(testShortUrl);

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(
      shortUrlPostRequest,
      mediator,
      shortUrlProvider
    );
    Assert.That(result, Is.TypeOf<CreatedAtRoute>());
    Assert.That(((CreatedAtRoute)result).RouteName, Is.EqualTo(RouteNames.RedirectRoute));
    Assert.That(((CreatedAtRoute)result).StatusCode, Is.EqualTo(StatusCodes.Status201Created));

    await mediator.Received(1).Send(Arg.Any<GetAndBumpSequenceIdRequest>());
    await mediator.Received(1).Send(Arg.Any<CreateShortUrlCommand>());
    await mediator
      .Received(1)
      .Send(
        Arg.Is<CreateShortUrlCommand>(x =>
          x.LongUrl == testLongUrl && x.SequenceId == testSequenceId
        )
      );
    shortUrlProvider.Received(1).GetShortUrl(Arg.Any<int>());
    shortUrlProvider.Received(1).GetShortUrl(Arg.Is<int>(x => x == testSequenceId));
  }

  [Test]
  public async Task PostShortUrlTest_HandlesError_CreateShortUrlCommandThrows()
  {
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    const int testSequenceId = 124213;
    var mediator = Substitute.For<IMediator>();
    var shortUrlProvider = Substitute.For<IShortUrlProvider>();
    SequenceId testSequenceIdEntity = EntityTestHelper.CreateSequenceId(value: testSequenceId);
    var getAndBumpSequenceIdResponse = new GetAndBumpSequenceIdResponse(testSequenceIdEntity);
    var shortUrlPostRequest = new ShortUrlPostRequest(testLongUrl);

    mediator.Send(Arg.Any<GetAndBumpSequenceIdRequest>()).Returns(getAndBumpSequenceIdResponse);
    mediator.When(x => x.Send(Arg.Any<CreateShortUrlCommand>())).Do(_ => throw new Exception());

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(
      shortUrlPostRequest,
      mediator,
      shortUrlProvider
    );
    Assert.That(result, Is.TypeOf<StatusCodeHttpResult>());
    Assert.That(((StatusCodeHttpResult)result).StatusCode, Is.EqualTo(500));

    await mediator.Received(1).Send(Arg.Any<GetAndBumpSequenceIdRequest>());
    await mediator.Received(1).Send(Arg.Any<CreateShortUrlCommand>());
    await mediator
      .Received(1)
      .Send(
        Arg.Is<CreateShortUrlCommand>(x =>
          x.LongUrl == testLongUrl && x.SequenceId == testSequenceId
        )
      );
    shortUrlProvider.DidNotReceive().GetShortUrl(Arg.Any<int>());
    shortUrlProvider.DidNotReceive().GetShortUrl(Arg.Is<int>(x => x == testSequenceId));
  }

  [Test]
  public async Task PostShortUrlTest_HandlesError_GetAndBumpSequenceIdRequestThrows()
  {
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    const int testSequenceId = 124213;
    var mediator = Substitute.For<IMediator>();
    var shortUrlProvider = Substitute.For<IShortUrlProvider>();
    var shortUrlPostRequest = new ShortUrlPostRequest(testLongUrl);

    mediator
      .When(x => x.Send(Arg.Any<GetAndBumpSequenceIdRequest>()))
      .Do(_ => throw new Exception());

    IResult result = await ShortUrlEndpointRestMethods.PostShortUrl(
      shortUrlPostRequest,
      mediator,
      shortUrlProvider
    );
    Assert.That(result, Is.TypeOf<StatusCodeHttpResult>());
    Assert.That(((StatusCodeHttpResult)result).StatusCode, Is.EqualTo(500));

    await mediator.Received(1).Send(Arg.Any<GetAndBumpSequenceIdRequest>());
    await mediator.DidNotReceive().Send(Arg.Any<CreateShortUrlCommand>());
    await mediator
      .DidNotReceive()
      .Send(
        Arg.Is<CreateShortUrlCommand>(x =>
          x.LongUrl == testLongUrl && x.SequenceId == testSequenceId
        )
      );
    shortUrlProvider.DidNotReceive().GetShortUrl(Arg.Any<int>());
    shortUrlProvider.DidNotReceive().GetShortUrl(Arg.Is<int>(x => x == testSequenceId));
  }
}
