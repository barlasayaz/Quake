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

namespace Quake.Service.Earthquakes
{
    public class EarthquakesService: IEarthquakesService
    {
        private readonly IQuakeRepository _baseService;
        public EarthquakesService(IQuakeRepository baseService)
        {
            _baseService = baseService;
        }

        public EntityResponse<EarthquakeDto> Get(int id)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@Id", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = id });
            var result = _baseService.QueryWithMultiOutput<EarthquakeDto>("sp_GetEarthquake", prms);
            var singleEarthquake = result.Item1.ToList().SingleOrDefault();
            return new EntityResponse<EarthquakeDto>() { ResponseCode = ResponseCodes.Successful, EntityData = singleEarthquake };
        }

        public PrimitiveResponse Post(EarthquakeDto model)
        {
            var prms = new Dictionary<string, SqlParam>();
            prms.Add("@StationId", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.StationId });
            prms.Add("@HarmLevel", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.HarmLevel });
            prms.Add("@EarthquakeType", new SqlParam() { Direction = System.Data.ParameterDirection.Input, SqlType = System.Data.SqlDbType.NVarChar, Value = model.EarthquakeType });
            var result = _baseService.ExecuteNonQuery("sp_PostEarthquake", prms);
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
    }
}
