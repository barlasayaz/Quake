using Quake.Dto;
using Quake.Dto.Enums;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Friends
{
    public interface IFriendsService
    {
        EntityListResponse<UserDto> Get(int userId);
        EntityListResponse<UserDto> GetInvitationsSent(int userId);
        EntityListResponse<UserDto> GetInvitationsReceived(int userId);
        PrimitiveResponse AddFriendsToGroup(int groupId, List<UserDto> model);
        PrimitiveResponse Delete(int groupId, List<UserDto> model);
        PrimitiveResponse InviteToAnswer(UserInvitedDto model);
       
    }
}
