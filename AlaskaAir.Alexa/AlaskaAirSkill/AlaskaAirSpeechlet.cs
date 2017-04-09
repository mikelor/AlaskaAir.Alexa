using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using AlexaSkillsKit.Slu;

namespace AlaskaAir.Alexa.AlaskaAirSkill
{
    public class AlaskaAirSpeechlet : SpeechletAsync
    {
        #region Properties
        public TraceWriter Logger { get; set; }
        #endregion

        #region Constructor
        public AlaskaAirSpeechlet(TraceWriter log)
        {
            Logger = log;
        }
        #endregion

        #region Public Overrides
        public override async Task OnSessionStartedAsync(SessionStartedRequest request, Session session)
        {
            Logger.Info($"OnSessionStarted requestId={request.RequestId}, sessionId={session.SessionId}");
        }

        public override async Task OnSessionEndedAsync(SessionEndedRequest request, Session session)
        {
            Logger.Info($"OnSessionStarted requestId={request.RequestId}, sessionId={session.SessionId}");
        }

        public override async Task<SpeechletResponse> OnLaunchAsync(LaunchRequest request, Session session)
        {
            Logger.Info($"OnSessionStarted requestId={request.RequestId}, sessionId={session.SessionId}");
            return await GetWelcomeResponseAsync();
        }

        public override async Task<SpeechletResponse> OnIntentAsync(IntentRequest request, Session session)
        {
            // Get intent from the request object.
            Intent intent = request.Intent;
            string intentName = (intent != null) ? intent.Name : null;

            Logger.Info($"OnIntent intentName={intentName} requestId={request.RequestId}, sessionId={session.SessionId}");


            // Note: If the session is started with an intent, no welcome message will be rendered;
            // rather, the intent specific response will be returned.

            switch (intentName)
            {
                case "HelloWorldIntent":
                    return await BuildHelloWorldResponseAsync(intent, session);
                default:
                    throw new SpeechletException("Invalid Intent");
            }
        }
        #endregion

        #region Private Methods
        private async Task<SpeechletResponse> GetWelcomeResponseAsync()
        {
            // Create the welcome message.
            string speechOutput =  "Say something like\nHi.\nHellow.\nHi Sir.";

            // Here we are setting shouldEndSession to false to not end the session and
            // prompt the user for input
            return await BuildSpeechletResponseAsync("Welcome", speechOutput, false);
        }
        private async Task<SpeechletResponse> BuildSpeechletResponseAsync(string title, string output, bool shouldEndSession)
        {
            // Create the Simple card content.
            SimpleCard card = new SimpleCard();
            card.Title = title;
            card.Content = output;

            // Create the plain text output.
            PlainTextOutputSpeech speech = new PlainTextOutputSpeech();
            speech.Text = output;

            // Create the speechlet response.
            SpeechletResponse response = new SpeechletResponse();
            response.ShouldEndSession = shouldEndSession;
            response.OutputSpeech = speech;
            response.Card = card;

            return response;
        }

        private async Task<SpeechletResponse> BuildHelloWorldResponseAsync(Intent intent, Session session)
        {
            string speechOutput = "What up, dude? Do you know how much Mike Rocks?";
            return await BuildSpeechletResponseAsync(intent.Name, speechOutput, false);
        }
    }
    #endregion
}