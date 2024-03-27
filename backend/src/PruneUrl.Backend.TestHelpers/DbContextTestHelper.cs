using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PruneUrl.Backend.Infrastructure.Database;

namespace PruneUrl.Backend.TestHelpers;

/// <summary>
/// A library of helper functions to do with <see cref="AppDbContext" />'s.
/// </summary>
public static class DbContextTestHelper
{
  /// <summary>
  /// Ensures that the database is created for a test <see cref="AppDbContext" /> instance.
  /// </summary>
  /// <param name="dbContext">
  /// The <see cref="AppDbContext" /> to use for creating the database.
  /// </param>
  /// <returns> A task representing the asyynchronous operation of creating the database. </returns>
  public static Task CreateDatabase(this AppDbContext dbContext)
  {
    return dbContext.Database.EnsureCreatedAsync();
  }

  /// <summary>
  /// Creates and registers an in-memory <see cref="AppDbContext" /> instance in the
  /// IoC container.
  /// </summary>
  /// <param name="services"> This <see cref="IServiceCollection"/> dependency injection container builder.</param>
  public static void AddInMemoryDbContext(this IServiceCollection services)
  {
    services.AddScoped(_ =>
    {
      SqliteConnection keepAliveConnection = new("DataSource=:memory:");
      keepAliveConnection.Open();
      return keepAliveConnection;
    });
    services.AddScoped(serviceProvider =>
    {
      DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(serviceProvider.GetRequiredService<SqliteConnection>())
        .EnableSensitiveDataLogging()
        .Options;
      IDatabaseConfiguration databaseConfiguration = Substitute.For<IDatabaseConfiguration>();
      databaseConfiguration.Options.Returns(options);
      databaseConfiguration
        .When(x => x.Configure(Arg.Any<ModelBuilder>()))
        .Do(x => CommonDatabaseConfiguration.ConfigureModel(x.Arg<ModelBuilder>()));

      return databaseConfiguration;
    });
    services.AddDbContext<AppDbContext>();
  }
}
