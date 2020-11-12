using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDisplayApp.WPF.Models
{
    public class Data
    {
        [JsonProperty("videos")]
        public Video[] Videos { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
