namespace BookShelf.Platform.BlobStorage;

public interface IBlobStorage
{
    Task UploadBlobAsync(string fileName);
    Task<IEnumerable<int>> GetAllFilesNameAsync(Guid shelfId);
    Task<bool> ExistsAsync(string fileName);
}