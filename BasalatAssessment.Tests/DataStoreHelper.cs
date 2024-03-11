using BasalatAssessment.Vehicle.Data.Tracking;
using BasalatAssessment.Vehicle.Data.Tracking.DataStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BasalatAssessment.Tests
{
    public class DataStoreHelper
    {
        public static DataStore GetDataStore()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

                var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(config.GetConnectionString("VehicleTracking"))
                    .Options;

                return new DataStore(new ApplicationDbContext(dbOptions));
        }
    }
}
