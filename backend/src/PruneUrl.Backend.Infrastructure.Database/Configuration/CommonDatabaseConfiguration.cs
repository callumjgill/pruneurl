using Microsoft.EntityFrameworkCore;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database;

/// <summary>
/// Defines methods which are used by all context providers, i.e. common configuration.
/// </summary>
public static class CommonDatabaseConfiguration
{
  /// <summary>
  /// Perform the common configuration on the entity model.
  /// </summary>
  /// <param name="modelBuilder">The <see cref="ModelBuilder"/> to apply the configuration to.</param>
  public static void ConfigureModel(ModelBuilder modelBuilder)
  {
    ConfigureShortUrl(modelBuilder);
  }

  private static void ConfigureShortUrl(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ShortUrl>().Property(shortUrl => shortUrl.Id).ValueGeneratedOnAdd();
    modelBuilder.Entity<ShortUrl>().HasIndex(shortUrl => shortUrl.Url);
  }
}
