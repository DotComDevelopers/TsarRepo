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
            _client = new TwilioRestClient("AC92899c64b86aefec4836139d443487ad", "c32f724903485277ecc816e298169f27");
        }

        public async Task<MessageResource> SendMessage(string text, string sendToNumber)
        {
            sendToNumber = "+27711919156";

            var toPhoneNumber = new PhoneNumber(sendToNumber);
            return await MessageResource.CreateAsync(
                toPhoneNumber,
                @from: new PhoneNumber("+12522854117"),
                body: text,
                mediaUrl: null,
                client: _client);


        }

    }

}