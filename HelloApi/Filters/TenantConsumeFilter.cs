using MassTransit;

namespace HelloApi.Filters
{
    public class TenantConsumeFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        public void Probe(ProbeContext context)
        {
            throw new NotImplementedException();
        }

        public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var tenantValueFromPublish = context.Headers.Get<string>("Tenant-From-Publish");
            var tenantValueFromSend = context.Headers.Get<string>("Tenant-From-Send");
            if (tenantValueFromPublish != null)
            {
                Console.WriteLine($"Publish:{tenantValueFromPublish}");
            }

            if (tenantValueFromSend!= null)
            {
                Console.WriteLine($"Send:{tenantValueFromSend}");
            }

            return next.Send(context);
        }
    }
}
