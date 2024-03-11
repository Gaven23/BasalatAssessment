using BasalatAssessment.Domain.Interfaces;
using BasalatAssessment.Domain.Models;
using BasalatAssessment.Vehicle.Data.Tracking;

namespace BasalatAssessment.Vehicle.Data.Hangfire
{
    public class HangFireService
    {
        private readonly IVehicleDataStore _vehicleDataStore;
        private readonly IStockDataStore _stockDataStore;
        private readonly IDataStore _dataStore;
        private int _randomNumber;
        private Random _randomGenerator;
        public HangFireService(IVehicleDataStore vehicleDataStore, IStockDataStore stockDataStore, IDataStore dataStore)
        {
            _vehicleDataStore = vehicleDataStore;
            _stockDataStore = stockDataStore;
            _dataStore = dataStore;
        }

        public async Task SaveVehicleDataAsync()
        {
            int page = 1;

            int pageSize = 10;

            var data = await _vehicleDataStore.GetVehicleMakesAsync(page, pageSize);

            var stockResult = await _stockDataStore.GetVehicleBodieAsync(data.VehicleMake.First().Id);

            BasalatAssessment.Vehicle.Data.Tracking.Entities.VehicleDetails vehicleDetails = await ToVehicleTracingDetailsConverter(stockResult, data.VehicleMake.First().Name);

            if (vehicleDetails != null)
            {
                await _dataStore.SaveVehicleDetailsAsync(vehicleDetails);
            }

        }

        private async Task<BasalatAssessment.Vehicle.Data.Tracking.Entities.VehicleDetails> ToVehicleTracingDetailsConverter(VehicleBodieResult vehicleBodieResult, string VehicleName)
        {
            _randomGenerator = new Random();
            _randomNumber = _randomGenerator.Next(100);

            var data = vehicleBodieResult.VehicleBodie.FirstOrDefault();

            if (data is null)
            {
                return null;
            }

            return new BasalatAssessment.Vehicle.Data.Tracking.Entities.VehicleDetails
            {
                VehicleName = VehicleName,
                VehicleNumber = _randomNumber.ToString(),
                VehicleMake = "test",
                VehicleModel = "test",
                VehicleEngineNum = _randomNumber.ToString(),
                Comment = "Test",
                Id = data.Id,
                MakeModelTrimId = data.MakeModelTrimId,
                Type = data.Type,
                Doors = data.Doors,
                Length = data.Length,
                Width = data.Width,
                Seats = data.Seats.ToString(),
                Height = data.Height,
                WheelBase = data.WheelBase,
                FrontTrack = data.FrontTrack.ToString(),
                RearTrack = data.RearTrack.ToString(),
                GroundClearance = data.GroundClearance.ToString(),
                CargoCapacity = data.CargoCapacity.ToString(),
                MaxCargoCapacity = _randomNumber.ToString(),
                CurbWeight = _randomNumber.ToString(),
                MaxTowingCapacity = _randomNumber.ToString(),
                GrossWeight = _randomNumber.ToString(),
                MaxPayload = _randomNumber.ToString(),
                CreatedOn = DateTime.Now,
            };
        }
    }
}
