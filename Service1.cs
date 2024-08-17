using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DashboardWCF2Lib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        SearchingClass searchingClass = new SearchingClass();
        
        public List<SearchResult> SearchID(string telemetryID)
        {
            return searchingClass.SearchingID(telemetryID);
        }

        public List<SearchResult> SearchName(string telemetryName)
        {
            return searchingClass.SearchingName(telemetryName);
        }

    }
}
