using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BookShelf.Platform.BlobStorage;

public class BlobStorage : IBlobStorage
{
    private readonly BlobConfiguration _configuration;
    private readonly BlobServiceClient _client;

    public BlobStorage(BlobConfiguration configuration, BlobServiceClient client)
    {
        _configuration = configuration;
        _client = client;
    }

    public async Task UploadBlobAsync(string fileName)
    {
        await _client
            .GetBlobContainerClient(_configuration.ContainerName)
            .GetBlobClient(fileName)
            .UploadAsync(new MemoryStream());
    }

    public async Task<IEnumerable<int>> GetAllFilesNameAsync(Guid shelfId)
    {
        var books = _client
            .GetBlobContainerClient(_configuration.ContainerName)
            .GetBlobs(BlobTraits.None, BlobStates.None, shelfId.ToString(), CancellationToken.None)
            .AsPages(default, 10000)
            .SelectMany(c => c.Values).Select(c => int.Parse(c.Name.Split("_").Last())).ToList();

        return books;
    }

    public async Task<bool> ExistsAsync(string fileName)
    {
        return await _client
            .GetBlobContainerClient(_configuration.ContainerName)
            .GetBlobClient(fileName)
            .ExistsAsync();
    }
}