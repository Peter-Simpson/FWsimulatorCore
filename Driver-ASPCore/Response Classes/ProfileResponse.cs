using System.Collections.Generic;

namespace ASCOMCore
{
    public class ProfileResponse : RestResponseBase
    {
        private List<ProfileDevice> list;

        public ProfileResponse() { }

        public ProfileResponse(int clientTransactionID, int transactionID, string method, List<ProfileDevice> value)
        {
            base.ServerTransactionID = transactionID;
            list = value;
            base.ClientTransactionID = clientTransactionID;
        }

        public List<ProfileDevice> Value
        {
            get { return list; }
            set { list = value; }
        }
    }
}