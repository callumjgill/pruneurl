using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;

namespace PruneUrl.Backend.Infrastructure.IoC.Modules.Application
{
  /// <summary>
  /// The Application "Transactions" specific code IoC module.
  /// </summary>
  internal sealed class TransactionsModule : Module
  {
    #region Protected Methods

    protected override void Load(ContainerBuilder builder)
    {
      RegisterGetAndBumpSequenceId(builder);
    }

    #endregion Protected Methods

    #region Private Methods

    private void RegisterGetAndBumpSequenceId(ContainerBuilder builder)
    {
      MediatRConfiguration configuration = MediatRConfigurationBuilder.Create(typeof(GetAndBumpSequenceIdRequest).Assembly)
                                                                      .WithAllOpenGenericHandlerTypesRegistered()
                                                                      .Build();
      builder.RegisterMediatR(configuration);
    }

    #endregion Private Methods
  }
}