using BasalatAssessment.Common;
using BasalatAssessment.Domain.Interfaces;
using BasalatAssessment.Stock.Data;
using BasalatAssessment.Vehicle.Data;
using BasalatAssessment.Vehicle.Data.Hangfire;
using BasalatAssessment.Vehicle.Data.Tracking;
using BasalatAssessment.Vehicle.Data.Tracking.DataStore;
using Hangfire;
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
            var backgroundJobs = app.Services.GetRequiredService<IBackgroundJobClient>();
            ConfigurePipeline(app, backgroundJobs);

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
        ConfigureData(builder.Services, appSettings?.ConnectionStrings?.VehicleTracking);
        ConfigureServices(builder.Services, builder.Configuration);
        ConfigureHttpClients(builder.Services, appSettings);
        ConfigureHsts(builder.Services);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void ConfigurePipeline(WebApplication app, IBackgroundJobClient backgroundJobs)
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
        app.UseStaticFiles();

        app.UseHangfireDashboard();

        // Enqueue a background job
        backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
        RecurringJob.AddOrUpdate<HangFireService>("Process Data Messages", x => x.SaveVehicleDataAsync(), Cron.Hourly);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute();
        });
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

    private static void ConfigureData(IServiceCollection services, string? VehicleTrackingConnectionString)
    {
        if (VehicleTrackingConnectionString == null)
        {
            throw new ArgumentNullException(nameof(VehicleTrackingConnectionString));
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(VehicleTrackingConnectionString);
        });

        services.AddScoped<IDataStore, DataStore>();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IVehicleDataStore, VehicleDataStore>();
        services.AddScoped<IStockDataStore, StockDataStore>();
        services.AddScoped<IVehicleDataStore, VehicleDataStore>();
        // Add Hangfire services.
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("VehicleTracking")));

        // Add the processing server as IHostedService
        services.AddHangfireServer();
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
