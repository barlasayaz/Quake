using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Quake.API.Pushwoosh
{
    public class PushwooshClient
    {
        public static void PushwooshTest()
        {
            Request matchData = new Request()
            {
                auth = "mpzxdFJdoO6GkvAYXXwmUI0KHekWugj2b1DA9tiajavEPCHRwCq5KGI4lMjEVxTngZ74HeqI94BEPVFffYxM",
                content = "hello",
                devices_filter = "AT(\"9C02E-A6599\", \"region\", IN, [\"Marmara 2\"])"
            };
            RootObject postData = new RootObject { request = matchData };
            string url = "https://7tp.pushwoosh.com/json/1.3/createTargetedMessage";
            string data = JsonConvert.SerializeObject(postData);
            WebRequest myReq = WebRequest.Create(url);
            myReq.Method = "POST";
            myReq.ContentLength = data.Length;
            myReq.ContentType = "application/json; charset=UTF-8";
            UTF8Encoding enc = new UTF8Encoding();
            myReq.Headers.Remove("auth-token");
            using (Stream ds = myReq.GetRequestStream())
            {
                ds.Write(enc.GetBytes(data), 0, data.Length);
            }
            WebResponse wr = myReq.GetResponse();
            Stream receiveStream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader.ReadToEnd();
        }
    }

    public class Request
    {
        public string auth { get; set; }
        public string content { get; set; }
        public string devices_filter { get; set; }
    }

    public class RootObject
    {
        public Request request { get; set; }
    }
}