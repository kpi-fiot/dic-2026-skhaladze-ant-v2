using Azure.Messaging.ServiceBus;
using BookShelf.Model.MessageBroker;

namespace BookShelf.Platform.MessageBroker;

public class ShelfStatsSubscriber : ISubscriber
{
    private readonly ServiceBusClient _client;
    protected virtual string TopicName => "shell-create-audit-queue";

    private readonly List<string> _data = new List<string>();
    public List<string> Data => _data;
    private ServiceBusProcessor _processor;

    public ShelfStatsSubscriber(ServiceBusClient client)
    {
        _client = client;
    }

    public async Task SubscribeAsync()
    {
        _processor = _client.CreateProcessor(TopicName);
        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;

        await _processor.StartProcessingAsync();
    }

    private async Task ErrorHandler(ProcessErrorEventArgs arg)
    {
        Console.WriteLine(arg.Exception);
    }

    private async Task MessageHandler(ProcessMessageEventArgs arg)
    {
        var shelfId = arg.Message.Body.ToString();
        _data.Add(shelfId);

        await arg.CompleteMessageAsync(arg.Message);
    }
}