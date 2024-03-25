using Autofac;
using AutoMapper;
using FluentValidation;
using Google.Cloud.Firestore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands.CreateSequenceId;
using PruneUrl.Backend.Application.Commands.CreateShortUrl;
using PruneUrl.Backend.Application.Configuration.Entities.SequenceId;
using PruneUrl.Backend.Application.Implementation.Factories.Entities;
using PruneUrl.Backend.Application.Implementation.Providers;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Application.Interfaces.Providers;
using PruneUrl.Backend.Application.Queries.GetSequenceId;
using PruneUrl.Backend.Application.Queries.GetShortUrl;
using PruneUrl.Backend.Application.Requests.Decorators;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Interfaces;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;
using PruneUrl.Backend.Infrastructure.IoC.Extensions;

namespace PruneUrl.Backend.Infrastructure.IoC.Tests.IntegrationTests.Extensions;

[TestFixture]
[Parallelizable]
public sealed class ContainerBuilderExtensionsIntegrationTests
{
  [Test]
  public void RegisterAllModulesTest()
  {
    var config = new Dictionary<string, string?>
    {
      { "FirestoreDbOptions:EmulatorDetection", "2" },
      { "FirestoreDbOptions:ProjectId", "TestId" },
      { "SequenceIdOptions:Id", "SomeGibberish" },
      { "FirestoreTransactionOptions:MaxAttempts", "1" }
    };
    var containerBuilder = new ContainerBuilder();
    IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
    containerBuilder.RegisterInstance(configuration).As<IConfiguration>();
    containerBuilder.Register(componentContext =>
    {
      SequenceIdOptions optionsObj =
        configuration.GetSection(nameof(SequenceIdOptions)).Get<SequenceIdOptions>()
        ?? throw new Exception($"Missing config for {nameof(SequenceIdOptions)}!");
      return Options.Create(optionsObj);
    });
    containerBuilder.Register(componentContext =>
    {
      FirestoreTransactionOptions optionsObj =
        configuration
          .GetSection(nameof(FirestoreTransactionOptions))
          .Get<FirestoreTransactionOptions>()
        ?? throw new Exception($"Missing config for {nameof(FirestoreTransactionOptions)}!");
      return Options.Create(optionsObj);
    });
    containerBuilder.RegisterAllModules();
    using (IContainer container = containerBuilder.Build())
    {
      Assert.Multiple(() =>
      {
        Assert.That(
          container.Resolve<IRequestHandler<CreateShortUrlCommand>>(),
          Is.TypeOf<ValidateRequestHandlerDecorator<CreateShortUrlCommand>>()
        );
        Assert.That(
          container.Resolve<IValidator<CreateShortUrlCommand>>(),
          Is.TypeOf<CreateShortUrlCommandValidator>()
        );
        Assert.That(
          container.Resolve<IRequestHandler<CreateSequenceIdCommand>>(),
          Is.TypeOf<ValidateRequestHandlerDecorator<CreateSequenceIdCommand>>()
        );
        Assert.That(
          container.Resolve<IValidator<CreateSequenceIdCommand>>(),
          Is.TypeOf<CreateSequenceIdCommandValidator>()
        );
        Assert.That(container.Resolve<ISequenceIdFactory>(), Is.TypeOf<SequenceIdFactory>());
        Assert.That(container.Resolve<IShortUrlFactory>(), Is.TypeOf<ShortUrlFactory>());
        Assert.That(container.Resolve<IDateTimeProvider>(), Is.TypeOf<DateTimeProvider>());
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
        Assert.That(
          container.Resolve<IRequestHandler<GetSequenceIdQuery, GetSequenceIdQueryResponse>>(),
          Is.TypeOf<
            ValidateRequestHandlerDecorator<GetSequenceIdQuery, GetSequenceIdQueryResponse>
          >()
        );
        Assert.That(
          container.Resolve<IValidator<GetSequenceIdQuery>>(),
          Is.TypeOf<GetSequenceIdQueryValidator>()
        );
        Assert.That(
          container.Resolve<
            IRequestHandler<GetAndBumpSequenceIdRequest, GetAndBumpSequenceIdResponse>
          >(),
          Is.TypeOf<GetAndBumpSequenceIdRequestHandler>()
        );
        Assert.That(
          container.Resolve<IFirestoreDbTransactionFactory>(),
          Is.TypeOf<FirestoreDbTransactionFactory>()
        );
        Assert.That(
          container.Resolve<IDbGetByIdOperationFactory>(),
          Is.TypeOf<FirestoreDbGetByIdOperationFactory>()
        );
        Assert.That(
          container.Resolve<IDbTransactionProvider>(),
          Is.TypeOf<FirestoreDbTransactionProvider>()
        );
        Assert.That(
          container.Resolve<IDbWriteBatchFactory>(),
          Is.TypeOf<FirestoreDbWriteBatchFactory>()
        );

        Assert.That(container.Resolve<IMediator>, Throws.Nothing);
        Assert.That(container.Resolve<FirestoreDb>, Throws.Nothing);
        Assert.That(container.Resolve<IDbGetByIdOperation<ShortUrl>>, Throws.Nothing);
        Assert.That(container.Resolve<IDbGetByIdOperation<SequenceId>>, Throws.Nothing);
        Assert.That(container.Resolve<IDbWriteBatch<ShortUrl>>, Throws.Nothing);
        Assert.That(container.Resolve<IDbWriteBatch<SequenceId>>, Throws.Nothing);
        Assert.That(container.Resolve<MapperConfiguration>, Throws.Nothing);
        Assert.That(container.Resolve<IMapper>, Throws.Nothing);
        Assert.That(
          container.Resolve<MapperConfiguration>().AssertConfigurationIsValid,
          Throws.Nothing
        );
      });
    }
  }
}
