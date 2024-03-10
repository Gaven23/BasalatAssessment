using Microsoft.AspNetCore.Components;
using VehicleTracking.Web.Domain.DataServices;
using VehicleTracking.Web.Domain.Models.Vehicle;

namespace VehicleTracking.Web.Pages
{
    public partial class AddVehiclePage
    {
        protected bool Saved;

        public Vehicle Vehicle { get; set; } = new Vehicle();


        protected async Task HandleValidSubmit()
        {
        }
        protected async Task HandleInvalidSubmit()
        {
        }

        protected void NavigateToOverview() { }
    }
}