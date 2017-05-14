using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace TSAR.SmsHelper
{
    public interface ITwilioSmsRestClient
    {
        Task<MessageResource> SendMessage(string text, string sendToNumber);
    }

}