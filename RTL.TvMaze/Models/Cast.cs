using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TvMaze.Models
{
    public class Cast
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonIgnore]
        public int ShowId { get; set; }

        [JsonIgnore]
        public Show Show { get; set; }
    }
}
