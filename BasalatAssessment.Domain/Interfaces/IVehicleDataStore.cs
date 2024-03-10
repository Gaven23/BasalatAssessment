using BasalatAssessment.Domain.Models;

namespace BasalatAssessment.Domain.Interfaces
{
    public interface IVehicleDataStore
    {
        Task<VehicleMakeResult> GetVehicleMakesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<IEnumerable<Model>> GetVehicleModelsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
