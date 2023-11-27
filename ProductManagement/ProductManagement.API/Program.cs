using FluentValidation;
using ProductManagement.API.Middleware;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Application.Product.Services;
using ProductManagement.Application.Product.Validations;
using ProductManagement.Domain.Core;
using ProductManagement.Domain.ExternalServices;
using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.Core;
using ProductManagement.Infrastructure.ExternalServices;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddInfrastructure(builder.Configuration);
ConfigureServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();
app.UseResponseTimeLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddHttpClient<IProductApiClient, ProductApiClient>("ApiDataApiClient", client =>
    {
        client.BaseAddress = new Uri("https://6563e225ceac41c0761d2b8c.mockapi.io/id/");
    });
    services.AddSingleton<IProductStatusCache, MemoryCacheAdapter>();
    services.AddMemoryCache();
    services.AddTransient<IProductService, ProductService>();
    services.AddTransient<IProductApiClient, ProductApiClient>();
    builder.Services.AddScoped<IValidator<ProductsRequestDto>, ProductsRequestDtoValidator>();

}
