using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PruneUrl.Backend.API;
using PruneUrl.Backend.Application.Configuration;
using PruneUrl.Backend.Infrastructure.Database.Firestore;
using PruneUrl.Backend.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder
  .Logging.ClearProviders() // Remove defaults
  .AddConsole();

builder
  .Services.AddOptions<SequenceIdOptions>()
  .Bind(builder.Configuration.GetSection(nameof(SequenceIdOptions)));
builder
  .Services.AddOptions<FirestoreTransactionOptions>()
  .Bind(builder.Configuration.GetSection(nameof(FirestoreTransactionOptions)));
builder
  .Services.AddOptions<FirestoreDbOptions>()
  .Bind(builder.Configuration.GetSection(nameof(FirestoreDbOptions)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc(
    "v1",
    new OpenApiInfo
    {
      Version = "v1",
      Title = "PruneUrl REST API",
      Description = "An ASP.NET Core Minimal API for managing the backend of PruneUrl"
    }
  );
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterAllModules());

var app = builder.Build();

app.MapEndpointRoutes();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PruneUrl REST API V1");
  });
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler(exceptionHandlerApp =>
    exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context))
  );
}

app.UseHttpsRedirection();

await app.EnsureDbIsSetup();

await app.RunAsync();
