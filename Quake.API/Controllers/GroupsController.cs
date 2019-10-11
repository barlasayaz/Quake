using Quake.Dto;
using Quake.Service.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quake.API.Controllers
{
    public class GroupsController : ApiController
    {
        private readonly IGroupsService _groupsService;
        public GroupsController(IGroupsService groupsService)
        {
            this._groupsService = groupsService;
        }

        [HttpGet]
        [Route("api/groups/get")]
        public HttpResponseMessage Get(int id)
        {
            var result = _groupsService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/groups")]
        public HttpResponseMessage Post(GroupDto model)
        {
            var result = _groupsService.Post(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPatch]
        [Route("api/groups")]
        public HttpResponseMessage Patch(GroupDto model)
        {
            var result = _groupsService.Put(model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("api/groups/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var result = _groupsService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/groups/user/{userid}")]
        public HttpResponseMessage GetGroupsByUserId(int userid)
        {
            var result = _groupsService.GetGroupsByUserId(userid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/groups/grouptouser/{groupid}")]
        public HttpResponseMessage GetGroupToUser(int groupid)
        {
            var result = _groupsService.GroupToUser(groupid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}