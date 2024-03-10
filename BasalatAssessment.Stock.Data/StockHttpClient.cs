namespace BasalatAssessment.Stock.Data
{
    public class StockHttpClient
    {
        public HttpClient HttpClient { get; }

        public StockHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
