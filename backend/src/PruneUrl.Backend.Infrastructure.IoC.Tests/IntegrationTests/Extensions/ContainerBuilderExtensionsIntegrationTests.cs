using Autofac;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Application.Implementation;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Application.Queries;
using PruneUrl.Backend.Application.Requests;
using PruneUrl.Backend.Infrastructure.Database;

namespace PruneUrl.Backend.Infrastructure.IoC.Tests;

[TestFixture]
[Parallelizable]
public sealed class ContainerBuilderExtensionsIntegrationTests
{
  [Test]
  public void RegisterAllModulesTest()
  {
    ContainerBuilder containerBuilder = new();
    containerBuilder.RegisterInstance(Substitute.For<IConfiguration>()).As<IConfiguration>();
    containerBuilder.RegisterAllModules();
    using IContainer container = containerBuilder.Build();
    Assert.Multiple(() =>
    {
      Assert.That(
        container.Resolve<IRequestHandler<CreateShortUrlCommand, CreateShortUrlCommandResponse>>(),
        Is.TypeOf<
          ValidateRequestHandlerDecorator<CreateShortUrlCommand, CreateShortUrlCommandResponse>
        >()
      );
      Assert.That(
        container.Resolve<IValidator<CreateShortUrlCommand>>(),
        Is.TypeOf<CreateShortUrlCommandValidator>()
      );
      Assert.That(container.Resolve<IShortUrlFactory>(), Is.TypeOf<ShortUrlFactory>());
      Assert.That(container.Resolve<IShortUrlProvider>(), Is.TypeOf<ShortUrlProvider>());
      Assert.That(container.Resolve<ISequenceIdProvider>(), Is.TypeOf<ShortUrlProvider>());
      Assert.That(
        container.Resolve<IRequestHandler<GetShortUrlQuery, GetShortUrlQueryResponse>>(),
        Is.TypeOf<ValidateRequestHandlerDecorator<GetShortUrlQuery, GetShortUrlQueryResponse>>()
      );
      Assert.That(
        container.Resolve<IValidator<GetShortUrlQuery>>(),
        Is.TypeOf<GetShortUrlQueryValidator>()
      );

      Assert.That(container.Resolve<IMediator>, Throws.Nothing);

      Assert.That(container.Resolve<IDbContext>, Is.TypeOf<AppDbContext>());
      Assert.That(
        container.Resolve<IDatabaseConfiguration>,
        Is.TypeOf<PostgresqlDatabaseConfiguration>()
      );
    });
  }
}
