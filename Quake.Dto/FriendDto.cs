using Quake.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class FriendDto : BaseDto
    {
        public int GroupId { get; set; }
        public virtual GroupDto Group { get; set; }
        public int? UserId { get; set; }
        public string FriendPhone { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
        public bool ShareLocation { get; set; }
        public bool ShareStatus { get; set; }
        public string LastLocation { get; set; }
        public DateTime LastLocationTime { get; set; }
    }
}
