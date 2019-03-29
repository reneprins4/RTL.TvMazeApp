using Microsoft.AspNetCore.Mvc;
using RTL.TvMaze.Services;
using System;
using System.Threading.Tasks;

namespace RTL.TvMaze.Controllers
{
    [Route("api/[controller]")]
    public class ShowController : Controller
    {
        private readonly IShowService showService;

        public ShowController(IShowService showService)
        {
            this.showService = showService;
        }

        // GET: api/show/7
        [HttpGet("{showId}")]
        public async Task<IActionResult> GetShow(int showId)
        {
            try
            {
                var show = await showService.ScrapeFromApiAsync(showId);

                await showService.SaveShowAsync(show);

                return Ok(show);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}