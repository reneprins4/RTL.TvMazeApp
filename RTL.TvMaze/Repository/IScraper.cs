using RTL.TvMaze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTL.TvMaze.Repository
{
    public interface IScraper
    {
        Task<Show> GetShowAsync(int showId);
        Task<IEnumerable<Cast>> GetCastAsync(int showId);
    }
}
