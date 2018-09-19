using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewsApi.Integration.Response
{
    public class Source
    {
        [JsonProperty("id")] public object Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}
