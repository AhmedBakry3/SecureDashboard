using Demo.Presentation.Utilities;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.Presentation.Helper.SmsService
{
    public interface ISmsService
    {
        public MessageResource SendSms(SmsMessage smsMessage);
    }
}
