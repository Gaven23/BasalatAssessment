using BasalatAssessment.Vehicle.Data.Tracking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasalatAssessment.Vehicle.Data.Tracking.DataStore
{
    partial class DataStore
    {
        public async Task<IEnumerable<VehicleDetails>> GetVehiclesDetailsAsync(Guid vehicleId, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.VehicleDetails.AsNoTracking().Where(e => e.VehicleId == vehicleId);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task SaveVehicleDetailsAsync(VehicleDetails vehicleDetails)
        {
            var query = _dbContext.VehicleDetails.AsNoTracking().Where(e => e.Id == vehicleDetails.Id);

            if (query == null)
            {
                _dbContext.VehicleDetails.Add(vehicleDetails);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
