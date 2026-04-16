using Azure.Messaging.ServiceBus;
using BookShelf.Model.MessageBroker;

namespace BookShelf.Platform.MessageBroker;

public class ShelfStatsPublisher : IPublisher
{
    private readonly ServiceBusClient _client;
    protected virtual string TopicName => "shell-create-audit-queue";
    private readonly ServiceBusSender _publisher;

    public ShelfStatsPublisher(ServiceBusClient client)
    {
        _client = client;
        _publisher = _client.CreateSender(TopicName);
    }

    public async Task PublishAsync(Guid id)
    {
        await _publisher.SendMessageAsync(new ServiceBusMessage(id.ToString("N")));
    }
}
