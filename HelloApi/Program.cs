using HelloApi.Consumers;
using MassTransit;
using System.Reflection;

namespace HelloApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddOptions<RabbitMqTransportOptions>().BindConfiguration("RabbitMq");

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(x =>
            {
                //consumers
                //var entryAssembly = Assembly.GetEntryAssembly();
                //x.AddConsumers(entryAssembly);
                // x.SetKebabCaseEndpointNameFormatter();
                // x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("hellos"));
                // x.AddConsumer(typeof(MessageConsumer));
                //x.AddConsumer<MessageConsumer>();

                 x.AddConsumer<SayByeConsumer>();
                 x.AddConsumer<MessageConsumer, MessageConsumerDefinition>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    //cfg.Host("localhost", "/", h =>
                    //{
                    //    h.Username("guest");
                    //    h.Password("guest");
                    //});

                    cfg.ConfigureEndpoints(context);
                });
            });

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
