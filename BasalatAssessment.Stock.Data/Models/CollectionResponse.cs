namespace BasalatAssessment.Stock.Data.Models
{
    public class CollectionResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

    }
}
