using Quake.Dto;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Users
{
    public interface IUserService
    {
        PrimitiveResponse Post(UserDto model);
        PrimitiveResponse Patch(UserDto model);
        PrimitiveResponse Delete(string pushNotificationId);
        EntityResponse<UserDto> Get(string PushNotificationId);
        PrimitiveResponse ImSafe(UserDto model);
        EntityResponse<UserDto> GetByPhoneNumber(string PhoneNumber);
        PrimitiveResponse SendSms(List<TwilioDto> model);
        PrimitiveResponse SendSmsActiveUser(TwilioDto model);
    }
}
