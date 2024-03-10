namespace BasalatAssessment.Vehicle.Data
{
    public class VehicleHttpClient
    {
        public HttpClient HttpClient { get; }

        public VehicleHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
