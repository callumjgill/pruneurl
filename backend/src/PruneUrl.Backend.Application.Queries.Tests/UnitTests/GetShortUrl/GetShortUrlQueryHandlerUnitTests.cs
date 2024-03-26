using Autofac;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Exceptions;
using PruneUrl.Backend.Application.Interfaces;
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

    ContainerBuilder containerBuilder = new();
    RegisterDependencies(containerBuilder);
    using IContainer container = containerBuilder.Build();

    AppDbContext dbContext = container.Resolve<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
    IEnumerable<ShortUrl> shortUrls =
    [
      testShortUrlEntity,
      EntityTestHelper.CreateShortUrl(url: "abcdefg"),
      EntityTestHelper.CreateShortUrl(url: "9086124hsjsmneh")
    ];
    dbContext.AddRange(shortUrls);
    await dbContext.SaveChangesAsync();

    GetShortUrlQueryHandler handler = container.Resolve<GetShortUrlQueryHandler>();
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

    ContainerBuilder containerBuilder = new();
    RegisterDependencies(containerBuilder);
    using IContainer container = containerBuilder.Build();

    AppDbContext dbContext = container.Resolve<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
    IEnumerable<ShortUrl> shortUrls =
    [
      testShortUrlEntity,
      EntityTestHelper.CreateShortUrl(url: "abcdefg"),
      EntityTestHelper.CreateShortUrl(url: "9086124hsjsmneh")
    ];
    dbContext.AddRange(shortUrls);
    await dbContext.SaveChangesAsync();

    GetShortUrlQueryHandler handler = container.Resolve<GetShortUrlQueryHandler>();
    Assert.That(
      async () => await handler.Handle(query, cancellationToken),
      Throws
        .TypeOf<EntityNotFoundException<ShortUrl>>()
        .With.Message.EqualTo(
          $"Entity of type {typeof(ShortUrl)} was not found! Url={testShortUrl}."
        )
    );
  }

  private static void RegisterDependencies(ContainerBuilder containerBuilder)
  {
    containerBuilder.RegisterInMemoryDbContext();
    containerBuilder.Register(
      (componentContext) => new GetShortUrlQueryHandler(componentContext.Resolve<IDbContext>())
    );
  }
}
