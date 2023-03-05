using BookHunter_Backend.Domain.Models;
using BookHunter_Backend.Objects;
using BookHunter_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHunter_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HuntController : ControllerBase
    {
        private readonly HunterService _hunterService;
        private static bool _huntStatus = false;

        public HuntController(HunterService hunterService)
        {
            // if(_huntStatus) throw new Exception("Some hunt already in process");
            // _huntStatus = true;
            _hunterService = hunterService;
        }

        [HttpGet("start/list")]
        public async Task<ActionResult> HuntList()
        {
            await _hunterService.HuntBooksList();
            return Ok("Hunt started");
        }

        [HttpGet("start")]
        public async Task<ActionResult> HuntDetails(int siteId = -1)
        {
            if (siteId <= -1)
                await _hunterService.HuntBookDetails();
            else
                await _hunterService.HuntBookDetailsFromSite(siteId);
            return Ok("Hunt started");
        }

        [HttpPost("site/add")]
        public async Task<ActionResult> AddSite(SiteParserInput site)
        {
            _hunterService.AddNewHuntingSite(site);
            return Ok("Success");
        }
        
        [HttpGet("site")]
        public async Task<ActionResult<IEnumerable<SiteParserInfo>>> GetSitesByName(string name)
        {
            var sites = await _hunterService.GetSitesByName(name);
            return Ok(sites);
        }
        [HttpGet("site/all")]
        public async Task<ActionResult<IEnumerable<SiteParserInfo>>> GetSites()
        {
            var sites = await _hunterService.GetAllHuntingSitesAsync();
            return Ok(sites);
        }

        [HttpGet("site/{siteId:int}")]
        public async Task<ActionResult<SiteParserInfo>> GetSite(int siteId)
        {
            return Ok(await _hunterService.GetHuntingSite(siteId));
        }
        [HttpGet("status")]
        public async Task<ActionResult> GetHuntStatus()
        {
            await _hunterService.HuntBooksList();
            return Ok();
        }
    }
}