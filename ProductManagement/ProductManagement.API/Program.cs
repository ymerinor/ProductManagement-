using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductManagement.API.Middleware;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Application.Product.Services;
using ProductManagement.Application.Product.Validations;
using ProductManagement.Domain.Core;
using ProductManagement.Domain.ExternalServices;
using ProductManagement.Domain.Repository.Interface;
using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.Core;
using ProductManagement.Infrastructure.ExternalServices;
using ProductManagement.Infrastructure.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductManagementDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("ProductManagementConnection")));

builder.Services.AddTransient<IProductRepository, ProductRepository>();
ConfigureServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product Management demo.",
        Description = "Demo API - para el productos  de la compañia Tekton ",
        Contact = new OpenApiContact
        {
            Name = "Yeiner  Merino",
        }
    });
});

// Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

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
        client.BaseAddress = new Uri(builder.Configuration.GetSection("UrlApiDiscount").Value);
    });
    services.AddSingleton<IProductStatusCache, MemoryCacheAdapter>();
    services.AddMemoryCache();
    services.AddTransient<IProductService, ProductService>();
    services.AddTransient<IProductApiClient, ProductApiClient>();
    builder.Services.AddScoped<IValidator<ProductsRequestDto>, ProductsRequestDtoValidator>();

}
