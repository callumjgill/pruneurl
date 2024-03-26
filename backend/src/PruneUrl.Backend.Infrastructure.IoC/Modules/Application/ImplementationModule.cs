﻿using Autofac;
using PruneUrl.Backend.Application.Implementation;
using PruneUrl.Backend.Application.Interfaces;

namespace PruneUrl.Backend.Infrastructure.IoC;

/// <summary>
/// The Application "Implementation" specific code IoC module.
/// </summary>
internal sealed class ImplementationModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    RegisterProviders(builder);
    RegisterFactories(builder);
  }

  private void RegisterFactories(ContainerBuilder builder)
  {
    builder.RegisterType<ShortUrlFactory>().As<IShortUrlFactory>();
  }

  private void RegisterProviders(ContainerBuilder builder)
  {
    builder.RegisterType<ShortUrlProvider>().AsImplementedInterfaces();
  }
}
