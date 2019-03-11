using System;

namespace ASCOMCore
{
    public class DateTimeResponse : RestResponseBase
    {
        public DateTimeResponse() { }

        public DateTimeResponse(int clientTransactionID, int transactionID, string method, DateTime value)
        {
            base.ServerTransactionID = transactionID;
            base.ClientTransactionID = clientTransactionID;
            Value = value;
        }

        public DateTime Value { get; set; }
    }
}
