using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTL.TvMaze.DbContexts;
using RTL.TvMaze.Models;

namespace RTL.TvMaze.Repository
{
    public class CastRepository : ICastRepository
    {
        private readonly DatabaseContext context;

        public CastRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task CreateCastAsync(Cast cast)
        {
            if (cast == null)
            {
                throw new ArgumentNullException($"{nameof(cast)} is not defined");
            }

            var exists = context.Casts.Any(c => c.Id == cast.Id);
            if (!exists)
            {
                await context.Casts.AddAsync(cast);
            }
        }

        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
