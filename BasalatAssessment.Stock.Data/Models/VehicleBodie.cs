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
        public object Max_Cargo_Capacity { get; set; }
        public object Curb_Weight { get; set; }
        public object Gross_Weight { get; set; }
        public object Max_Payload { get; set; }
        public object Max_Towing_Capacity { get; set; }
    }
}
