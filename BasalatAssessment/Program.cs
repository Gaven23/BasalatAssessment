using BasalatAssessment.Common;
using BasalatAssessment.Domain.Interfaces;
using BasalatAssessment.Stock.Data;
using BasalatAssessment.Vehicle.Data;
using BasalatAssessment.Vehicle.Data.Tracking;
using BasalatAssessment.Vehicle.Data.Tracking.DataStore;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BasalatAssessment.Vehicle;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Information()
            .CreateLogger();

        try
        {
            Log.Information("Starting application");

            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigurePipeline(app);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(builder.Configuration);
        var appSettings = builder.Configuration.Get<AppSettings>();
        ConfigureHttpClients(builder.Services, appSettings);
        ConfigureServices(builder.Services);
        ConfigureHsts(builder.Services);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void ConfigurePipeline(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
    }

    private static void ConfigureHttpClients(IServiceCollection services, AppSettings? appSettings)
    {
        services.AddHttpClient<VehicleHttpClient>(client =>
        {
            if (appSettings?.VehicleSettings.VehicleApiUrl == null)
            {
                throw new ArgumentNullException(nameof(appSettings.VehicleSettings.VehicleApiUrl), innerException: null);
            }

            client.BaseAddress = new Uri(appSettings.VehicleSettings.VehicleApiUrl);
        });

        services.AddHttpClient<StockHttpClient>(client =>
        {
            if (appSettings?.VehicleSettings.VehicleApiUrl == null)
            {
                throw new ArgumentNullException(nameof(appSettings.VehicleSettings.VehicleApiUrl), innerException: null);
            }

            client.BaseAddress = new Uri(appSettings.VehicleSettings.VehicleApiUrl);
        });
    }


    private static void ConfigureData(IServiceCollection services, string? smartHubConnectionString)
    {
        if (smartHubConnectionString == null)
        {
            throw new ArgumentNullException(nameof(smartHubConnectionString));
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(smartHubConnectionString);
        });

        services.AddScoped<IDataStore, DataStore>();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IVehicleDataStore, VehicleDataStore>();
        services.AddScoped<IStockDataStore, StockDataStore>();
    }

    private static void ConfigureHsts(IServiceCollection services)
    {
        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromSeconds(63072000);
        });
    }
}
