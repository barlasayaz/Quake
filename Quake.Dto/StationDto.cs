using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class StationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int RegionId { get; set; }
        public virtual RegionDto Region { get; set; }
    }
}
