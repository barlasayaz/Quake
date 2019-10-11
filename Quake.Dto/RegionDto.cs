using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class RegionDto : BaseDto
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<StationDto> Stations { get; set; }
        public virtual ICollection<AlarmDto> Alarms { get; set; }
    }
}
