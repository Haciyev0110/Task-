using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.DTO.Requests
{
    public class SearchRequest
    {
        [JsonProperty("word")]
        public string Searchitem { get; set; }

        public override string ToString()
        {
            return $"{nameof(Searchitem)}: {Searchitem}";
        }
    }
}
