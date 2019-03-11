using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ASCOMCore
{
    public class ImageArrayResponseBase : RestResponseBase
    {
        private int rank = 0;
        private ResponseTypes.ImageArrayElementTypes type = ResponseTypes.ImageArrayElementTypes.Unknown;

        [JsonProperty(Order = -3)]
        public int Type
        {
            get { return (int)type; }
            set { type = (ResponseTypes.ImageArrayElementTypes)value; }
        }

        [JsonProperty(Order = -2)]
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }

    }
}
