using Microsoft.EntityFrameworkCore;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database;

/// <summary>
/// The <see cref="DbContext"/> for the application.
/// </summary>
/// <param name="databaseConfiguration">The database configuration to use.</param>
public sealed class AppDbContext(IDatabaseConfiguration databaseConfiguration)
  : DbContext(databaseConfiguration.Options),
    IDbContext
{
  private readonly IDatabaseConfiguration databaseConfiguration = databaseConfiguration;

  /// <inheritdoc cref="IDbContext.ShortUrls" />
  public DbSet<ShortUrl> ShortUrls { get; set; }

  /// <inheritdoc cref="DbContext.OnModelCreating(ModelBuilder)" />
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    databaseConfiguration.Configure(modelBuilder);
  }
}
