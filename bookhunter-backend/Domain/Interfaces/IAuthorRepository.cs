using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface IAuthorRepository: IGenericRepository<Author>
    {
        Task<Author?> GetByName(string name);
    }
}