using Quake.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class EarthquakeDto
    {
        public int Id { get; set; }
        public int HarmLevel { get; set; }
        public DateTime CreatedOn { get; set; }
        public EarthquakeType EarthquakeType { get; set; }
        public int StationId { get; set; }
        public virtual StationDto Station { get; set; }
    }
}
