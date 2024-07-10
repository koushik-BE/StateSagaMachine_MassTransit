using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newsletter.Api.Emails;
using Newsletter.Api.Messages;
using Newsletter.Api.Mongo;
using Newsletter.Api.Sagas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>(provider =>
{
    return new MongoDbContext("mongodb://localhost:27017", "DemoSaga");
});

builder.Services.AddTransient<IEmailService, EmailService>();


builder.Services.AddMassTransit(busConfigurator =>
{
   // busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumers(typeof(Program).Assembly);

    busConfigurator.AddSagaStateMachine<NewsletterOnboardingSaga, NewsletterOnboardingSagaData>().MongoDbRepository(r =>
    {
        r.Connection = "mongodb://localhost:27017";
        r.DatabaseName = "DemoSaga";
        r.CollectionName = "SagaDatas";
    });

    busConfigurator.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host("localhost", "/", c =>
            {
                c.Username("guest");
                c.Password("guest");
            });

            cfg.ConfigureEndpoints(ctx);
        });
 });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/newsletters", async ([FromBody] string email, IBus bus) =>
{
    await bus.Publish(new SubscribeToNewsletter(email));

    return Results.Accepted();
});

app.UseHttpsRedirection();

app.Run();
