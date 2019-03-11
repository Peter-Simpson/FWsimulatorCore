using System;

namespace ASCOMCore
{
    public class DoubleArray3DResponse : ImageArrayResponseBase
    {
        private double[,,] doubleArray3D;

        private const int RANK = 3;
        private const ResponseTypes.ImageArrayElementTypes TYPE = ResponseTypes.ImageArrayElementTypes.Double;

        public DoubleArray3DResponse(int clientTransactionID, int transactionID, string method, double[,,] value)
        {
            base.ServerTransactionID = transactionID;
            doubleArray3D = value;
            base.Type = (int)TYPE;
            base.Rank = RANK;
            base.ClientTransactionID = clientTransactionID;
        }

        public double[,,] Value
        {
            get { return doubleArray3D; }
            set
            {
                doubleArray3D = value;
                base.Type = (int)TYPE;
                base.Rank = RANK;
            }
        }
    }
}
