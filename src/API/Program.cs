using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Services;
using Business.Services;
using Business.Services.Implementations;
using Data.Repositories;
using Data.Repositories.Implementations;

class Program
{
    static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        string? connStr = configuration.GetConnectionString("Default");
        if (string.IsNullOrEmpty(connStr))
        {
            Console.WriteLine("Connection string is missing in appsettings.json.");
            return;
        }
        string connectionString = connStr;

        var services = new ServiceCollection();

        services.AddTransient<IMessageRepository>(provider =>
            new MessageRepository(connectionString));
        services.AddTransient<IMessageProcessor, MessageProcessor>();
        services.AddSingleton<NatsSubscriberService>(provider =>
            new NatsSubscriberService(provider.GetRequiredService<IMessageProcessor>().ProcessMessageAsync)
        );

        var provider = services.BuildServiceProvider();
        var subscriber = provider.GetRequiredService<NatsSubscriberService>();
        subscriber.Subscribe();

        Console.CancelKeyPress += (sender, e) =>
        {
            Console.WriteLine("Shutting down...");
            subscriber.Dispose();
        };

        await Task.Delay(Timeout.Infinite);
    }
}
