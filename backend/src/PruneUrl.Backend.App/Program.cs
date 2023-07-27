using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PruneUrl.Backend.App.Endpoints;
using PruneUrl.Backend.Infrastructure.IoC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders() // Remove defaults
               .AddConsole();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "pruneurl.com REST API",
    Description = "An ASP.NET Core Minimal API for managing the backend of pruneurl.com"
  });
});
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterAllModules());

var app = builder.Build();

app.MapEndpointRoutes();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
}

app.UseHttpsRedirection();

await app.RunAsync();