using Autofac;
using Autofac.Features.Decorators;
using FluentValidation;
using MediatR;
using PruneUrl.Backend.Application.Requests.Decorators;

namespace PruneUrl.Backend.Infrastructure.IoC.Modules.Application;

/// <summary>
/// The Application "Requests" specific code IoC module.
/// </summary>
internal sealed class RequestsModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    RegisterDecorators(builder);
  }

  private void RegisterDecorators(ContainerBuilder builder)
  {
    builder.RegisterGenericDecorator(
      typeof(ValidateRequestHandlerDecorator<>),
      typeof(IRequestHandler<>),
      context => ValidatorIsRegistered(typeof(IRequestHandler<>).Name, context)
    );
    builder.RegisterGenericDecorator(
      typeof(ValidateRequestHandlerDecorator<,>),
      typeof(IRequestHandler<,>),
      context => ValidatorIsRegistered(typeof(IRequestHandler<,>).Name, context)
    );
  }

  private bool ValidatorIsRegistered(string interfaceName, IDecoratorContext context)
  {
    Type? requestHandlerInterfaceType = context
      .ImplementationType.GetInterfaces()
      .FirstOrDefault(interfaceType => interfaceType.Name == interfaceName);
    Type? genericTypeArgument = requestHandlerInterfaceType?.GenericTypeArguments.FirstOrDefault(
      genericTypeArgument => genericTypeArgument.IsAssignableTo(typeof(IBaseRequest))
    );
    return genericTypeArgument != null
      && context.IsRegistered(typeof(IValidator<>).MakeGenericType(genericTypeArgument));
  }
}
