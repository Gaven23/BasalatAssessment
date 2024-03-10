using BasalatAssessment.Vehicle.Data.Tracking.Entities;

namespace BasalatAssessment.Vehicle.Data.Tracking
{
    public interface IDataStore
    {
        Task SaveVehicleDetailsAsync(BasalatAssessment.Domain.Models.VehicleTracking vehicleTrackin);
    }
}
