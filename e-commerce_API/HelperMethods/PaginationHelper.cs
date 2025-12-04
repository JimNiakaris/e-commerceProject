namespace e_commerce_API.HelperMethods
{
    public class PaginationHelper<T>
    {
        // I use this instead of the primary constructor
        public PaginationHelper(int pageIndex,int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> ?Data { get; set; }
    }
}
