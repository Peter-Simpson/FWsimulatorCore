using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASCOMCore
{
    public abstract class RestResponseBase
    {
        public int ClientTransactionID { get; set; }
        public int ServerTransactionID { get; set; }
        public int ErrorNumber { get; set; } = 0;
        public string ErrorMessage { get; set; } = "";
    }
}
