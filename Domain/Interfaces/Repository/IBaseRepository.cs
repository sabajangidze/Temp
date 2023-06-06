using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IBaseRepository<TAggregateRoot> where TAggregateRoot : class
    {
        IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>>? expression = default);
        Task<TAggregateRoot?> OfIdAsync(Guid id);
        Task<TAggregateRoot?> OfIdAsync(string id);
        Task InsertAsync(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        void Delete(TAggregateRoot aggregateRoot);
    }
}