using System;

namespace ASCOMCore
{
    public class ShortArray2DResponse : ImageArrayResponseBase
    {
        private short[,] shortArray2D;

        private const int RANK = 2;
        private const ResponseTypes.ImageArrayElementTypes TYPE = ResponseTypes.ImageArrayElementTypes.Short;

        public ShortArray2DResponse(int clientTransactionID, int transactionID, string method, short[,] value)
        {
            base.ServerTransactionID = transactionID;
            shortArray2D = value;
            base.Type = (int)TYPE;
            base.Rank = RANK;
            base.ClientTransactionID = clientTransactionID;
        }

        public short[,] Value
        {
            get { return shortArray2D; }
            set
            {
                shortArray2D = value;
                base.Type = (int)TYPE;
                base.Rank = RANK;
            }
        }
    }
}
