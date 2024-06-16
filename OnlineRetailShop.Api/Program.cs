using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Api.Middleware;
using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Application.Services;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Mappings;
using OnlineRetailShop.Infrastructure.Data;
using OnlineRetailShop.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ConnectionString Injection
builder.Services.AddDbContext<OnlineRetailShopDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineRetailShopConnectionString")));

//Service Injection
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();

//Automapper Register
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
