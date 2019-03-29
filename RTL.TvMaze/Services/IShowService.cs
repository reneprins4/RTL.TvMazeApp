using RTL.TvMaze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTL.TvMaze.Services
{
    public interface IShowService
    {
        Task<Show> ScrapeFromApiAsync(int showId);
        Task SaveShowAsync(Show show);
    }
}
