using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PruneUrl.Backend.Infrastructure.Database;

/// <summary>
/// A PostgreSQL specific implementation of <see cref="IDatabaseConfiguration" />.
/// </summary>
public sealed class PostgresqlDatabaseConfiguration(
  IConfiguration configuration,
  DbContextOptionsBuilder optionsBuilder
) : IDatabaseConfiguration
{
  private const string PostgresqlConnectionStringName = "PostgresqlDb";
  private readonly DbContextOptions options = optionsBuilder
    .UseNpgsql(configuration.GetConnectionString(PostgresqlConnectionStringName))
    .Options;

  /// <inheritdoc cref="IDatabaseConfiguration.Options" />
  public DbContextOptions Options => options;

  /// <inheritdoc cref="IDatabaseConfiguration.Configure" />
  public void Configure(ModelBuilder modelBuilder)
  {
    CommonDatabaseConfiguration.ConfigureModel(modelBuilder);
  }
}
