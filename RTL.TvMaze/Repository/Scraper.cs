using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTL.TvMaze.Models;

namespace RTL.TvMaze.Repository
{
    public class Scraper : IScraper
    {
        public async Task<IEnumerable<Cast>> GetCastAsync(int showId)
        {
            var url = $"http://api.tvmaze.com/shows/{showId}/cast";
            var castResult = await GetJsonAsync(url);
            var json = JArray.Parse(castResult);
            return json
                .Select(p => p["person"])
                .Select(p => p.ToObject<Cast>())
                .OrderByDescending(o => o.Birthday.HasValue) // Some values does not contain dates
                .ThenBy(o => o.Birthday)
                .ToList();
        }

        public async Task<Show> GetShowAsync(int showId)
        {
            var url = $"http://api.tvmaze.com/shows/{showId}?embed[]=cast";
            var showResult = await GetJsonAsync(url);
            var show = JsonConvert.DeserializeObject<Show>(showResult);
            var casts = await GetCastAsync(showId);

            show.Casts = casts;
            return show;
        }

        private async Task<string> GetJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                using (var req = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (var res = await client.SendAsync(req))
                    {
                        var result = await res.Content.ReadAsStringAsync();

                        if (res.StatusCode ==  System.Net.HttpStatusCode.OK)
                        {
                            return result;
                        }

                        return null;
                    }
                }
            }
        }
    }
}
