using Autofac;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using PruneUrl.Backend.Application.Commands;

namespace PruneUrl.Backend.Infrastructure.IoC;

/// <summary>
/// The Application "Commands" specific code IoC module
/// </summary>
internal sealed class CommandsModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    RegisterCreateShortUrl(builder);
  }

  private void RegisterCreateShortUrl(ContainerBuilder builder)
  {
    MediatRConfiguration configuration = MediatRConfigurationBuilder
      .Create(typeof(CreateShortUrlCommand).Assembly)
      .WithAllOpenGenericHandlerTypesRegistered()
      .Build();
    builder.RegisterMediatR(configuration);
    builder.RegisterType<CreateShortUrlCommandValidator>().As<IValidator<CreateShortUrlCommand>>();
  }
}
