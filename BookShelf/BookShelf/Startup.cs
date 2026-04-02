using AutoMapper;
using BookShelf.Api.Book;
using BookShelf.Api.Shelf;
using BookShelf.Dal;
using BookShelf.Dal.Book;
using BookShelf.Dal.Shelf;
using BookShelf.Model.Book;
using BookShelf.Model.Shelf;
using BookShelf.Orchestrator.Book;
using BookShelf.Orchestrator.Shelf;
using Microsoft.EntityFrameworkCore;

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

        services.AddAutoMapper(config => config.AddProfiles(
            new List<Profile>
            {
                new BookMap(),
                new DaoMap(),
                new ShelfMap()
            }));

        ConfigureDb(services);
    }

    protected virtual void ConfigureDb(IServiceCollection services)
    {
        services.AddDbContext<SqlDbContext>(
            c => c.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<CosmosDbContext>(
            c=> c.UseCosmos(_configuration.GetConnectionString("CosmosConnection"), "dic-2026"));
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