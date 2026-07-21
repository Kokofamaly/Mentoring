namespace NorthwindWebApi.Models
{
    public class ProductQueryParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? CategoryIdFilter { get; set; } = null;
    }
}
