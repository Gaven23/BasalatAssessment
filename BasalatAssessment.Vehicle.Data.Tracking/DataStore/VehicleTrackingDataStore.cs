using BasalatAssessment.Vehicle.Data.Tracking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasalatAssessment.Vehicle.Data.Tracking.DataStore
{
    partial class DataStore
    {
        public async Task<IEnumerable<VehicleTracking>> GetVehiclesAsync(Guid vehicleId, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.VehicleTracking.AsNoTracking().Where(e => e.VehicleId == vehicleId);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task SaveVehicleDetailsAsync(BasalatAssessment.Domain.Models.VehicleTracking vehicleTracking)
        {
            var newVehicle = new VehicleTracking
            {
                VehicleId = vehicleTracking.VehicleId,
                VehicleName = vehicleTracking.VehicleName,
                VehicleNumber = vehicleTracking.VehicleNumber,
                VehicleMake = vehicleTracking.VehicleMake,
                VehicleModel = vehicleTracking.VehicleModel,
                VehicleEngineNum = vehicleTracking.VehicleEngineNum,
                Comment = vehicleTracking.Comment,
                CreatedOn = DateTime.Now,
                ModifiedOn = vehicleTracking.ModifiedOn,
                Deleted = vehicleTracking.Deleted,
                DeletedBy = vehicleTracking.DeletedBy,
                DeletedOn = vehicleTracking.DeletedOn,
                DeletedReason = vehicleTracking.DeletedReason
            };

            _dbContext.VehicleTracking.Add(newVehicle);

            await _dbContext.SaveChangesAsync();
        }
    }
}
