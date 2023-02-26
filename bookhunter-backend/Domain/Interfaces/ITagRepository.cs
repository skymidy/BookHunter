using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface ITagRepository: IGenericRepository<Tag>
    {
        Task<Tag?> GetByName(string name);
    }
}