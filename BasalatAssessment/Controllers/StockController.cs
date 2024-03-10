using BasalatAssessment.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasalatAssessment.Vehicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockDataStore _stockDataStore;

        public StockController(IStockDataStore stockDataStore)
        {
            _stockDataStore = stockDataStore;
        }

        /// <summary>
  		/// Retrieves a collection of vehicle bodie
        /// </summary>
        [HttpGet("VehicleBodie")]
        [ProducesResponseType(typeof(Domain.Models.VehicleBodieResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicleMakesAsync(int Id = 1, CancellationToken cancellationToken = default)
        {
            var result = await _stockDataStore.GetVehicleBodieAsync(Id, cancellationToken: cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
