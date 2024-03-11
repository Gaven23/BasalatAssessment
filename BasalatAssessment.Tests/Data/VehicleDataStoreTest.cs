using BasalatAssessment.Vehicle.Data.Tracking;
using BasalatAssessment.Vehicle.Data.Tracking.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BasalatAssessment.Tests.Data
{
    public class VehicleDataStoreTest
    {
        private readonly DataStore _dataStore;

        public VehicleDataStoreTest()
        {
            _dataStore = TestHelpers.GetDataStore();
        }

        [Fact]
        public async Task GetVehiclesDetailsAsyncTest()
        {
            var result = await _dataStore.GetVehiclesDetailsAsync();

            Assert.NotEmpty(result);
        }
    }
}
