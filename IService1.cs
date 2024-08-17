using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DashboardWCF2Lib
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<SearchResult> SearchID(string telemetryID);
        [OperationContract]
        List<SearchResult> SearchName(string telemetryName);
    }

    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
