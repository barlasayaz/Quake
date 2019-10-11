using Quake.Dto;
using Quake.Service.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quake.API.Controllers
{
    public class FriendsController : ApiController
    {
        private readonly IFriendsService _friendsService;
        public FriendsController(IFriendsService friendsService)
        {
            this._friendsService = friendsService;
        }

        [HttpGet]
        [Route("api/friends/{userId}")]
        public HttpResponseMessage Get(int userId)
        {
            var result = _friendsService.Get(userId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("api/friends/{groupid}")]
        public HttpResponseMessage Delete(int groupid, List<UserDto> model)
        {
            var result = _friendsService.Delete(groupid, model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/friends/addfriendstogroup/{groupid}")]
        public HttpResponseMessage AddFriendsToGroup(int groupid, List<UserDto> model)
        {
            // TODO: Grup servisi eklendikten sonra bu kısma kontrol eklenecek...
            var result = _friendsService.AddFriendsToGroup(groupid, model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/friends/getinvitationsreceived/{userid}")]
        public HttpResponseMessage GetInvitationsReceived(int userid)
        {
            var result = _friendsService.GetInvitationsReceived(userid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/friends/getinvitationssent/{userid}")]
        public HttpResponseMessage GetInvitationsSent(int userid)
        {
            var result = _friendsService.GetInvitationsSent(userid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/friends/invitetoanswer")]
        public HttpResponseMessage InviteToAnswer(UserInvitedDto model)
        {
            var result = _friendsService.InviteToAnswer(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}