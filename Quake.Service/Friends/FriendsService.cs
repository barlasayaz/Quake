using Quake.Dto;
using Quake.Repository.QuakeRepository;
using Quake.Repository.QuakeSqlParameter;
using Quake.Service.ExternalServices;
using Quake.Toolkit.Enums;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Friends
{
    public class FriendsService : IFriendsService
    {
        private readonly IQuakeRepository _baseService;
        public FriendsService(IQuakeRepository baseService)
        {
            _baseService = baseService;
        }

        public EntityListResponse<UserDto> Get(int userId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GetFriends", prms);
            var userList = result.Item1.ToList();
            return new EntityListResponse<UserDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = userList };
        }

        public PrimitiveResponse AddFriendsToGroup(int groupId, List<UserDto> model)
        {
            UserDto me = this.GetUser(groupId);
            PrimitiveResponse primitiveResult = null;
            int groupCount = this.GetGroupCount(groupId);
            if (groupCount == 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.InsertNotCompleted, ResponseMessage = groupId + " Id'li bir grup yok!" };
            }
            else if (model.Where(x => x.PhoneNumber == me.PhoneNumber).ToList().Count != 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.InsertNotCompleted, ResponseMessage = "Kişi kendini gruplara ekleyemez!" };
            }
            else
            {
                foreach (var item in model)
                {
                    var prms = new Dictionary<string, SqlParam>();
                    prms.Add("@GroupId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = groupId });
                    prms.Add("@PhoneNumber", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = item.PhoneNumber });
                    var result = _baseService.ExecuteNonQuery("sp_AddFriendsToGroup", prms);
                    TwilioService.SendSMS(new TwilioDto { Content = me.FirstName + " " + me.LastName + " adlı arkadaşınız sizi 7TP Deprem Erken Uyarı Sistemine davet ediyor. Uygulamayı indirmek için http://7tpbilisim.com/store/ adresine gidebilirsiniz.", PhoneNumber = "+9" + item.PhoneNumber });
                }
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Insert successfully." };
            }
            return primitiveResult;
        }

        public PrimitiveResponse Delete(int groupId, List<UserDto> model)
        {
            PrimitiveResponse primitiveResult = null;
            var friendsCount = this.IsGroupToDeleteFriends(groupId, model);
            if (friendsCount == 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Bu grupta silecek arkadaş yok!" };
            }
            else
            {
                foreach (var item in model)
                {
                    var prms = new Dictionary<string, SqlParam>();
                    prms.Add("@GroupId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = groupId });
                    prms.Add("@FriendUserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = item.Id });
                    var result = _baseService.ExecuteNonQuery("sp_DeleteFriends", prms);
                }
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Delete successfully." };
            }
            return primitiveResult;
        }

        public EntityListResponse<UserDto> GetInvitationsReceived(int userId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GetInvitationsReceived", prms);
            var receivedFriends = result.Item1.ToList();
            return new EntityListResponse<UserDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = receivedFriends };
        }

        public EntityListResponse<UserDto> GetInvitationsSent(int userId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userId });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GetInvitationsSent", prms);
            var receivedFriends = result.Item1.ToList();
            return new EntityListResponse<UserDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = receivedFriends };
        }

        private int GetGroupCount(int groupId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@GroupId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = groupId });
            var result = _baseService.QueryWithMultiOutput<int>("sp_GetGroupCount", prms);
            return result.Item1.SingleOrDefault();
        }

        private UserDto GetUser(int groupId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@GroupId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = groupId });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GroupIsOwnPhoneNumber", prms);
            return result.Item1.SingleOrDefault();
        }

        private int IsGroupToDeleteFriends(int groupId, List<UserDto> FriendUser)
        {
            string queryMatch = string.Empty;
            foreach (var item in FriendUser)
            {
                queryMatch += "" + item.Id + ",";
            }
            queryMatch = queryMatch.Substring(0, queryMatch.Length - 1);
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@GroupId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = groupId });
            prms.Add("@UserIdList", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = queryMatch });
            var result = _baseService.QueryWithMultiOutput<int>("sp_GroupToDeleteFriends", prms);
            return result.Item1.SingleOrDefault();
        }

        public PrimitiveResponse InviteToAnswer(UserInvitedDto model)
        {
            PrimitiveResponse primitiveResult = null;
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = model.Id });
            prms.Add("@FriendshipStatus", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = model.FriendshipStatus });
            var result = _baseService.ExecuteNonQuery("sp_InviteToAcceptOrReject", prms);
            primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Insert successfully." };
            return primitiveResult;
        }


    }
}
