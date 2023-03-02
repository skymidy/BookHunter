using BookHunter_Backend.Domain;
using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHunter_Backend.Repository
{
    public class SiteParserRepository : GenericRepository<SiteParser>, ISiteParserRepository
    {
        public SiteParserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SiteParser>> GetSitesByName(string name)
        {
            return await Set
            .Where(b => b.SiteName.ToLower().Contains(name.ToLower()))
            .ToListAsync();
        }
    }
}