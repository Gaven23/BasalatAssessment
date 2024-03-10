namespace BasalatAssessment.Common
{
    public class AppSettings
    {
        public VehicleSettings VehicleSettings { get; set; }
        public IEnumerable<string>? AllowedOrigins { get; set; }
        //public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class VehicleSettings
    {
        public string? VehicleApiUrl { get; set; }
        public string? VehicleMakeApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string Host { get; set; }
    }
}
