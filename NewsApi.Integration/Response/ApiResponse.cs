using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace NewsApi.Integration.Response
{
    class ApiResponse
    {
        public Statuses Status { get; set; }

        public ErrorCodes? Code { get; set; }

        public string Message { get; set; }

        public List<Article> Articles { get; set; }

        public int TotalResults { get; set; }
    }

    class ApiSourceResponse
    {
        public Statuses Status { get; set; }

        public ErrorCodes? Code { get; set; }

        public string Message { get; set; }

        public List<SourceResponse> Sources { get; set; }

        public int TotalResults { get; set; }
    }
}
