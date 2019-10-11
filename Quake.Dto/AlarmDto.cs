using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class AlarmDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AlarmLevel { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int RegionId { get; set; }
        public virtual RegionDto Region { get; set; }
        public virtual ICollection<UserAlarmDto> UserAlarms { get; set; }

    }
}
