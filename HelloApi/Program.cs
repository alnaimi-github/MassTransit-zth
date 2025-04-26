using HelloApi.Consumers;
using HelloApi.Contracts;
using HelloApi.Filters;
using HelloApi.Orders.Consumers;
using HelloApi.Orders.Contracts;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Tenant>();

            builder.Services.AddMassTransit(x =>
            { //consumers
              //var entryAssembly = Assembly.GetEntryAssembly();
              //x.AddConsumers(entryAssembly);
              // x.SetKebabCaseEndpointNameFormatter();
              // x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("hellos"));
              // x.AddConsumer(typeof(MessageConsumer));
                x.AddConsumer<MessageConsumer>();
                x.AddConsumer<MyRequestConsumer>();
                x.AddConsumer<VerifyOrderConsumer>();

                x.AddRequestClient<MyRequest>();
                x.AddRequestClient<VerifyOrder>();

                // x.AddConsumer<SayByeConsumer>();
                // x.AddConsumer<MessageConsumer, MessageConsumerDefinition>();
                //x.AddConsumer<MessageConsumer>()
                // .Endpoint(e =>
                // {
                //     e.Name = "salutation";
                // });

                x.UsingRabbitMq((context, cfg) =>
                {
                    // cfg.UseSendFilter(typeof(TenantSendFilter<>), context);
                    // cfg.UsePublishFilter(typeof(TenantPublishFilter<>), context);

                    //cfg.UsePublishFilter(typeof(TenantPublishFilter<>), context,
                    //    x=>x.Include(typeof(Message)));

                    // cfg.UseConsumeFilter(typeof(TenantConsumeFilter<>), context);
                    //this will be fixed in a new versiin of MassTransit
                    //cfg.UsePublishFilter<TenantPublishMessageFilter>(context);

                    cfg.ConfigurePublish(x =>
                    {
                        x.UseFilter<Email>(new TenantPublishMessageFilter());
                    });


                    //cfg.UseMessageRetry(r =>
                    //{
                    //   // r.Handle<ArgumentNullException>();
                    //    r.Ignore(typeof(InvalidOperationException));
                    //    r.Immediate(3);
                    //    //r.Ignore<ArgumentException>(t => t.ParamName == "orderTotal");
                    //});

                    cfg.ReceiveEndpoint("manually-configured", e =>
                    {
                        e.UseMessageRetry(r =>
                        {
                            // r.Ignore(typeof(InvalidOperationException));
                            r.Immediate(2);
                        });
                        e.ConfigureConsumer<MessageConsumer>(context);


                    });

                    cfg.UseDelayedRedelivery(r =>
                    {
                        r.Intervals(
                            TimeSpan.FromSeconds(10),
                            TimeSpan.FromMinutes(15),
                            TimeSpan.FromMinutes(30));
                    });

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
