namespace BasalatAssessment.Domain.Models
{
    public class VehicleBodie
    {
        public int Id { get; set; }
        public int MakeModelTrimId { get; set; }
        public string Type { get; set; }
        public int Doors { get; set; }
        public double Length { get; set; }
        public decimal Width { get; set; }
        public object Seats { get; set; }
        public decimal Height { get; set; }
        public double WheelBase { get; set; }
        public object FrontTrack { get; set; }
        public object RearTrack { get; set; }
        public object GroundClearance { get; set; }
        public object CargoCapacity { get; set; }
        public object MaxCargoCapacity { get; set; }
        //public int CurbWeight { get; set; }
        //public object GrossWeight { get; set; }
        //public object MaxPayload { get; set; }
        //public object MaxTowingCapacity { get; set; }
        //public MakeModelTrim MakeModelTrim { get; set; }
        //public MakeModel MakeModel { get; set; }
    }
}
