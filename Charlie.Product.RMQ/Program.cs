using Charlie.Product.API;
using Charlie.Product.DataAccess;
using Charlie.Product.DataAccess.Repositories;
using Charlie.Product.RMQ;
using Charlie.Product.Shared.Mappers;
using Charlie.Product.Shared.Models;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ProductDb");

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<RabbitMqClient>();

builder.Services.AddScoped<IProductRepository<ProductModel>, ProductRepository>();
builder.Services.AddSingleton<ProductMapper>();

builder.Services.AddHostedService<Worker>();



var host = builder.Build();
await host.RunAsync();
