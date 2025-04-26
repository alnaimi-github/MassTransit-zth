using HelloApi.Contracts;
using MassTransit;

namespace HelloApi.Filters
{
    public class TenantPublishMessageFilter : IFilter<PublishContext<Email>>
    {
        public void Probe(ProbeContext context)
        {
            throw new NotImplementedException();
        }

        public Task Send(PublishContext<Email> context, IPipe<PublishContext<Email>> next)
        {
            Console.WriteLine("TenantPublishMessageFilter");
            return next.Send(context);
        }
    }
}
