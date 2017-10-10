using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TSAR.SmsHelper
{
    public class TwilioSmsRestClient : ITwilioSmsRestClient
    {
        private readonly ITwilioRestClient _client;

        public TwilioSmsRestClient()
        {
            _client = new TwilioRestClient("AC199a67c53269d7e50b9f14a622fec90e", "8ab9cfd3eOF8a2554dc62a008b3ed81d");
        }

        /*DO NOT REMOVE COMMENT:
         * TwilioRestClient only implements SendMessage as async, and caters for methods
         CreatAsync and Create. System needs to be aware of wether or not the sms was sent successfully or not
         to ensure view is updated correctly, therefore the method must be async but the return cannot be await 
         when using MessageResource.Create()*/
        public async Task<MessageResource> SendMessage(string text)
        {
            var toPhoneNumber = new PhoneNumber("+27843927758");
            return MessageResource.Create(
                toPhoneNumber,
                @from: new PhoneNumber("+14049484680"),
                body: text,
                mediaUrl: null,
                client: _client);
        }

        public async Task<MessageResource> SendMessage(string text, string sendToNumber)
        {
            var toPhoneNumber = new PhoneNumber(sendToNumber);
            return MessageResource.Create(
                toPhoneNumber,
                @from: new PhoneNumber("+14049484680"),
                body: text,
                mediaUrl: null,
                client: _client);
        }

    }

}