using MassTransit;
using Newsletter.Api.Emails;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Handlers;

public class SendWelcomeEmailHandler(IEmailService emailService) : IConsumer<SendWelcomeEmail>
{
    public async Task Consume(ConsumeContext<SendWelcomeEmail> context)
    {

        try
        {
            await emailService.SendWelcomeEmailAsync(context.Message.Email);
            await context.Publish(new WelcomeEmailSent
            {
                SubscriberId = context.Message.SubscriberId,
                Email = context.Message.Email
            });
        }
        catch 
        {
            await context.Publish<Fault<WelcomeEmailSent>>(new
            {
                Message = new WelcomeEmailSent()
                {
                    SubscriberId = context.Message.SubscriberId,
                    Email = context.Message.Email
                }
            });
        }
       
    }
}
