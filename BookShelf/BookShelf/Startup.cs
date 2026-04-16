using AutoMapper;
using Azure.Storage.Blobs;
using BookShelf.Api.Book;
using BookShelf.Api.Shelf;
using BookShelf.Dal;
using BookShelf.Dal.Book;
using BookShelf.Dal.Shelf;
using BookShelf.Model.Book;
using BookShelf.Model.BookShelf;
using BookShelf.Model.Shelf;
using BookShelf.Orchestrator.Book;
using BookShelf.Orchestrator.BookShelf;
using BookShelf.Orchestrator.Shelf;
using BookShelf.Platform.BlobStorage;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Azure.Messaging.ServiceBus;
using BookShelf.Model.MessageBroker;
using BookShelf.Platform.MessageBroker;

namespace BookShelf.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<IBookOrchestrator, BookOrchestrator>();
        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<IShelfOrchestrator, ShelfOrchestrator>();
        services.AddScoped<IShelfRepository, ShelfRepository>();

        services.AddScoped<IBooksShelfOrchestrator, BookShelfOrchestrator>();

        services.AddAutoMapper(config => config.AddProfiles(
            new List<Profile>
            {
                new BookMap(),
                new DaoMap(),
                new ShelfMap()
            }));

        ConfigureDb(services);
        ConfigureEdgeServices(services);
    }

    protected virtual void ConfigureDb(IServiceCollection services)
    {
        services.AddDbContext<SqlDbContext>(
            c => c.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<CosmosDbContext>(
            c=> c.UseCosmos(_configuration.GetConnectionString("CosmosConnection"), "dic-2026"));
    }

    protected virtual void ConfigureEdgeServices(IServiceCollection services)
    {
        var blobConfig = new BlobConfiguration();
        _configuration.Bind("AzureBlobContainerConnectionString", blobConfig);
        services.AddSingleton(blobConfig);

        var client = new BlobServiceClient(blobConfig.ConnectionString);
        services.AddScoped<IBlobStorage, BlobStorage>();
        services.AddScoped(_ => client);

        services.AddSingleton(typeof(ServiceBusClient), new ServiceBusClient(_configuration.GetConnectionString("ServiceBusConnectionString")));
        services.AddScoped<IPublisher, ShelfStatsPublisher>();

        var subscriber = new ShelfStatsSubscriber(new ServiceBusClient(_configuration.GetConnectionString("ServiceBusConnectionString")));
        subscriber.SubscribeAsync().GetAwaiter().GetResult();

        services.AddScoped<ISubscriber>(_ => subscriber);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<Exception.GlobalExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}