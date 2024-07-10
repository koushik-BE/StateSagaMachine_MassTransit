namespace Newsletter.Api.Messages;

public record SendWelcomeEmail(Guid SubscriberId, string Email);

public record SendFollowUpEmail(Guid SubscriberId, string Email);

public record SubscribeToNewsletter(string Email);

public record RevertSendWelcomeEmail(Guid SubsciberId, string Email);

public record RevertSendFollowUpEmail(Guid SubsciberId, string Email);

public record RevertOnboarding(Guid SubsciberId, string Email);
