using Quake.Dto;
using Quake.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quake.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        [Route("api/users")]
        public HttpResponseMessage Post(UserDto model)
        {
            var result = _userService.Post(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/users/pushwoosh")]
        public HttpResponseMessage Pushwoosh()
        {
            Quake.API.Pushwoosh.PushwooshClient.PushwooshTest();
            return Request.CreateResponse(HttpStatusCode.OK, "OK");
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            var result = _userService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/users/{userid}")]
        public HttpResponseMessage Get(string userid)
        {
            var result = _userService.Get(userid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPatch]
        [Route("api/users")]
        public HttpResponseMessage Patch(UserDto model)
        {
            var result = _userService.Patch(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/users/imsafe")]
        public HttpResponseMessage ImSafe(UserDto model)
        {
            var result = _userService.ImSafe(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/users/getbyphonenumber/{phonenumber}")]
        public HttpResponseMessage GetByPhoneNumber(string phonenumber)
        {
            var result = _userService.GetByPhoneNumber(phonenumber);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/users/sendsms")]
        public HttpResponseMessage SendSms(List<TwilioDto> model)
        {
            var result = _userService.SendSms(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/users/sendsmsactiveuser")]
        public HttpResponseMessage SendSmsActiveUser(TwilioDto model)
        {
            var result = _userService.SendSmsActiveUser(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}