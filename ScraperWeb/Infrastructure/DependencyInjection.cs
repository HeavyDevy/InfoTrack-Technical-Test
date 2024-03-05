using Application.Common.Interfaces;
using Application.Settings;
using Infrastructure.HttpClients.External;
using Infrastructure.Services.SearchUtils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using ScraperWeb.Persistance.Data;
using System;
using System.Runtime;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ScraperDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddHttpClient<SearchHttpClient>();
        services.AddScoped<ISearchService,SearchService>();





        services.AddSingleton(TimeProvider.System);

        return services;
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SearchSettings>(configuration.GetSection(nameof(SearchSettings)));
        

        return services;
    }
}
