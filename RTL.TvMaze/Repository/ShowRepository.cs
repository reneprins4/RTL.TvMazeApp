using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTL.TvMaze.DbContexts;
using RTL.TvMaze.Models;

namespace RTL.TvMaze.Repository
{
    public class ShowRepository : IShowRepository
    {
        private readonly DatabaseContext context;

        public ShowRepository(DatabaseContext context)
        {
            this.context = context;
        }
        
        public async Task CreateAsync(Show show)
        {
            if (show == null)
            {
                throw new ArgumentNullException($"{nameof(show)} is not defined");
            }

            var exists = await context.Shows.AnyAsync(s => s.ShowId == show.ShowId);
            if (!exists)
            {
                await context.Shows.AddAsync(show);
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
