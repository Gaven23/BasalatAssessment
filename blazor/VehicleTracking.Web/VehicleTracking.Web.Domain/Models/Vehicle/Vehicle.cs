using System.ComponentModel.DataAnnotations;

namespace VehicleTracking.Web.Domain.Models.Vehicle
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Vehicle Name is too long.")]
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleEngineNum { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedReason { get; set; }

    }
}
