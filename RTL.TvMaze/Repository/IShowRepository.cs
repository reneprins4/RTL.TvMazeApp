using RTL.TvMaze.Models;
using System.Threading.Tasks;

namespace RTL.TvMaze.Repository
{
    public interface IShowRepository
    {
        Task CreateAsync(Show show);
        Task SaveAsync();
    }
}
