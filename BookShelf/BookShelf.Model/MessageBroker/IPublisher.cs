namespace BookShelf.Model.MessageBroker;

public interface IPublisher
{
    Task PublishAsync(Guid id);
}
