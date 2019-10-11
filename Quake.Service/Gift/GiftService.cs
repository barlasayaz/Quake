using Quake.Dto;
using Quake.Repository.QuakeRepository;
using Quake.Repository.QuakeSqlParameter;
using Quake.Service.ExternalServices;
using Quake.Toolkit.Enums;
using Quake.Toolkit.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Quake.Service.Gift
{
    public class GiftService : IGiftService
    {
        private readonly IQuakeRepository _baseService;

        public GiftService(IQuakeRepository baseService)
        {
            _baseService = baseService;
        }

        public PrimitiveResponse BuyGift(int userId, int giftCount)
        {
            PrimitiveResponse primitiveResult = null;
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
            prms.Add("@GiftCount", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = giftCount });
            var result = _baseService.ExecuteNonQuery("sp_BuyGift", prms);
            primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Insert successfully." };
            return primitiveResult;
        }

        public EntityResponse<UserDto> AsAGift(int userId, List<UserDto> model)
        {
            UserDto me = null;
            foreach (var item in model)
            {
                if (!this.IsPremiumUser(item.PhoneNumber))
                {
                    me = this.GetUser(userId);
                    if (me.GiftCount > me.GiftPendingCount)
                    {
                        var prms = new Dictionary<string, SqlParam>();
                        prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
                        prms.Add("@PhoneNumber", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = item.PhoneNumber });
                        var result = _baseService.ExecuteNonQuery("sp_AsAGift", prms);
                        if (result != 0)
                            TwilioService.SendSMS(new TwilioDto { Content = me.FirstName + " " + me.LastName + " adlı arkadaşınız size 7TP Deprem Erken Uyarı Sistemini hediye etti. Uygulamayı indirmek için http://7tpbilisim.com/store/ adresine gidebilirsiniz.", PhoneNumber = "+9" + item.PhoneNumber });
                    }
                }
            }
            return new EntityResponse<UserDto>() { ResponseCode = ResponseCodes.Successful, EntityData = me };
        }

        private bool IsPremiumUser(string phoneNumber)
        {
            bool returnResult = false;
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@PhoneNumber", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = phoneNumber });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_IsPremiumToUser", prms);
            if (result.Item1.SingleOrDefault() == null) returnResult = false;
            else returnResult = true;
            return returnResult;
        }

        public PrimitiveResponse GiftPopupShown(int userId)
        {
            PrimitiveResponse primitiveResult = null;
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
            var result = _baseService.ExecuteNonQuery("sp_GiftPopupShown", prms);
            primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Insert successfully." };
            return primitiveResult;
        }

        private UserDto GetUser(int userId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GetUserById", prms);
            return result.Item1.SingleOrDefault();
        }
    }
}
