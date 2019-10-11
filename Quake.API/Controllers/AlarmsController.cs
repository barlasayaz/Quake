using Quake.Service.Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quake.API.Controllers
{
    public class AlarmsController : ApiController
    {
        private readonly IAlarmService _alarmService;
        public AlarmsController(IAlarmService alarmService)
        {
            this._alarmService = alarmService;
        }

        [HttpGet]
        [Route("api/alarms/getalarms")]
        public HttpResponseMessage GetAlarms()
        {
            var result = _alarmService.GetAlarms();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/alarms/getsinglealarm/{id}")]
        public HttpResponseMessage GetSingleAlarm(int id)
        {
            var result = _alarmService.GetAlarm(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}