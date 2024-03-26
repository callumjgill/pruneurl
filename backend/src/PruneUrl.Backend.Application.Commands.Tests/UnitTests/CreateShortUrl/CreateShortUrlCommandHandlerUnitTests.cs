using Autofac;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Commands.Tests;

[TestFixture]
[Parallelizable]
public sealed class CreateShortUrlCommandHandlerUnitTests
{
  [Test]
  public async Task HandleTest()
  {
    const string testUrl = "https://short";
    const string testLongUrl = "https://www.youtube.com";
    const int testSequenceId = 6124323;
    IShortUrlFactory shortUrlFactory = Substitute.For<IShortUrlFactory>();
    ShortUrl testPlaceholderShortUrl = EntityTestHelper.CreateShortUrl(id: testSequenceId);
    ShortUrl testShortUrl = EntityTestHelper.CreateShortUrl(url: testUrl);
    CancellationToken cancellationToken = CancellationToken.None;

    ContainerBuilder containerBuilder = new();
    RegisterDependencies(containerBuilder);
    using IContainer container = containerBuilder.Build();

    AppDbContext dbContext = container.Resolve<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();

    shortUrlFactory.Create(Arg.Any<string>(), Arg.Any<int>()).Returns(testShortUrl);
    shortUrlFactory.Create().Returns(testPlaceholderShortUrl);

    CreateShortUrlCommandHandler handler = new(dbContext, shortUrlFactory);
    CreateShortUrlCommand command = new(testLongUrl);

    CreateShortUrlCommandResponse response = await handler.Handle(command, cancellationToken);

    shortUrlFactory.Received(1).Create(Arg.Any<string>(), Arg.Any<int>());
    shortUrlFactory.Received(1).Create(testLongUrl, testSequenceId);
    shortUrlFactory.Received(1).Create();
    Assert.That(response.ShortUrl, Is.EqualTo(testUrl));
  }

  private static void RegisterDependencies(ContainerBuilder containerBuilder)
  {
    containerBuilder.RegisterInMemoryDbContext();
  }
}
