using Quake.Dto;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Groups
{
    public interface IGroupsService
    {
        EntityResponse<GroupDto> Get(int id);
        PrimitiveResponse Post(GroupDto model);
        EntityResponse<GroupDto> Put(GroupDto model);
        PrimitiveResponse Delete(int id);
        EntityListResponse<GroupDto> GetGroupsByUserId(int userid);
        EntityListResponse<UserDto> GroupToUser(int groupId);
    }
}
