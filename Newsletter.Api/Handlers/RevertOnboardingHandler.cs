using MassTransit;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Handlers
{
    public class RevertOnboardingHandler : IConsumer<RevertOnboarding>
    {
        private readonly ILogger<RevertOnboardingHandler> _logger;

        public RevertOnboardingHandler( ILogger<RevertOnboardingHandler> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RevertOnboarding> context)
        {
            _logger.LogInformation("❌ Rolling back onboarding tasks");

            await context.Publish<Fault<FollowUpEmailSent>>(new
            {
                Message = new FollowUpEmailSent()
                {
                    SubscriberId = context.Message.SubsciberId,
                    Email = context.Message.Email
                }
            });
        }
    }
}
