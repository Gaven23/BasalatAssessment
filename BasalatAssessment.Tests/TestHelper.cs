using BasalatAssessment.Vehicle.Data.Tracking.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasalatAssessment.Tests
{
    internal static class TestHelpers
    {
        internal static DataStore GetDataStore()
        {
            return DataStoreHelper.GetDataStore();
        }
    }
}
