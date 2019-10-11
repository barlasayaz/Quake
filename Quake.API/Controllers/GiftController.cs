using Quake.Dto;
using Quake.Service.Gift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quake.API.Controllers
{
    public class GiftController : ApiController
    {
        private readonly IGiftService _giftService;
        public GiftController(IGiftService giftService)
        {
            this._giftService = giftService;
        }

        [HttpPost]
        [Route("api/gift/buygift/{userid}/{giftcount}")]
        public HttpResponseMessage BuyGift(int userId, int giftcount)
        {
            var result = _giftService.BuyGift(userId,giftcount);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/gift/asagift/{userid}")]
        public HttpResponseMessage BuyGift(int userid, List<UserDto> model)
        {
            var result = _giftService.AsAGift(userid, model);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/gift/giftpopupshown/{userid}")]
        public HttpResponseMessage GiftPopupShown(int userid)
        {
            var result = _giftService.GiftPopupShown(userid);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}