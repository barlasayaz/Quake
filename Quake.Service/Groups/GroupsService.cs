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

namespace Quake.Service.Groups
{
    public class GroupsService : IGroupsService
    {
        private readonly IQuakeRepository _baseService;
        public GroupsService(IQuakeRepository baseService)
        {
            _baseService = baseService;
        }

        public EntityResponse<GroupDto> Get(int id)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = id });
            var result = _baseService.QueryWithMultiOutput<GroupDto>("sp_GetGroup", prms);
            var singleGroup = result.Item1.ToList().SingleOrDefault();
            return new EntityResponse<GroupDto>() { ResponseCode = ResponseCodes.Successful, EntityData = singleGroup };
        }

        public PrimitiveResponse Post(GroupDto model)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = model.UserId });
            prms.Add("@Name", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.Name });
            prms.Add("@ShareLocation", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.ShareLocation });
            prms.Add("@ShareStatus", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.ShareStatus });
            var result = _baseService.ExecuteNonQuery("sp_PostGroup", prms);
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

        public EntityResponse<GroupDto> Put(GroupDto model)
        {
            EntityResponse<GroupDto> returnResult = null;
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = model.Id });
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = model.UserId });
            prms.Add("@Name", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.Name });
            prms.Add("@ShareLocation", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.ShareLocation });
            prms.Add("@ShareStatus", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.ShareStatus });
            prms.Add("@ModifiedDate", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.ModifiedOn });
            var result = _baseService.ExecuteNonQuery("sp_UpdateGroup", prms);
            if (result > 0)
                returnResult = new EntityResponse<GroupDto>() { ResponseCode = ResponseCodes.Successful, EntityData = model };
            else
                returnResult = new EntityResponse<GroupDto>() { ResponseCode = ResponseCodes.UpdateNotCompleted, EntityData = null };

            return returnResult;
        }

        public PrimitiveResponse Delete(int id)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = id });
            var result = _baseService.ExecuteNonQuery("sp_DeleteGroup", prms);
            PrimitiveResponse primitiveResult;
            if (result > 0)
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Delete successfully." };
            }
            else
            {
                primitiveResult = new PrimitiveResponse() { ResponseCode = ResponseCodes.DeleteNotCompleted, ResponseErrorMessage = "Canceled could not be completed." };
            }
            return primitiveResult;
        }

        public EntityListResponse<GroupDto> GetGroupsByUserId(int userid)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@UserId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = userid });
            var result = _baseService.QueryWithMultiOutput<GroupDto>("GetGroupsByUserId", prms);
            var userGroup = result.Item1.ToList();
            return new EntityListResponse<GroupDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = userGroup };
        }

        public EntityListResponse<UserDto> GroupToUser(int groupId)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@GroupId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.Int, Value = groupId });
            var result = _baseService.QueryWithMultiOutput<UserDto>("sp_GroupToUser", prms);
            var groupUser = result.Item1.ToList();
            return new EntityListResponse<UserDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = groupUser };
        }
    }
}
