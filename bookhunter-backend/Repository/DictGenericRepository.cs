using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHunter_Backend.Repository
{
    public class DictGenericRepository<T> : GenericRepository<T> where T: BaseDict
    {
        public DictGenericRepository(DbContext context) : base(context)
        {
        }
        public async Task<T?> GetByName(string name)
        {
            name = name.ToLower();
            return await Set
            .Where(b => b.Name.Contains(name))
            .FirstAsync();
        }
    }
}