using Quake.Dto;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Earthquakes
{
    public interface IEarthquakesService
    {
        PrimitiveResponse Post(EarthquakeDto model);
        EntityResponse<EarthquakeDto> Get(int id);
    }
}
