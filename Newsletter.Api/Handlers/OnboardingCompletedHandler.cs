using MassTransit;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Handlers;

public class OnboardingCompletedHandler(ILogger<OnboardingCompletedHandler> _logger) : IConsumer<OnboardingCompleted>
{
    public async Task Consume(ConsumeContext<OnboardingCompleted> context)
    {
        try
        {
            _logger.LogInformation("✅ OnboardingCompletedHandler");

            //throw new Exception();

            await context.Publish(new JobCompleted()
            {
                SubscriberId = context.Message.SubscriberId,
                Email = context.Message.Email
            });
        }
        catch
        {
            _logger.LogError("❌ Error encountered.........");
            _logger.LogError("❌ Reverting all changes");

            await context.Publish<Fault<OnboardingCompleted>>(new
            {
                Message = new OnboardingCompleted()
                {
                    SubscriberId = context.Message.SubscriberId,
                    Email = context.Message.Email
                }
            });
        }
    }
}
