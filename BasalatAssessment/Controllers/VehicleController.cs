using BasalatAssessment.Domain.Interfaces;
using BasalatAssessment.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasalatAssessment.Vehicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleDataStore _vehicleDataStore;

        public VehicleController(IVehicleDataStore vehicleDataStore)
        {
            _vehicleDataStore = vehicleDataStore;
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
    }
}
