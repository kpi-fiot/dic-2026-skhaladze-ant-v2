namespace BookShelf.Model.Exception;

public class EntityNotFound : System.Exception
{
    public EntityNotFound(string message) : base(message)
    {
    }
}