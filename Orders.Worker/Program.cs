using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Application.Data;
using Orders.Worker.Consumers;

namespace Orders.Worker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<OrdersDbContext>(x =>
                    {

                        x.UseNpgsql("Server=localhost;Port=5432;Database=Store;User ID=postgres;Password=postgres;", opt =>
                        {
                            opt.EnableRetryOnFailure(5);
                        });
                    });

                    services.AddMassTransit(x =>
                    {

                        x.AddConsumer<OrderCreatedConsumer>();
                        x.AddEntityFrameworkOutbox<OrdersDbContext>(o =>
                        {
                            o.QueryDelay = TimeSpan.FromSeconds(5);
                            o.UsePostgres();
                        });

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}
