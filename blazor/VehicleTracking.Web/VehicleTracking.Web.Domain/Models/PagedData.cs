namespace VehicleTracking.Web.Domain.Models
{
    public class PagedData<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int Total { get; set; }
        public List<AggregateResults> AggregateResults { get; set; }
    }

    public class AggregateResults
    {
        public decimal Value { get; set; }
        public string Member { get; set; }
        public decimal FormattedValue { get; set; }
        public int ItemCount { get; set; }
        public string Caption { get; set; }
        public string FunctionName { get; set; }
        public string AggregateMethodName { get; set; }
    }
}
