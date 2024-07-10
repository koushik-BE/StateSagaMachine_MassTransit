namespace Newsletter.Api.Emails;

internal sealed class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public async Task SendWelcomeEmailAsync(string email)
    {
        await Task.Delay(100);
        logger.LogInformation("Sending welcome email to {Email}", email);
    }

    public async Task SendFollowUpEmailAsync(string email)
    {
        await Task.Delay(100);
        logger.LogInformation("Sending follow-up email to {Email}", email);  
    }
}
