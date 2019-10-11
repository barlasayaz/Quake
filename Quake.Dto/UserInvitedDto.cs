using Quake.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class UserInvitedDto : BaseDto
    {
        public int UserId { get; set; }
        public int FriendUserId { get; set; }
        public int GroupId { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
        public DateTime AnswerDate { get; set; }
    }
}
