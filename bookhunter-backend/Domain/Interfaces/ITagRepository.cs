using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface ITagRepository: IGenericRepository<Tag>
    {
        Tag? GetByName(string name);
    }
}