using Quake.Dto;
using Quake.Repository.QuakeRepository;
using Quake.Repository.QuakeSqlParameter;
using Quake.Toolkit.Enums;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Alarm
{
    public class AlarmService : IAlarmService
    {
        private readonly IQuakeRepository _baseService;
        public AlarmService(IQuakeRepository baseService)
        {
            _baseService = baseService;
        }

        public EntityResponse<AlarmDto> GetAlarm(int id)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = id });
            var result = _baseService.QueryWithMultiOutput<AlarmDto>("sp_GetSingleAlarm", prms);
            var singleAlarm = result.Item1.ToList().SingleOrDefault();
            return new EntityResponse<AlarmDto>() { ResponseCode = ResponseCodes.Successful, EntityData = singleAlarm };
        }

        public EntityListResponse<AlarmDto> GetAlarms()
        {
            var prms = new Dictionary<string, SqlParam>();
            var result = _baseService.QueryWithMultiOutput<AlarmDto>("sp_GetAllAlarms", prms);
            var alarmList = result.Item1.ToList();
            return new EntityListResponse<AlarmDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = alarmList };
        }
    }
}
