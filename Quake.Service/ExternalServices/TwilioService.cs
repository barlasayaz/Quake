using Quake.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Quake.Service.ExternalServices
{
    public class TwilioService
    {
        public static string SendSMS(TwilioDto model)
        {
            string accountSid = "AC3953fb97b7fa825b0daa97af74d6b943";
            string authToken = "eff0d88734b66ba5684d52f281ac7401";
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber(model.PhoneNumber);
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+17067864854"),
                body: model.Content);
            return message.Sid;
        }
    }
}
