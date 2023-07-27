using Autofac;
using PruneUrl.Backend.Application.Implementation.Factories.Entities;
using PruneUrl.Backend.Application.Implementation.Providers;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Application.Interfaces.Providers;

namespace PruneUrl.Backend.Infrastructure.IoC.Modules.Application
{
  /// <summary>
  /// The Application "Implementation" specific code IoC module.
  /// </summary>
  internal sealed class ImplementationModule : Module
  {
    #region Protected Methods

    protected override void Load(ContainerBuilder builder)
    {
      RegisterProviders(builder);
      RegisterFactories(builder);
    }

    #endregion Protected Methods

    #region Private Methods

    private void RegisterFactories(ContainerBuilder builder)
    {
      builder.RegisterType<SequenceIdFactory>().As<ISequenceIdFactory>();
      builder.RegisterType<ShortUrlFactory>().As<IShortUrlFactory>();
    }

    private void RegisterProviders(ContainerBuilder builder)
    {
      builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>();
      builder.RegisterType<ShortUrlProvider>().AsImplementedInterfaces();
    }

    #endregion Private Methods
  }
}