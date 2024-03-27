using Microsoft.OpenApi.Models;
using PruneUrl.Backend.API;
using PruneUrl.Backend.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder
  .Logging.ClearProviders() // Remove defaults
  .AddConsole();

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
builder.Services.AddHttpLogging(logging =>
{
  logging.CombineLogs = true;
});

builder.Services.AddAppServices();

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

await app.RunAsync();
