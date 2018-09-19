using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace NewsApi.Integration.Response
{
    public class SourceResult
    {
        public Statuses Status { get; set; }

        public Error Error { get; set; }

        public int TotalResults { get; set; }

        public List<SourceResponse> SourceResponses { get; set; }
    }
}
