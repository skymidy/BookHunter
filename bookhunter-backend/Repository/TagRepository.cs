using BookHunter_Backend.Domain;
using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Repository
{
    public class TagRepository : DictGenericRepository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}