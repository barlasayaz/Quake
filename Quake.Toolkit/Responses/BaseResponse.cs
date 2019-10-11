using Quake.Toolkit.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Toolkit.Responses
{
    [Serializable]
    [DataContract]
    public abstract class BaseResponse
    {
        [DataMember]
        public ResponseCodes ResponseCode { get; set; }

        [DataMember]
        public string ResponseMessage { get; set; }

        [DataMember]
        public string ResponseErrorMessage { get; set; }


    }
}
