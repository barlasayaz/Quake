using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quake.Service.Regions;

namespace Quake.API.Controllers
{
    public class RegionsController : ApiController
    {
        private readonly IRegionsService _regionService;
        public RegionsController(IRegionsService regionService)
        {
            this._regionService = regionService;
        }

        [HttpGet]
        [Route("api/regions")]
        public HttpResponseMessage Get()
        {
            var result = _regionService.GetRegion();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}