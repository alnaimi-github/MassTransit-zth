
using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Application.Data;
using Orders.Application.Services;
using OrdersApi.Infrastructure;

namespace OrdersApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrdersDbContext>(x =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MyStoreDb");

                x.UseNpgsql(connectionString, options =>
                {

                    options.MigrationsAssembly(typeof(OrdersDbContext).Assembly.GetName().Name);
                    options.MigrationsHistoryTable($"__{nameof(OrdersDbContext)}");

                    options.EnableRetryOnFailure(5);
                    options.MinBatchSize(1);

                });

            });

            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddHostedService<RecreateDatabaseHostedService<OrdersDbContext>>();

            builder.Services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<OrdersDbContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(5);
                    o.UsePostgres();
                    o.UseBusOutbox();
                    //o.UseBusOutbox(x => x.DisableDeliveryService());
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

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
