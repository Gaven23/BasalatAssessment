namespace BasalatAssessment.Vehicle.Data.Helper
{
    public static class PagedData
    {
        public static async Task<PagedData<T>> ToCustomPagedListAsync<T>(this IEnumerable<T> source, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var items = await Task.Run(() => source.Skip((page - 1) * pageSize).Take(pageSize), cancellationToken);

            return new PagedData<T> { Data = items, Total = source.Count() };
        }
    }

    public class PagedData<T>
    {
        /// <summary>
        /// Use this when there is no data grouping and the response is flat data, like in the common case.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Always set this to the total number of records.
        /// </summary>
        public int Total { get; set; }
    }
}
