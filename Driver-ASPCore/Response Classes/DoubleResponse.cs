using System;

namespace ASCOMCore
{
    public class DoubleResponse : RestResponseBase
    {
        public DoubleResponse() { }

        public DoubleResponse(int clientTransactionID, int transactionID, string method, double value)
        {
            base.ServerTransactionID = transactionID;
            base.ClientTransactionID = clientTransactionID;
            Value = value;
        }

        public double Value { get; set; }
    }
}
