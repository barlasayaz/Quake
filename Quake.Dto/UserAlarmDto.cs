using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class UserAlarmDto : BaseDto
    {
        public int UserId { get; set; }
        public int RegionId { get; set; }
        public int AlarmId { get; set; }
        public virtual AlarmDto Alarm { get; set; }
    }
}
