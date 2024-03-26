using Autofac;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Infrastructure.Database.Tests;

[TestFixture]
[Parallelizable]
public sealed class AppDbContextUnitTests
{
  [Test]
  public async Task OnModelCreatingTest()
  {
    ContainerBuilder containerBuilder = new();
    containerBuilder.RegisterInMemoryDbContext();
    using IContainer container = containerBuilder.Build();

    IDatabaseConfiguration databaseConfiguration = container.Resolve<IDatabaseConfiguration>();
    AppDbContext dbContext = container.Resolve<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();

    databaseConfiguration.Received().Configure(Arg.Any<ModelBuilder>());
  }
}
