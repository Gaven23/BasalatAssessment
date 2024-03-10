using BasalatAssessment.Common;
using BasalatAssessment.Domain.Interfaces;
using BasalatAssessment.Domain.Models;
using BasalatAssessment.Stock.Data.Converters;
using BasalatAssessment.Stock.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace BasalatAssessment.Stock.Data
{
    public class StockDataStore : IStockDataStore
    {
        private readonly StockHttpClient _stockHttpClient;
        private readonly AppSettings _appSettings;
        private readonly ILogger<StockDataStore> _logger;

        public StockDataStore(StockHttpClient stockHttpClient, IOptionsSnapshot<AppSettings> appSettings, ILogger<StockDataStore> logger)
        {
            _stockHttpClient = stockHttpClient;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task<BasalatAssessment.Domain.Models.VehicleBodieResult> GetVehicleBodieAsync(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var uri = $"/api/bodies";

                _stockHttpClient.HttpClient.DefaultRequestHeaders.Clear();

                _stockHttpClient.HttpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _appSettings.VehicleSettings.ApiKey);
                _stockHttpClient.HttpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _appSettings.VehicleSettings.Host);

                var response = await _stockHttpClient.HttpClient.GetFromJsonAsync<CollectionResponse<BasalatAssessment.Stock.Data.Models.VehicleBodie>>(uri, cancellationToken);

                if (response != null && response.Data != null)
                {
                    return new VehicleBodieResult
                    {
                        VehicleBodie = response.Data.Select(e => e.ToDomainVehicleBodie())
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP request failed: {ex.Message}");
            }
            catch (OperationCanceledException)
            {
                _logger.LogError("The operation was canceled.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }

            return null;
        }
    }
}
