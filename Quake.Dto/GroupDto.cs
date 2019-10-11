
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int UserId { get; set; }
        public virtual UserDto User { get; set; }
        public virtual ICollection<FriendDto> Friends { get; set; }
        public bool ShareLocation { get; set; }
        public bool ShareStatus { get; set; }
    }
}
