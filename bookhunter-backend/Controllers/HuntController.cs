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

        [HttpGet("start")]
        public ActionResult StartHunt()
        {
            if (_huntStatus) BadRequest("Hunt in process");
            _hunterService.HuntBooksList().ContinueWith(t =>
            {
                if (t.IsCompleted) _huntStatus = false;
            });
            return Ok("Hunt started");
        }
        [HttpGet("status")]
        public ActionResult GetHuntStatus()
        {
            return Ok(_huntStatus ? "Hunt in process" : "No hunt is running");
        }
    }
}