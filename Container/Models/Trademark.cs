using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Models
{
    public class Trademark
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("logo_url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("classes")]
        public int Classes { get; set; }

        [JsonProperty("status1")]
        public string Status1 { get; set; }

        [JsonProperty("status2")]
        public string Status2 { get; set; }

        [JsonProperty("details_page_url")]
        public string Details { get; set; }

        public override string ToString()
        {
            return $"{nameof(Number)}: {Number}, {nameof(Url)}: {Url}, {nameof(Name)}: {Name}, {nameof(Classes)}:{Classes}, {nameof(Status1)}:{Status1},{nameof(Status2)}:{Status2},{nameof(Details)}:{Details}";
        }

    }
}
