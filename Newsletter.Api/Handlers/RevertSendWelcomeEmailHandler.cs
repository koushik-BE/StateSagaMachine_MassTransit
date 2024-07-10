using MassTransit;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Handlers
{
    public class RevertSendWelcomeEmailHandler : IConsumer<RevertSendWelcomeEmail>
    {
        private readonly ILogger<RevertSendWelcomeEmail> _logger;

        public RevertSendWelcomeEmailHandler(ILogger<RevertSendWelcomeEmail> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<RevertSendWelcomeEmail> context)
        {
            _logger.LogInformation("❌ Rolling back welcome email sending job");

            return Task.CompletedTask;
        }
    }
}
