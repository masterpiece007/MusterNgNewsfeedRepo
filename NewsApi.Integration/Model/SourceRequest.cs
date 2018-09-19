using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI.Constants;

namespace NewsApi.Integration.Model
{
   public  class SourceRequest
    { 

        public List<string> Sources = new List<string>();
        public Categories? Category { get; set; }
        public Languages? Language { get; set; }
        public Countries? Country { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
