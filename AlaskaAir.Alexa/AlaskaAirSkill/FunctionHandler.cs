using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

using AlexaSkillsKit;

namespace AlaskaAir.Alexa.AlaskaAirSkill
{
    public class FunctionHandler 
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            {
                log.Info($"Request={req}");
                var speechlet = new AlaskaAirSpeechlet(log);
                var getResponse = await speechlet.GetResponseAsync(req);

                return getResponse;
            }
        }
    }
}