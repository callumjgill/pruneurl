using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;

namespace PruneUrl.Backend.Infrastructure.IoC.Modules.Application;

/// <summary>
/// The Application "Transactions" specific code IoC module.
/// </summary>
internal sealed class TransactionsModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    RegisterGetAndBumpSequenceId(builder);
  }

  private void RegisterGetAndBumpSequenceId(ContainerBuilder builder)
  {
    MediatRConfiguration configuration = MediatRConfigurationBuilder
      .Create(typeof(GetAndBumpSequenceIdRequest).Assembly)
      .WithAllOpenGenericHandlerTypesRegistered()
      .Build();
    builder.RegisterMediatR(configuration);
  }
}
