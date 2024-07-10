using MassTransit;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Handlers
{
    public class RevertSendFollowUpEmailHandler : IConsumer<RevertSendFollowUpEmail>
    {
        private readonly ILogger<RevertSendFollowUpEmailHandler> _logger;

        public RevertSendFollowUpEmailHandler(ILogger<RevertSendFollowUpEmailHandler> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RevertSendFollowUpEmail> context)
        {
            _logger.LogInformation("❌ Rolling back follow-up tasks");

            await context.Publish<Fault<WelcomeEmailSent>>(new
            {
                Message = new WelcomeEmailSent()
                {
                    SubscriberId = context.Message.SubsciberId,
                    Email = context.Message.Email
                }
            });
        }
    }
}
