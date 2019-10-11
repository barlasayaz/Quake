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

namespace Quake.Service.Regions
{
    public class RegionsService : IRegionsService
    {
        private readonly IQuakeRepository _baseService;
        public RegionsService(IQuakeRepository baseService)
        {
            _baseService = baseService;

        }

        public EntityListResponse<RegionDto> GetRegion()
        {
            var prms = new Dictionary<string, SqlParam>();
            var result = _baseService.QueryWithMultiOutput<RegionDto>("sp_GetRegion", prms);
            var allregions = result.Item1.ToList();
            return new EntityListResponse<RegionDto>() { ResponseCode = ResponseCodes.Successful, EntityDataList = allregions };
        }
    }
}
