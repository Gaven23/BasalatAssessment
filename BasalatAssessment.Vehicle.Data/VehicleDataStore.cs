using BasalatAssessment.Common;
using BasalatAssessment.Domain.Interfaces;
using BasalatAssessment.Vehicle.Data.Converters;
using BasalatAssessment.Vehicle.Data.Helper;
using BasalatAssessment.Vehicle.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace BasalatAssessment.Vehicle.Data
{
    public class VehicleDataStore : IVehicleDataStore
    {
        private readonly VehicleHttpClient _vehicleHttpClient;
        private readonly AppSettings _appSettings;
        private readonly ILogger<VehicleDataStore> _logger;

        public VehicleDataStore(VehicleHttpClient vehicleHttpClient, IOptionsSnapshot<AppSettings> appSettings, ILogger<VehicleDataStore> logger)
        {
            _vehicleHttpClient = vehicleHttpClient;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        /// <summary>
        /// Return the result
        /// Clear headers before adding to avoid duplicates in subsequent calls
        /// Add required headers
        /// Make the HTTP request
        /// Convert response data to custom paged list
        /// Return the result
        /// </summary>
        public async Task<Domain.Models.VehicleMakeResult> GetVehicleMakesAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var uri = "/api/makes";

                _vehicleHttpClient.HttpClient.DefaultRequestHeaders.Clear();

                _vehicleHttpClient.HttpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _appSettings.VehicleSettings.ApiKey);
                _vehicleHttpClient.HttpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _appSettings.VehicleSettings.Host);

                var response = await _vehicleHttpClient.HttpClient.GetFromJsonAsync<CollectionResponse<VehicleMake>>(uri, cancellationToken);

                if (response != null && response.Data != null)
                {
                    var pagedData = await PagedData.ToCustomPagedListAsync(response.Data, page, pageSize, cancellationToken);

                    return new Domain.Models.VehicleMakeResult
                    {
                        VehicleMake = pagedData.Data.Select(e => e.ToDomainVehicleMake()).OrderBy(e => e.Id),
                        TotalCount = pagedData.Total
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

        public async Task<IEnumerable<Domain.Models.Model>> GetVehicleModelsAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                _vehicleHttpClient.HttpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _appSettings.VehicleSettings.ApiKey);
                _vehicleHttpClient.HttpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _appSettings.VehicleSettings.Host);

                CollectionResponse<Model> response = await _vehicleHttpClient.HttpClient.GetFromJsonAsync<CollectionResponse<Model>>(_appSettings.VehicleSettings.VehicleApiUrl) ?? new CollectionResponse<Model>();

                if (response != null && response.Data != null)
                {
                    var pagedData = await PagedData.ToCustomPagedListAsync(response.Data, page, pageSize, cancellationToken);

                    return pagedData.Data.Select(e => e.ToDomainVehicleModel());

                }
                else
                    return Enumerable.Empty<Domain.Models.Model>();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
    }
}


