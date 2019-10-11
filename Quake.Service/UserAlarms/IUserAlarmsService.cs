using Quake.Dto;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.UserAlarms
{
    public interface IUserAlarmsService
    {
        EntityListResponse<UserAlarmDto> Get(int userId);
        PrimitiveResponse Post(List<UserAlarmDto> model);
        PrimitiveResponse Delete(int userId);
    }
}
