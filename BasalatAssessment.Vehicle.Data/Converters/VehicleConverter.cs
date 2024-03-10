using BasalatAssessment.Domain.Models;

namespace BasalatAssessment.Vehicle.Data.Converters
{
    internal static class VehicleConverter
    {
        public static Model ToDomainVehicleModel(this BasalatAssessment.Vehicle.Data.Models.Model vehicleModel)
        {
            if (vehicleModel == null)
                return null;

            return new Model
            {
                 Id = vehicleModel.Id,
                 MakeId = vehicleModel.Make_id,
                 Name = vehicleModel.Name,
            };
        }

        public static VehicleMake ToDomainVehicleMake(this BasalatAssessment.Vehicle.Data.Models.VehicleMake vehicleMake)
        {
            if (vehicleMake == null)
                return null;

            return new VehicleMake
            {
                Id = vehicleMake.Id,
                Name = vehicleMake.Name,    
            };
        }
    }
}
