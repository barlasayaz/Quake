using Quake.Dto;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Alarm
{
    public interface IAlarmService
    {
        EntityListResponse<AlarmDto> GetAlarms();
        EntityResponse<AlarmDto> GetAlarm(int id);
    }
}
