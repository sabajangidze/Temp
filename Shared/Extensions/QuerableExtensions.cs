using Shared.Interfaces;

namespace Shared.Extensions
{
    public static class QuerableExtensions
    {
        public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, IPaginationRequest request)
        {
            if (request == null)
            {
                return source;
            }

            request.PageSize = request.PageSize ?? 10;
            request.Page = request.Page ?? 1;

            return source.Skip(request.PageSize.Value * (request.Page.Value - 1)).Take(request.PageSize.Value);
        }
    }
}