using FCG.API.Configuration;
using FCG.API.Configuration.Jwt;
using FCG.API.Configuration.Middleware.CorrelationId;
using FCG.API.Configuration.Middleware.GlobalExceptionHandling;
using FCG.API.Configuration.Middleware.RequestLogging;
using FCG.API.Configuration.Swagger;
using FCG.API.Endpoints;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.RegisterDependencies();

builder.Services.AddMemoryCache();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
    .AddPolicy("User", policy => policy.RequireRole("User"));

WebApplication app = builder.Build();

app.UseGlobalExceptionHandling();
app.UseCorrelationMiddleware();
app.UseRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapGroup("/api/v1/")
   .WithTags("Login endpoints")
   .MapLoginEndpoints();
app.MapGroup("/api/v1/")
   .WithTags("User endpoints")
   .MapUserEndpoints();
app.MapGroup("/api/v1/")
   .WithTags("Game endpoints")
   .MapGameEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
