namespace Domain.Interfaces.DataAccess
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}