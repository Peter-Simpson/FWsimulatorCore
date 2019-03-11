using System;

namespace ASCOMCore
{
    public class IntArray2DResponse : ImageArrayResponseBase
    {
        private int[,] intArray2D;

        private const int RANK = 2;
        private const ResponseTypes.ImageArrayElementTypes TYPE = ResponseTypes.ImageArrayElementTypes.Int;

        public IntArray2DResponse(int clientTransactionID, int transactionID, string method)
        {
            base.ServerTransactionID = transactionID;
            base.ClientTransactionID = clientTransactionID;
        }

        public int[,] Value
        {
            get { return intArray2D; }
            set
            {
                intArray2D = value;
                base.Type = (int)TYPE;
                base.Rank = RANK;
            }
        }
    }
}
