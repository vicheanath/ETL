using ETL.Core.Interfaces;
using ETL.Core.Services;
using ETL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ETL.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddETLScheduling(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }
    
    public static IServiceCollection AddETLLogging(this IServiceCollection services, IConfiguration config)
    {
        

        return services;
    }
    
    public static IServiceCollection AddETLPlugins(this IServiceCollection services)
    {
        // Load assemblies dynamically (e.g., ETL.DataSources.dll)
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName.StartsWith("ETL."));

        foreach (var assembly in assemblies)
        {
            // Register data sources
            var dataSourceTypes = assembly.GetTypes()
                .Where(t => typeof(IDataSourceProvider).IsAssignableFrom(t) && !t.IsInterface);
            foreach (var type in dataSourceTypes)
                services.AddTransient(typeof(IDataSourceProvider), type);

            // Similarly register transformations and outputs
        }

        return services;
    }
    
    public static IServiceCollection AddETLCore(this IServiceCollection services, IConfiguration config)
    {
        // Register core services
        services.AddTransient<IPipelineOrchestrator, PipelineOrchestrator>();
        services.AddTransient<SchedulerService>();

        // Register plugins dynamically
        services.AddETLPlugins();

        // Register Quartz.NET scheduler
        services.AddETLScheduling();
        
        // Register logging
        services.AddETLLogging(config);

        // Register database context
        services.AddDbContext<ETLDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("ETLDatabase")));

        return services;
    }
}