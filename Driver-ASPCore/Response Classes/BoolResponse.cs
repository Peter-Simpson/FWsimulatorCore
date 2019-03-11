using System;

namespace ASCOMCore
{
    public class BoolResponse : RestResponseBase
    {
        public BoolResponse() { }
        public BoolResponse(int clientTransactionID, int transactionID, string method, bool value)
        {
            base.ServerTransactionID = transactionID;
            base.ClientTransactionID = clientTransactionID;
            Value = value;
        }

        public bool Value { get; set; }
    }
}
