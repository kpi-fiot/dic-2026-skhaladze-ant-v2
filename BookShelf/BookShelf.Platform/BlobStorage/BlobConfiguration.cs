namespace BookShelf.Platform.BlobStorage;

public class BlobConfiguration
{
    public virtual string ConnectionString { get; set; }
    public string ContainerName{ get; set; }
}