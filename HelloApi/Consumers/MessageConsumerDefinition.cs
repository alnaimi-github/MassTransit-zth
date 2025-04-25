using MassTransit;

namespace HelloApi.Consumers
{
    public class MessageConsumerDefinition : ConsumerDefinition<MessageConsumer>
    {
        public MessageConsumerDefinition()
        {
            Endpoint(x => x.Name = "message.consumer");
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<MessageConsumer> consumerConfigurator,
            IRegistrationContext context)
        {

            consumerConfigurator.ConcurrentMessageLimit = 2;
            //  consumerConfigurator.UseMessageRetry(x => x.Interval(5, TimeSpan.FromSeconds(3)));
            consumerConfigurator.UseCircuitBreaker(cb =>
            {
                cb.TrackingPeriod = TimeSpan.FromSeconds(1);
                cb.TripThreshold = 15;
                cb.ActiveThreshold = 10;
                cb.ResetInterval = TimeSpan.FromMinutes(5);
            });
        }
    }
}
