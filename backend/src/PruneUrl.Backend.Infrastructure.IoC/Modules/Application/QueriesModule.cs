using Autofac;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using PruneUrl.Backend.Application.Queries.GetSequenceId;
using PruneUrl.Backend.Application.Queries.GetShortUrl;

namespace PruneUrl.Backend.Infrastructure.IoC.Modules.Application
{
  /// <summary>
  /// The Application "Queries" specific code IoC module
  /// </summary>
  internal sealed class QueriesModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      RegisterGetSequenceId(builder);
      RegisterGetShortUrl(builder);
    }

    private void RegisterGetSequenceId(ContainerBuilder builder)
    {
      MediatRConfiguration configuration = MediatRConfigurationBuilder
        .Create(typeof(GetSequenceIdQuery).Assembly)
        .WithAllOpenGenericHandlerTypesRegistered()
        .Build();
      builder.RegisterMediatR(configuration);
      builder.RegisterType<GetSequenceIdQueryValidator>().As<IValidator<GetSequenceIdQuery>>();
    }

    private void RegisterGetShortUrl(ContainerBuilder builder)
    {
      MediatRConfiguration configuration = MediatRConfigurationBuilder
        .Create(typeof(GetShortUrlQuery).Assembly)
        .WithAllOpenGenericHandlerTypesRegistered()
        .Build();
      builder.RegisterMediatR(configuration);
      builder.RegisterType<GetShortUrlQueryValidator>().As<IValidator<GetShortUrlQuery>>();
    }
  }
}
