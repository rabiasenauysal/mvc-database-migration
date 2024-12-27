using System;
using Microsoft.EntityFrameworkCore;
using odevSon.Interfaces;
using odevSon.Models;
using odevSon.Repositories;
using odevSon.Services;
using ILogger = odevSon.Interfaces.ILogger;



namespace odevSon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var loggerType = builder.Configuration.GetValue<string>("LoggerType");
            if (loggerType == "File")
                builder.Services.AddScoped<ILogger, FileLogger>();
            else
                builder.Services.AddScoped<ILogger, DatabaseLogger>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations(); // Swagger açýklamalarýný etkinleþtirin
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // Register DbContext
            builder.Services.AddDbContext<ProductVT>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repositories and Services
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            // Logger Dependency Injection
            builder.Services.AddScoped<ILogger, FileLogger>(); // Varsayýlan olarak FileLogger atanabilir

           //builder.Services.AddSingleton<ILogger>(provider => new FileLogger("logs/app.log"));


            // Add Swagger/OpenAPI
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
        }
    }
}
