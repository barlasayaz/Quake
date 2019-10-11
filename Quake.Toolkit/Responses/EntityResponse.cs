using System;
using System.Runtime.Serialization;

namespace Quake.Toolkit.Responses
{
    [Serializable]
    [DataContract]
    public class EntityResponse<T> : BaseResponse
    {
        [DataMember]
        public T EntityData { get; set; }
    }
}
