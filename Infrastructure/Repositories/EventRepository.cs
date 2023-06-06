using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}