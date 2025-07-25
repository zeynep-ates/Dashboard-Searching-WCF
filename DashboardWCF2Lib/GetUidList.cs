using System.Collections.Generic;
using System.Configuration;

namespace DashboardWCF2Lib
{
    public class GetUidList
    {
        public List<string> UidList = new List<string> { ConfigurationManager.AppSettings["Uids"] };
    }
}
