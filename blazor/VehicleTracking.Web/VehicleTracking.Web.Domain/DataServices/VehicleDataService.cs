using VehicleTracking.Web.Domain.HttpClients;
using VehicleTracking.Web.Domain.Models;
using VehicleTracking.Web.Domain.Models.Vehicle;

namespace VehicleTracking.Web.Domain.DataServices
{
    public sealed class VehicleDataService : IDisposable
    {
        private readonly VehicleTrackingHttpClient _vehicleTrackingHttpClient;
        private CancellationTokenSource _vehicleTrackingCancelToken;

        public VehicleDataService(VehicleTrackingHttpClient vehicleTrackingHttpClient)
        {
            _vehicleTrackingHttpClient = vehicleTrackingHttpClient;
        }

        public async Task<RequestResponse<PagedData<Vehicle>>> PostVehiclesAsync(Vehicle vehicle)
        {
            _vehicleTrackingCancelToken?.Cancel();
            _vehicleTrackingCancelToken = new();

            var url = $"api/v1/VehicleProfile/PostVehicle";

            return (RequestResponse<PagedData<Vehicle>>)await _vehicleTrackingHttpClient.PostAsJsonAsync(url, vehicle);
        }

        public void Dispose()
        {
            _vehicleTrackingCancelToken?.Cancel();
            _vehicleTrackingCancelToken?.Dispose();
            _vehicleTrackingCancelToken = null!;
        }

    }
}
