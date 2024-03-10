namespace BasalatAssessment.Stock.Data.Models
{
    public class VehicleBodie
    {
        public int Id { get; set; }
        public int make_model_trim_id { get; set; }
        public string Type { get; set; }
        public int Doors { get; set; }
        public double Length { get; set; }
        public decimal Width { get; set; }
        public object Seats { get; set; }
        public decimal Height { get; set; }
        public double Wheel_Base { get; set; }
        public object Front_Track { get; set; }
        public object Rear_Track { get; set; }
        public object Ground_Clearance { get; set; }
        public object Cargo_Capacity { get; set; }
        //public object MaxCargoCapacity { get; set; }
        //public int CurbWeight { get; set; }
        //public object GrossWeight { get; set; }
        //public object MaxPayload { get; set; }
        //public object MaxTowingCapacity { get; set; }
        //public MakeModelTrim MakeModelTrim { get; set; }
        //public MakeModel MakeModel { get; set; }
    }
}
