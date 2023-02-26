using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHunter_Backend.Repository
{
    public class GenreRepository : DictGenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {
        }
    }
}