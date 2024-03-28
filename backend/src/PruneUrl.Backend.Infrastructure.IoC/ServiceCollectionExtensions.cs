using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Application.Implementation;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Application.Queries;
using PruneUrl.Backend.Application.Requests;
using PruneUrl.Backend.Infrastructure.Database;
using PruneUrl.Backend.Infrastructure.Requests;

namespace PruneUrl.Backend.Infrastructure.IoC;

/// <summary>
/// Extensions for the <see cref="IServiceCollection"/> interface which allows registering the dependencies
/// of the entire application.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Adds all the application services to this <see cref="IServiceCollection"/>.
  /// </summary>
  /// <param name="services">This <see cref="IServiceCollection"/>.</param>
  public static IServiceCollection AddAppServices(this IServiceCollection services)
  {
    return services.AddCommandAndQueryServices().AddDatabaseServices().AddImplementationServices();
  }

  private static IServiceCollection AddCommandAndQueryServices(this IServiceCollection services)
  {
    services.AddMediatR(configuration =>
      configuration
        .RegisterServicesFromAssemblyContaining<CreateShortUrlCommand>()
        .RegisterServicesFromAssemblyContaining<GetShortUrlQuery>()
        .AddOpenBehavior(typeof(LogRequestBehavior<,>))
        .AddOpenBehavior(typeof(ValidateRequestBehavior<,>))
    );
    services.AddTransient<IValidator<CreateShortUrlCommand>, CreateShortUrlCommandValidator>();
    services.AddTransient<IValidator<GetShortUrlQuery>, GetShortUrlQueryValidator>();
    return services;
  }

  private static IServiceCollection AddDatabaseServices(this IServiceCollection services)
  {
    services.AddDbContext<AppDbContext>();
    services.AddScoped<IDbContext>(serviceProvider =>
      serviceProvider.GetRequiredService<AppDbContext>()
    );
    services.AddScoped<DbContextOptionsBuilder<AppDbContext>>();
    services.AddScoped<IDatabaseConfiguration>(serviceProvider =>
    {
      IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
      DbContextOptionsBuilder<AppDbContext> optionsBuilder = serviceProvider.GetRequiredService<
        DbContextOptionsBuilder<AppDbContext>
      >();
      return new PostgresqlDatabaseConfiguration(configuration, optionsBuilder);
    });
    return services;
  }

  private static IServiceCollection AddImplementationServices(this IServiceCollection services)
  {
    services.AddTransient<IShortUrlFactory, ShortUrlFactory>();
    services.AddTransient<ShortUrlProvider>();
    services.AddTransient<IShortUrlProvider>(serviceProvider =>
      serviceProvider.GetRequiredService<ShortUrlProvider>()
    );
    services.AddTransient<ISequenceIdProvider>(serviceProvider =>
      serviceProvider.GetRequiredService<ShortUrlProvider>()
    );
    return services;
  }
}
