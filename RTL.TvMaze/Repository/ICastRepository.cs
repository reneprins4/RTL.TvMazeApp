using RTL.TvMaze.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RTL.TvMaze.Repository
{
    public interface ICastRepository
    {
        Task CreateCastAsync(Cast cast);
        Task SaveAsync();
    }
}
