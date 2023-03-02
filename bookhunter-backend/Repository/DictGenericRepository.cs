using BookHunter_Backend.Domain;
using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHunter_Backend.Repository
{
    public class DictGenericRepository<T> : GenericRepository<T> where T: BaseDict
    {
        public DictGenericRepository(ApplicationDbContext context) : base(context)
        {
        }
        public T? GetByName(string name)
        {
            name = name.ToLower();
            var result = Set.Where(b => b.Name.Contains(name));
            return result.Count() != 0 ? result.First() : null;
        }
    }
}