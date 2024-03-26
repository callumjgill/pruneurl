using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Exceptions;
using PruneUrl.Backend.Application.Queries;
using PruneUrl.Backend.Application.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.API.Tests;

[TestFixture]
[Parallelizable]
public sealed class RedirectEndpointRestMethodsUnitTests
{
  [Test]
  public async Task GetShortUrlTest_HandlesException_StatusCode500()
  {
    const string testShortUrl = "Testing123";
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl(longUrl: testLongUrl);
    GetShortUrlQueryResponse response = new(testShortUrlEntity);

    mediator
      .When(x => x.Send(Arg.Any<GetShortUrlQuery>()))
      .Do(x =>
      {
        throw new Exception();
      });

    IResult result = await RedirectEndpointRestMethods.GetShortUrl(testShortUrl, mediator);
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<StatusCodeHttpResult>());
      Assert.That(((StatusCodeHttpResult)result).StatusCode, Is.EqualTo(500));
    });

    await mediator.Received(1).Send(Arg.Any<GetShortUrlQuery>());
    await mediator.Received(1).Send(Arg.Is<GetShortUrlQuery>(x => x.ShortUrl == testShortUrl));
  }

  [Test]
  public async Task GetShortUrlTest_HandlesInvalidRequestException_StatusCode400()
  {
    const string testShortUrl = "Testing123";
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl(longUrl: testLongUrl);
    GetShortUrlQueryResponse response = new(testShortUrlEntity);

    mediator
      .When(x => x.Send(Arg.Any<GetShortUrlQuery>()))
      .Do(x =>
      {
        throw new InvalidRequestException([]);
      });

    IResult result = await RedirectEndpointRestMethods.GetShortUrl(testShortUrl, mediator);
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<BadRequest<string>>());
      Assert.That(((BadRequest<string>)result).StatusCode, Is.EqualTo(400));
    });

    await mediator.Received(1).Send(Arg.Any<GetShortUrlQuery>());
    await mediator.Received(1).Send(Arg.Is<GetShortUrlQuery>(x => x.ShortUrl == testShortUrl));
  }

  [Test]
  public async Task GetShortUrlTest_ShortUrlFound()
  {
    const string testShortUrl = "Testing123";
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl(longUrl: testLongUrl);
    GetShortUrlQueryResponse response = new(testShortUrlEntity);

    mediator.Send(Arg.Any<GetShortUrlQuery>()).Returns(response);

    IResult result = await RedirectEndpointRestMethods.GetShortUrl(testShortUrl, mediator);
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<RedirectHttpResult>());
      Assert.That(((RedirectHttpResult)result).Url, Is.EqualTo(testLongUrl));
    });

    await mediator.Received(1).Send(Arg.Any<GetShortUrlQuery>());
    await mediator.Received(1).Send(Arg.Is<GetShortUrlQuery>(x => x.ShortUrl == testShortUrl));
  }

  [Test]
  public async Task GetShortUrlTest_ShortUrlNotFound()
  {
    const string testShortUrl = "Testing123";
    const string testLongUrl = "SomeLongUrlWouldBeHere";
    IMediator mediator = Substitute.For<IMediator>();
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl(longUrl: testLongUrl);
    GetShortUrlQueryResponse response = new(testShortUrlEntity);

    mediator
      .When(x => x.Send(Arg.Any<GetShortUrlQuery>()))
      .Do(x =>
      {
        throw new EntityNotFoundException<ShortUrl>(string.Empty);
      });

    IResult result = await RedirectEndpointRestMethods.GetShortUrl(testShortUrl, mediator);
    Assert.That(result, Is.TypeOf<NotFound>());
    await mediator.Received(1).Send(Arg.Any<GetShortUrlQuery>());
    await mediator.Received(1).Send(Arg.Is<GetShortUrlQuery>(x => x.ShortUrl == testShortUrl));
  }
}
