using BasalatAssessment.Vehicle.Data.Tracking;
using BasalatAssessment.Vehicle.Data.Tracking.Entities;

namespace BasalatAssessment.VehicleTracking.Services
{
    public class VehicleTrackingService
    {
        private readonly IDataStore _dataStore;
        public VehicleTrackingService(IDataStore dataStore)
        {
            _dataStore = dataStore;

        }

        public async Task<IEnumerable<VehicleDetails>> GetVehiclesAsync(Guid vehicleId, CancellationToken cancellationToken = default)
        {
            var vehicle = await _dataStore.(vehicleId, cancellationToken);

            return vehicle.Select(v => v.ToVehicleProfile());
        }

        public async Task AddVehicleAsync(VehicleDetails vehicleDetails)
        {
            await _dataStore.SaveVehicleDetailsAsync(vehicleDetails);
        }
    }
}
