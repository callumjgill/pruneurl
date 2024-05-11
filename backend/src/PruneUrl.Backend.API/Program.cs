using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PruneUrl.Backend.API;
using PruneUrl.Backend.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder
  .Logging.ClearProviders() // Remove defaults
  .AddConsole();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
  options.AddDefaultPolicy(policy =>
    policy
      .AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod()
      .WithExposedHeaders(HeaderNames.Location)
  )
);

if (builder.Environment.IsDevelopment())
{
  builder.Services.AddSwaggerGen(options =>
  {
    options.SwaggerDoc(
      "v1",
      new OpenApiInfo
      {
        Version = "v1",
        Title = "PruneURL REST API",
        Description =
          "An ASP.NET Core Minimal API for managing the backend of the PruneURL application."
      }
    );
  });
}

builder.Services.AddHttpLogging(logging =>
{
  logging.CombineLogs = true;
});

builder.Services.AddAppServices();

WebApplication app = builder.Build();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PruneURL REST API V1");
  });
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler(exceptionHandlerApp =>
    exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context))
  );
}

app.UseHttpsRedirection();
app.UseCors();

app.MapEndpointRoutes();

await app.RunAsync();
