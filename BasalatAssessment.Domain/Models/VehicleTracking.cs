namespace BasalatAssessment.Domain.Models
{
    public class VehicleTracking
    {
        public Guid VehicleId { get; set; }
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
