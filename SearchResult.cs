using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardWCF2Lib
{
    public class SearchResult
    {
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
        public string panelTitle { get; set; }
        public string panelUrl { get; set; }
        public string uid { get; set; } 

    }
}
