using BasalatAssessment.Vehicle.Data.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasalatAssessment.VehicleTracking.Services
{
    public class VehicleTrackingService
    {
        private readonly IDataStore _dataStore;
        public VehicleTrackingService(IDataStore dataStore)
        {
            _dataStore = dataStore;

        }

        public async Task AddVehicleAsync(BasalatAssessment.Domain.Models.VehicleTracking vehicleTracking)
        {
            await _dataStore.SaveVehicleDetailsAsync(vehicleTracking);
        }
    }
}
