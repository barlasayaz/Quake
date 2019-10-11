using Quake.Dto;
using Quake.Service.Earthquakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Quake.API.Controllers
{
    public class EarthquakesController : ApiController
    {
        static HttpClient client;
        private readonly IEarthquakesService _earthQuakeService;
        public EarthquakesController(IEarthquakesService earthquakesService)
        {
            this._earthQuakeService = earthquakesService;
            client.BaseAddress = new Uri(@"https://cp.pushwoosh.com/json/1.3/createMessage");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/earthquake/post")]
        //public HttpResponseMessage Post(EarthquakeDto model)
        //{
        //    var result = _earthQuakeService.Post(model);
        //    if (result.ResponseCode == 0) SendPushNotifications();
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/earthquake/get/{id}")]
        //public HttpResponseMessage Get(int id)
        //{
        //    var result = _earthQuakeService.Get(id);
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //private static async Task SendPushNotifications()
        //{
        //    string applePayload = "{\"aps\": {\"alert\": \"Message to IOS\" }}";
        //    string androidPayload = "{\"data\": {\"message\": \"Message to Android\" }}";

        //    var hub = Notifications.Instance.Hub;

        //    await hub.SendGcmNativeNotificationAsync(androidPayload);
        //    await hub.SendAppleNativeNotificationAsync(applePayload);

        //    var obj = new
        //    {
        //        request = new
        //        {
        //            application = "13612-48EE6",
        //            auth = "1RMOPwayuf51HtZ8FDqi1JQGIO0BulJXgA1PzhSGsYwyHmbwz99ZBrraN1ItUsDKRYDf9PCsbP7cGdK5CeBu",
        //            notifications = new[]
        //            {
        //                new
        //                {
        //                    send_date = "now",
        //                    ignore_user_timezone = true,
        //                    content = "Message from Pushwoosh!",
        //                }
        //            }
        //        }
        //    };
        //    await client.PostAsJsonAsync("", obj);
        //}
    }
}