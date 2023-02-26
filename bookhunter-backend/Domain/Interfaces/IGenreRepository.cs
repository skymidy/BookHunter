using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface IGenreRepository: IGenericRepository<Genre>
    {
        Task<Genre?> GetByName(string name);
    }
}