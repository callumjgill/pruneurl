using Autofac;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using PruneUrl.Backend.Application.Interfaces;
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
  /// <param name="containerBuilder"> The AutoFac dependency injection container builder.</param>
  public static void RegisterInMemoryDbContext(this ContainerBuilder containerBuilder)
  {
    containerBuilder.Register(_ =>
    {
      SqliteConnection keepAliveConnection = new("DataSource=:memory:");
      keepAliveConnection.Open();
      return keepAliveConnection;
    });
    containerBuilder
      .Register(componentContext =>
      {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
          .UseSqlite(componentContext.Resolve<SqliteConnection>())
          .EnableSensitiveDataLogging()
          .Options;
        IDatabaseConfiguration databaseConfiguration = Substitute.For<IDatabaseConfiguration>();
        databaseConfiguration.Options.Returns(options);
        databaseConfiguration
          .When(x => x.Configure(Arg.Any<ModelBuilder>()))
          .Do(x => CommonDatabaseConfiguration.ConfigureModel(x.Arg<ModelBuilder>()));

        return databaseConfiguration;
      })
      .InstancePerLifetimeScope();
    containerBuilder
      .Register(componentContext =>
      {
        IDatabaseConfiguration databaseConfiguration =
          componentContext.Resolve<IDatabaseConfiguration>();
        return new AppDbContext(databaseConfiguration);
      })
      .AsSelf()
      .As<IDbContext>()
      .InstancePerLifetimeScope();
  }
}
