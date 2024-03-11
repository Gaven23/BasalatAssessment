using BasalatAssessment.Domain.Interfaces;
using BasalatAssessment.Domain.Models;
using BasalatAssessment.Vehicle.Data.Tracking;
using Microsoft.AspNetCore.Mvc;

namespace BasalatAssessment.Vehicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleDataStore _vehicleDataStore;

        private readonly IDataStore _dataStore;
        public VehicleController(IVehicleDataStore vehicleDataStore, IDataStore dataStore)
        {
            _vehicleDataStore = vehicleDataStore;
            _dataStore = dataStore; 
        }

        /// <summary>
  		/// Retrieves a collection of vehicle makes
        /// </summary>
        [HttpGet("VehicleMakes")]
        [ProducesResponseType(typeof(Domain.Models.VehicleMakeResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicleMakesAsync(int page = 1, int pageSize = 15, CancellationToken cancellationToken = default)
        {
            var result = await _vehicleDataStore.GetVehicleMakesAsync(page, pageSize, cancellationToken: cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        /// <summary>
        /// Retrieves a collection of vehicle Datails from database that are stored from the API
        /// </summary>
        [HttpGet("VehicleDatails")]
        [ProducesResponseType(typeof(VehicleDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicleDetailsAsync(int page = 1, int pageSize = 15, CancellationToken cancellationToken = default)
        {
            var result = await _dataStore.GetVehiclesDetailsAsync(cancellationToken: cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
