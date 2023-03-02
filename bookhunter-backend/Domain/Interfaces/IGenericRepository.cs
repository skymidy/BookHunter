using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPaginated(int pageNumber, int pageSize = 10);
        Task<T> GetByIdAsync(int id);
        T Add(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}