using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDisplayApp.WPF.Models
{
    public class Pagination
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("perPage")]
        public long PerPage { get; set; }

        [JsonProperty("currentPage")]
        public long CurrentPage { get; set; }

        [JsonProperty("totalPages")]
        public long TotalPages { get; set; }
    }
}
