using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface IGenreRepository: IGenericRepository<Genre>
    {
        Genre? GetByName(string name);
    }
}