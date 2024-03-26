using Microsoft.EntityFrameworkCore;

namespace PruneUrl.Backend.Infrastructure.Database;

/// <summary>
/// Defines the configuration for the database.
/// </summary>
public interface IDatabaseConfiguration
{
  /// <summary>
  /// The <see cref="DbContextOptions" /> that are in use.
  /// </summary>
  DbContextOptions Options { get; }

  /// <summary>
  /// Configures all the entity types.
  /// </summary>
  /// <param name="modelBuilder"> The builder for constructing the model for the context. </param>
  void Configure(ModelBuilder modelBuilder);
}
