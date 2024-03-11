using BasalatAssessment.Domain.Models;

namespace BasalatAssessment.Stock.Data.Converters
{
    internal static class VehicleStockConverter
    {
        public static VehicleBodie ToDomainVehicleBodie(this BasalatAssessment.Stock.Data.Models.VehicleBodie vehicleBodie)
        {
            if (vehicleBodie == null)
                return null;

            return new VehicleBodie
            {
                Id = vehicleBodie.Id,
                MakeModelTrimId = vehicleBodie.make_model_trim_id,
                Type = vehicleBodie.Type,
                Doors = vehicleBodie.Doors,
                Length = vehicleBodie.Length,
                Width = vehicleBodie.Width,
                Seats = vehicleBodie.Seats,
                Height = vehicleBodie.Height,
                WheelBase = vehicleBodie.Wheel_Base,
                FrontTrack = vehicleBodie.Front_Track,
                RearTrack = vehicleBodie.Rear_Track,
                GroundClearance = vehicleBodie.Ground_Clearance,
                CargoCapacity = vehicleBodie.Cargo_Capacity,
                MaxCargoCapacity = vehicleBodie.Max_Cargo_Capacity,
                CurbWeight = vehicleBodie.Curb_Weight,
                GrossWeight = vehicleBodie.Gross_Weight,
                MaxPayload = vehicleBodie.Max_Payload,
                MaxTowingCapacity = vehicleBodie.Max_Towing_Capacity      
            };
        }
    }
}
