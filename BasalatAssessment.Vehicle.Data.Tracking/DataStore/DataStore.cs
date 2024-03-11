using BasalatAssessment.Vehicle.Data.Tracking.Entities;

namespace BasalatAssessment.Vehicle.Data.Tracking.DataStore
{
    public partial class DataStore : IDataStore
    {
        private readonly ApplicationDbContext _dbContext;

        public DataStore(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
