using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTL.TvMaze.Models;
using RTL.TvMaze.Repository;

namespace RTL.TvMaze.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository showRepository;
        private readonly ICastRepository castRepository;
        private readonly IScraper scraper;

        public ShowService(IShowRepository showRepository, ICastRepository castRepository, IScraper scraper)
        {
            this.showRepository = showRepository;
            this.castRepository = castRepository;
            this.scraper = scraper;
        }

        public async Task SaveShowAsync(Show show)
        {
            if (show == null)
            {
                throw new ArgumentNullException($"{nameof(show)} is null");
            }

            // save show
            await showRepository.CreateAsync(show);
            await showRepository.SaveAsync();

            // save cast
            await SaveCastAsync(show.Casts);
        }

        public async Task<Show> ScrapeFromApiAsync(int showId)
        {
            return await scraper.GetShowAsync(showId);
        }

        private async Task SaveCastAsync(IEnumerable<Cast> casts)
        {
            if (casts == null)
            {
                throw new ArgumentNullException($"{nameof(casts)} is null");
            }

            foreach (var cast in casts)
            {
                await castRepository.CreateCastAsync(cast);
            }

            await castRepository.SaveAsync();
        }
    }
}
