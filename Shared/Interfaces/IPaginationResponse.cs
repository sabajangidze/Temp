namespace Shared.Interfaces
{
    public interface IPaginationResponse
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int Total { get; set; }
    }
}