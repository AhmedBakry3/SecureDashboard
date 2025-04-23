using Demo.Presentation.Settings;
using Demo.Presentation.Utilities;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.Presentation.Helper.SmsService
{
    public class SmsService(IOptions<SmsSettings> _options) : ISmsService
    {
        public MessageResource SendSms(SmsMessage smsMessage)
        {
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);

            var Message = MessageResource.Create(
                to: smsMessage.PhoneNumber,
                from: new Twilio.Types.PhoneNumber(_options.Value.TwilioPhoneNumber),
                body: smsMessage.Body
            );

            return Message;
        }
    }
}
