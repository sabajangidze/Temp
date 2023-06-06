using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TAggregateRoot> : IBaseRepository<TAggregateRoot>
            where TAggregateRoot : class
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual void Delete(TAggregateRoot aggregateRoot)
        {
            _context.Set<TAggregateRoot>().Remove(aggregateRoot);
        }

        public async Task<TAggregateRoot> OfIdAsync(Guid id)
        {
            return await _context.Set<TAggregateRoot>().FindAsync(id);
        }

        public async Task<TAggregateRoot> OfIdAsync(string id)
        {
            return await _context.Set<TAggregateRoot>().FindAsync(id);
        }

        public IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> expression = default)
        {
            return expression == null ? _context.Set<TAggregateRoot>().AsQueryable() : _context.Set<TAggregateRoot>().Where(expression);
        }

        public virtual async Task InsertAsync(TAggregateRoot aggregateRoot)
        {
            await _context.Set<TAggregateRoot>().AddAsync(aggregateRoot);
        }

        public virtual void Update(TAggregateRoot aggregateRoot)
        {
            _context.Entry(aggregateRoot).State = EntityState.Modified;
        }
    }
}