using BasalatAssessment.Vehicle.Data.Tracking.Entities;

namespace BasalatAssessment.Vehicle.Data.Tracking
{
    public interface IDataStore
    {
        Task<IEnumerable<VehicleDetails>> GetVehiclesDetailsAsync(CancellationToken cancellationToken = default);
        Task SaveVehicleDetailsAsync(VehicleDetails vehicleDetails);
    }
}
