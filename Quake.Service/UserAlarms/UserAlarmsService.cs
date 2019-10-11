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

namespace Quake.Service.UserAlarms
{
    public class UserAlarmsService : IUserAlarmsService
    {
        private readonly IQuakeRepository _baseService;
        public UserAlarmsService(IQuakeRepository baseService)
        {
            _baseService = baseService;
        }

        public EntityListResponse<UserAlarmDto> Get(int userId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = userId });
            var result = _baseService.QueryWithMultiOutput<UserAlarmDto>("sp_GetUserAlarms", prms);
            var userAlarms = result.Item1.ToList();
            return new EntityListResponse<UserAlarmDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = userAlarms };
        }

        public PrimitiveResponse Post(List<UserAlarmDto> model)
        {
            PrimitiveResponse primitiveResult;
            var idGroups = model.GroupBy(x => x.UserId).Select(group => group.Key);
            if (idGroups.Count() != 1)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.UpdateNotCompleted, ResponseErrorMessage = "Tüm UserId'ler aynı olmalı!" };
            }
            else
            {
                foreach (var item in model)
                {
                    var prms = new Dictionary<string, SqlParam>();
                    prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = item.UserId });
                    prms.Add("@RegionId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = item.RegionId });
                    prms.Add("@AlarmId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = item.AlarmId });
                    var result = _baseService.ExecuteNonQuery("sp_PostUserAlarms", prms);
                }
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Insert successfully." };
            }
            return primitiveResult;
        }

        public PrimitiveResponse Delete(int userId)
        {
            PrimitiveResponse primitiveResult;
            this.GetUser(userId);
            if (this.GetUser(userId) == 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.UpdateNotCompleted, ResponseErrorMessage = userId + " Id'li bir kullanıcı yok!" };
            }
            else
            {
                var prms = new Dictionary<string, SqlParam>();
                prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
                var result = _baseService.ExecuteNonQuery("sp_DeleteUserAlarms", prms);
                if (result > 0)
                {
                    primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Delete successfully." };
                }
                else
                {
                    primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.UpdateNotCompleted, ResponseErrorMessage = "Canceled could not be completed." };
                }
            }
            return primitiveResult;
        }

        private int GetUser(int userid)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = userid });
            var result = _baseService.QueryWithMultiOutput<UserAlarmDto>("sp_GetUserById", prms);
            return result.Item1.ToList().Count;
        }
    }
}
