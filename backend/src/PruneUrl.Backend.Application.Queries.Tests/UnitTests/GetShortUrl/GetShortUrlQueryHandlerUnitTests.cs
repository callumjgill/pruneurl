using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Exceptions;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Queries.Tests;

[TestFixture]
[Parallelizable]
public sealed class GetShortUrlQueryHandlerUnitTests
{
  [Test]
  public async Task GetShortUrlTest_EntityFound()
  {
    const string testShortUrl = "hsfwgss";
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl(url: testShortUrl);
    GetShortUrlQuery query = new(testShortUrl);
    CancellationToken cancellationToken = CancellationToken.None;

    ServiceCollection services = new();
    services.AddInMemoryDbContext();
    using ServiceProvider serviceProvider = services.BuildServiceProvider();

    AppDbContext dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
    IEnumerable<ShortUrl> shortUrls =
    [
      testShortUrlEntity,
      EntityTestHelper.CreateShortUrl(url: "abcdefg"),
      EntityTestHelper.CreateShortUrl(url: "9086124hsjsmneh")
    ];
    dbContext.AddRange(shortUrls);
    await dbContext.SaveChangesAsync();

    GetShortUrlQueryHandler handler = new(dbContext);
    GetShortUrlQueryResponse response = await handler.Handle(query, cancellationToken);
    Assert.That(response.ShortUrl, Is.EqualTo(testShortUrlEntity));
  }

  [Test]
  public async Task GetShortUrlTest_EntityNotFound()
  {
    const string testShortUrl = "hsfwgss";
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl();
    GetShortUrlQuery query = new(testShortUrl);
    CancellationToken cancellationToken = CancellationToken.None;

    ServiceCollection services = new();
    services.AddInMemoryDbContext();
    using ServiceProvider serviceProvider = services.BuildServiceProvider();

    AppDbContext dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
    IEnumerable<ShortUrl> shortUrls =
    [
      testShortUrlEntity,
      EntityTestHelper.CreateShortUrl(url: "abcdefg"),
      EntityTestHelper.CreateShortUrl(url: "9086124hsjsmneh")
    ];
    dbContext.AddRange(shortUrls);
    await dbContext.SaveChangesAsync();

    GetShortUrlQueryHandler handler = new(dbContext);
    Assert.That(
      async () => await handler.Handle(query, cancellationToken),
      Throws
        .TypeOf<EntityNotFoundException<ShortUrl>>()
        .With.Message.EqualTo(
          $"Entity of type {typeof(ShortUrl)} was not found! Url={testShortUrl}."
        )
    );
  }
}
