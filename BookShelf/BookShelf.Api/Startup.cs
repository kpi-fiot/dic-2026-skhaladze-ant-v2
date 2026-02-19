using AutoMapper;
using BookShelf.Api.Book;
using BookShelf.Dal;
using BookShelf.Dal.Book;
using BookShelf.Model.Book;
using BookShelf.Orchestartor.Book;
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

        services.AddAutoMapper(config => config.AddProfiles(
            new List<Profile>
            {
                new BookProfile(),
                new DaoProfile()
            }));

        ConfigureDb(services);
    }

    protected virtual void ConfigureDb(IServiceCollection services)
    {
        services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookShelf API v1"));

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}