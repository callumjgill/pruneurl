using Autofac;

namespace PruneUrl.Backend.Infrastructure.IoC;

/// <summary>
/// Extensions for the <see cref="ContainerBuilder" /> class.
/// </summary>
public static class ContainerBuilderExtensions
{
  /// <summary>
  /// Register all the modules required for the container.
  /// </summary>
  public static void RegisterAllModules(this ContainerBuilder builder)
  {
    builder.RegisterModule<CommandsModule>();
    builder.RegisterModule<ImplementationModule>();
    builder.RegisterModule<QueriesModule>();
    builder.RegisterModule<RequestsModule>();

    builder.RegisterModule<DatabaseModule>();
  }
}
