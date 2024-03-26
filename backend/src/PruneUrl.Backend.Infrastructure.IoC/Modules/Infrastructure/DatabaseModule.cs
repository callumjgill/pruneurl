using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Infrastructure.Database;

namespace PruneUrl.Backend.Infrastructure.IoC;

/// <summary>
/// The Infrastructure "Database" specific code IoC module.
/// </summary>
internal sealed class DatabaseModule : Module
{
  /// <inheritdoc cref="Module.Load" />
  protected override void Load(ContainerBuilder builder)
  {
    RegisterDbContext(builder);
    RegisterDatabaseConfiguration(builder);
  }

  private static void RegisterDbContext(ContainerBuilder builder)
  {
    builder.Register(_ => new DbContextOptionsBuilder<AppDbContext>()).InstancePerLifetimeScope();
    builder
      .Register(componentContext =>
      {
        IDatabaseConfiguration databaseConfiguration =
          componentContext.Resolve<IDatabaseConfiguration>();
        return new AppDbContext(databaseConfiguration);
      })
      .As<IDbContext>();
  }

  private static void RegisterDatabaseConfiguration(ContainerBuilder builder)
  {
    builder
      .Register(componentContext =>
      {
        IConfiguration configuration = componentContext.Resolve<IConfiguration>();
        DbContextOptionsBuilder<AppDbContext> optionsBuilder = componentContext.Resolve<
          DbContextOptionsBuilder<AppDbContext>
        >();
        return new PostgresqlDatabaseConfiguration(configuration, optionsBuilder);
      })
      .As<IDatabaseConfiguration>();
  }
}
