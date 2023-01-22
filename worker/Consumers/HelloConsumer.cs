using Microsoft.Extensions.Logging;

namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class HelloConsumer :
        IConsumer<Hello>
    {
        private readonly ILogger<HelloConsumer> _logger;

        public HelloConsumer(ILogger<HelloConsumer> logger)
        {
            _logger = logger;
        }
        
        public Task Consume(ConsumeContext<Hello> context)
        {
            _logger.LogInformation("Hello {Name}", context.Message.Name);
            return Task.CompletedTask;
        }
    }
}