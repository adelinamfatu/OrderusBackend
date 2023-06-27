using App.BusinessLogic.SMSLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace App.Server
{
    public static class TwilioSMSService
    {
        static string accountSid = TwilioAccountInformation.SID;
        static string authToken = TwilioAccountInformation.AuthToken;

        public static void Start()
        {
            TwilioClient.Init(accountSid, authToken);
            Task.Run(() =>
            {
                SendSMS();
            });
        }

        private static void SendSMS()
        {
            var smsDisplay = new SMSDisplay();

            while (true)
            {
                var orderTime = DateTime.Now;
                var orders = smsDisplay.RetrieveDataForSchedullingSMS();

                foreach(var order in orders)
                {
                    if(order.Value == BusinessLogic.Helper.NextOrderType.NextHourOrder)
                    {
                        //SendSMSThroughTwilio(order.Key, TwilioMessage.NextHourMessage.Replace("{0}", orderTime.AddHours(1).ToString("dd.MM.yyyy HH:mm")));
                    }
                    if (order.Value == BusinessLogic.Helper.NextOrderType.NextDayOrder)
                    {
                        //SendSMSThroughTwilio(order.Key, TwilioMessage.NextDayMessage.Replace("{0}", orderTime.AddDays(1).ToString("HH:mm")));
                    }
                }

                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }

        public static void SendSMSThroughTwilio(string phoneNumber, string msgBody)
        {
            var message = MessageResource.Create(
                body: msgBody,
                from: new Twilio.Types.PhoneNumber(TwilioAccountInformation.TwilioNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber));
        }
    }
}
