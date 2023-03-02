using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface ISiteParserRepository : IGenericRepository<SiteParser>
    {
        
        Task<IEnumerable<SiteParser>> GetSitesByName(string name);
    }
}