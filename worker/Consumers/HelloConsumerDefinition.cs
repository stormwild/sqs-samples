namespace Company.Consumers
{
    using MassTransit;

    public class HelloConsumerDefinition :
        ConsumerDefinition<HelloConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<HelloConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}