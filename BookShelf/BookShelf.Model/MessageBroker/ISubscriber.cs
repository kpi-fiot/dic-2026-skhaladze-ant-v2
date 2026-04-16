namespace BookShelf.Model.MessageBroker;

public interface ISubscriber
{
    Task SubscribeAsync();

    public List<string> Data { get; }
}
