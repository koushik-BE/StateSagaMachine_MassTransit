using MassTransit;
using Newsletter.Api.Entities;
using Newsletter.Api.Messages;
using Newsletter.Api.Mongo;

namespace Newsletter.Api.Handlers;

public class SubscribeToNewsletterHandler(MongoDbContext _dbContext) : IConsumer<SubscribeToNewsletter>
{
    public async Task Consume(ConsumeContext<SubscribeToNewsletter> context)
    {
        var subscriber = new Subscriber
        {
            Id = Guid.NewGuid(),
            Email = context.Message.Email,
            SubscribedOnUtc = DateTime.UtcNow
        };

        await _dbContext.Subscribers.InsertOneAsync(subscriber);

        await context.Publish(new SubscriberCreated
        {
            SubscriberId = subscriber.Id,
            Email = context.Message.Email
        });
    }
}
