using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quake.Dto;
using Quake.Repository.QuakeRepository;
using Quake.Repository.QuakeSqlParameter;
using Quake.Service.Groups;
using Quake.Service.UserAlarms;
using Quake.Toolkit.Enums;
using Quake.Toolkit.Responses;
using Quake.Service.ExternalServices;

namespace Quake.Service.Users
{
    public class UserService : IUserService
    {
        private readonly GroupsService _grupService;
        private readonly UserAlarmsService _userAlarmsService;

        private readonly IQuakeRepository _baseService;

        public UserService(IQuakeRepository baseService)
        {
            _baseService = baseService;
            _grupService = new GroupsService(baseService);
            _userAlarmsService = new UserAlarmsService(baseService);

        }

        public PrimitiveResponse Post(UserDto model)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@PushNotificationId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.PushNotificationId });
            prms.Add("@PhoneNumber", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.PhoneNumber });
            prms.Add("@FirstName", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.FirstName });
            prms.Add("@LastName", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.LastName });
            prms.Add("@IsPhoneNumberVerified", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Bit, Value = model.IsPhoneNumberVerified });
            prms.Add("@IsPremium", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Bit, Value = model.IsPremium });
            if (model.UserInfo != null)
            {
                prms.Add("@DeviceOS", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.UserInfo.DeviceOS });
                prms.Add("@DeviceModel", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.UserInfo.DeviceModel });
                prms.Add("@DeviceOSVersion", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.UserInfo.DeviceOSVersion });
            }
            var result = _baseService.ExecuteNonQuery("sp_PostUser", prms);
            PrimitiveResponse primitiveResult;
            if (result > 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Insert successfully." };
            }
            else
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.UpdateNotCompleted, ResponseErrorMessage = "Canceled could not be completed." };
            }
            return primitiveResult;
        }

        public PrimitiveResponse Delete(string pushNotificationId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@PushNotificationId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = pushNotificationId });
            var result = _baseService.ExecuteNonQuery("sp_DeleteUser", prms);
            PrimitiveResponse primitiveResult;
            if (result > 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Delete successfully." };
            }
            else
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.UpdateNotCompleted, ResponseErrorMessage = "Canceled could not be completed." };
            }
            return primitiveResult;
        }

        public EntityResponse<UserDto> Get(string PushNotificationId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@PushNotificationId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = PushNotificationId });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GetUser", prms);
            var singleUser = result.Item1.ToList().SingleOrDefault();
            singleUser.Groups = _grupService.GetGroupsByUserId(singleUser.Id).EntityDataList.ToList();
            singleUser.UserAlarmsIds = _userAlarmsService.Get(singleUser.Id).EntityDataList.ToList();
            return new EntityResponse<UserDto>() { ResponseCode = ResponseCodes.Successful, EntityData = singleUser };
        }

        public PrimitiveResponse Patch(UserDto model)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = model.Id });
            prms.Add("@PushNotificationId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.PushNotificationId });
            prms.Add("@PhoneNumber", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.PhoneNumber });
            prms.Add("@FirstName", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.FirstName });
            prms.Add("@LastName", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.LastName });
            prms.Add("@IsPhoneNumberVerified", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Bit, Value = model.IsPhoneNumberVerified });
            prms.Add("@IsPremium", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Bit, Value = model.IsPremium });
            prms.Add("@DoesUserBuyIt", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Bit, Value = model.DoesUserBuyIt });

            prms.Add("@DeviceOS", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.UserInfo != null ? model.UserInfo.DeviceOS : null });
            prms.Add("@DeviceModel", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.UserInfo != null ? model.UserInfo.DeviceModel : null });
            prms.Add("@DeviceOSVersion", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.UserInfo != null ? model.UserInfo.DeviceOSVersion : null });
            var result = _baseService.ExecuteNonQuery("sp_PatchUser", prms);
            PrimitiveResponse primitiveResult;
            if (result > 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Update successfully." };
            }
            else
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.UpdateNotCompleted, ResponseErrorMessage = "Canceled could not be completed." };
            }
            return primitiveResult;
        }

        public PrimitiveResponse ImSafe(UserDto model)
        {
            PrimitiveResponse primitiveResult = null;
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = model.Id });
            prms.Add("@LastLocation", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.LastLocation });
            var result = _baseService.ExecuteNonQuery("sp_ImSafe", prms);
            primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Insert successfully." };
            return primitiveResult;
        }

        public EntityResponse<UserDto> GetByPhoneNumber(string PhoneNumber)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@PhoneNumber", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = PhoneNumber });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_UserGetByPhoneNumber", prms);
            var singleUser = result.Item1.ToList().SingleOrDefault();
            return new EntityResponse<UserDto>() { ResponseCode = ResponseCodes.Successful, EntityData = singleUser };
        }

        public PrimitiveResponse SendSms(List<TwilioDto> model)
        {
            PrimitiveResponse primitiveResult = null;
            try
            {
                foreach (var item in model)
                {
                    item.PhoneNumber = "+9" + item.PhoneNumber;
                    TwilioService.SendSMS(item);
                }
                primitiveResult = new PrimitiveResponse()
                {
                    ResponseCode = ResponseCodes.Successful,
                    ResponseMessage = "Send Sms Success."
                };
            }
            catch (Exception ex)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.ServiceError, ResponseMessage = ex.Message };
            }
            return primitiveResult;
        }

        public PrimitiveResponse SendSmsActiveUser(TwilioDto model)
        {
            var prms = new Dictionary<string, SqlParam>();
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GetActiveUserList", prms);
            var userList = result.Item1.ToList();
            PrimitiveResponse primitiveResult = null;
            try
            {
                foreach (var item in userList)
                {
                    item.PhoneNumber = "+9" + item.PhoneNumber;
                    //TwilioService.SendSMS(new TwilioDto { PhoneNumber = item.PhoneNumber, Content = model.Content });
                }
                primitiveResult = new PrimitiveResponse()
                {
                    ResponseCode = ResponseCodes.Successful,
                    ResponseMessage = "Send Sms Success."
                };
            }
            catch (Exception ex)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.ServiceError, ResponseMessage = ex.Message };
            }
            return primitiveResult;
        }
    }
}
