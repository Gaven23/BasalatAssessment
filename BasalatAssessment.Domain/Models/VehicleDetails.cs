namespace BasalatAssessment.Domain.Models
{
    public class VehicleDetails
    {
        public Guid VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleEngineNum { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }
        public string DeletedReason { get; set; }
        public int Id { get; set; }
        public int MakeModelTrimId { get; set; }
        public string Type { get; set; }
        public int Doors { get; set; }
        public double Length { get; set; }
        public decimal Width { get; set; }
        public string Seats { get; set; }
        public decimal Height { get; set; }
        public double WheelBase { get; set; }
        public string FrontTrack { get; set; }
        public string RearTrack { get; set; }
        public string GroundClearance { get; set; }
        public string CargoCapacity { get; set; }
        public string MaxCargoCapacity { get; set; }
        public string CurbWeight { get; set; }
        public string GrossWeight { get; set; }
        public string MaxPayload { get; set; }
        public string MaxTowingCapacity { get; set; }
    }
}
