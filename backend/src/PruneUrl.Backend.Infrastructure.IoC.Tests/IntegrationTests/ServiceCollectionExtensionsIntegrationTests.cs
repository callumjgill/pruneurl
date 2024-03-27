using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Application.Implementation;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Application.Queries;
using PruneUrl.Backend.Infrastructure.Database;

namespace PruneUrl.Backend.Infrastructure.IoC.Tests;

[TestFixture]
[Parallelizable]
public sealed class ServiceCollectionExtensionsIntegrationTests
{
  [Test]
  public void AddAppServicesTest()
  {
    ServiceCollection services = new();
    services.AddScoped(_ => Substitute.For<IConfiguration>());
    services.AddAppServices();
    using ServiceProvider serviceProvider = services.BuildServiceProvider();
    Assert.Multiple(() =>
    {
      Assert.That(
        serviceProvider.GetRequiredService<IValidator<CreateShortUrlCommand>>(),
        Is.TypeOf<CreateShortUrlCommandValidator>()
      );
      Assert.That(
        serviceProvider.GetRequiredService<IShortUrlFactory>(),
        Is.TypeOf<ShortUrlFactory>()
      );
      Assert.That(
        serviceProvider.GetRequiredService<IShortUrlProvider>(),
        Is.TypeOf<ShortUrlProvider>()
      );
      Assert.That(
        serviceProvider.GetRequiredService<ISequenceIdProvider>(),
        Is.TypeOf<ShortUrlProvider>()
      );
      Assert.That(
        serviceProvider.GetRequiredService<IValidator<GetShortUrlQuery>>(),
        Is.TypeOf<GetShortUrlQueryValidator>()
      );

      Assert.That(serviceProvider.GetRequiredService<IMediator>, Throws.Nothing);

      Assert.That(serviceProvider.GetRequiredService<IDbContext>, Is.TypeOf<AppDbContext>());
      Assert.That(
        serviceProvider.GetRequiredService<IDatabaseConfiguration>,
        Is.TypeOf<PostgresqlDatabaseConfiguration>()
      );
    });
  }
}
