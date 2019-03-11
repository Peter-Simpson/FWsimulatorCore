using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ASCOMCore
{
    public class ConfigurationResponse : RestResponseBase
    {
        private ConcurrentDictionary<string, ConfiguredDevice> list;

        public ConfigurationResponse() { }

        public ConfigurationResponse(int clientTransactionID, int transactionID, string method, ConcurrentDictionary<string, ConfiguredDevice> value)
        {
            base.ServerTransactionID = transactionID;
            list = value;
            base.ClientTransactionID = clientTransactionID;
        }

        public ConcurrentDictionary<string, ConfiguredDevice> Value
        {
            get { return list; }
            set { list = value; }
        }
    }
}