using MassTransit;

namespace HelloApi.Filters
{
    public class TenantPublishFilter<T> : IFilter<PublishContext<T>> where T : class
    {
        private readonly Tenant tenant;

        public TenantPublishFilter(Tenant tenant)
        {
            this.tenant = tenant;
        }

        public void Probe(ProbeContext context)
        {
            throw new NotImplementedException();
        }

        public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
        {

            if (!string.IsNullOrEmpty(tenant.MyValue))
            {
                context.Headers.Set("Tenant-From-Publish", tenant.MyValue);

            }
            return next.Send(context);
        }
    }
}
