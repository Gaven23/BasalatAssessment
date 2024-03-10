using BasalatAssessment.Domain.Models;

namespace BasalatAssessment.Domain.Interfaces
{
    public interface IStockDataStore
    {
        Task<VehicleBodieResult> GetVehicleBodieAsync(int Id, CancellationToken cancellationToken = default);
    }
}
