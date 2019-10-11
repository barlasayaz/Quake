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
    public class PrimitiveResponse : BaseResponse
    {
        [DataMember]
        public string PrimitiveResponseValue { get; set; }

        [DataMember]
        public string EntityPrimaryKey { get; set; }
    }
}
