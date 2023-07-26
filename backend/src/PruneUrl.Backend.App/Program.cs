using Autofac;
using Autofac.Extensions.DependencyInjection;
using PruneUrl.Backend.Infrastructure.IoC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders() // Remove defaults
               .AddConsole();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterAllModules());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHttpLogging();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
}

app.UseHttpsRedirection();

await app.RunAsync();