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
    public class EntityListResponse<T> : BaseResponse
    {
        private List<T> _entityDataList = new List<T>();

        [DataMember]
        public List<T> EntityDataList
        {
            get { return _entityDataList; }
            set { _entityDataList = value; }
        }
    }
}
