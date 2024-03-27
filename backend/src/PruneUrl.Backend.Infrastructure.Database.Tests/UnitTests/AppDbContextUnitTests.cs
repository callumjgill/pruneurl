using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    ServiceCollection services = new();
    services.AddInMemoryDbContext();
    using ServiceProvider serviceProvider = services.BuildServiceProvider();

    IDatabaseConfiguration databaseConfiguration =
      serviceProvider.GetRequiredService<IDatabaseConfiguration>();
    AppDbContext dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();

    databaseConfiguration.Received().Configure(Arg.Any<ModelBuilder>());
  }
}
