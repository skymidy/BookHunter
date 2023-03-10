using BookHunter_Backend.Domain;
using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookHunter_Backend.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> Set;

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var row = await Set.FindAsync(id);
            if (row == null) throw new Exception("Error: Invalid row id!");
            return row;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }
        
        public async Task<IEnumerable<T>> GetPaginated(int pageNumber, int pageSize = 10)
        {
            const int maxPageSize = 100;
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 1;
            }
            else if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }
    
            var list = await GetAllAsync();
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public T Add(T entity)
        {
            var entityEntry =  Set.Add(entity);
            Context.SaveChanges();
            return entityEntry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Set.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            Set.Update(entity);
            Context.SaveChanges();
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
            Context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
            Context.SaveChanges();
        }
    }
}