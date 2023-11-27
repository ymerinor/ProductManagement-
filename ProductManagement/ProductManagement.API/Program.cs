using FluentValidation;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Application.Product.Services;
using ProductManagement.Application.Product.Validations;
using ProductManagement.Domain.ExternalServices;
using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IProductService, ProductService>();
    builder.Services.AddScoped<IValidator<ProductsRequestDto>, ProductsRequestDtoValidator>();
    services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
    {
        client.BaseAddress = new Uri("https://6563e225ceac41c0761d2b8c.mockapi.io/id/");
    });
}
