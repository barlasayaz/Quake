using Quake.Dto;
using Quake.Service.UserAlarms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quake.API.Controllers
{
    public class UserAlarmsController : ApiController
    {
        private readonly IUserAlarmsService _userAlarmsService;
        public UserAlarmsController(IUserAlarmsService userAlarmssService)
        {
            this._userAlarmsService = userAlarmssService;
        }

        [HttpGet]
        [Route("api/useralarms/{userid}")]
        public HttpResponseMessage Get(int userid)
        {
            var result = _userAlarmsService.Get(userid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/useralarms")]
        public HttpResponseMessage Post(List<UserAlarmDto> model)
        {
            if (model != null && model.Count >= 1)
            {
                if (model[0].UserId != 0)
                    _userAlarmsService.Delete(model[0].UserId);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "UserId nesnesi boş olamaz!");
            }
            var result = _userAlarmsService.Post(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("api/useralarms/{deleteuserid}")]
        public HttpResponseMessage Delete(int deleteuserid)
        {
            var result = _userAlarmsService.Delete(deleteuserid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}