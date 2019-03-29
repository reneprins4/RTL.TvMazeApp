using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TvMaze.Models
{
    public class Show
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        
        [JsonProperty("id")]
        public int ShowId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        public virtual IEnumerable<Cast> Casts { get; set; }
    }
}
