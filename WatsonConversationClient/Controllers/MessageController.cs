using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WatsonConversationClient.Configuration;
using WatsonConversationClient.Helpers;
using WatsonConversationClient.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WatsonConversationClient.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private ConversationHelper conversation;
        private PoCConversationConfiguration configSettings;

        public MessageController(IOptions<PoCConversationConfiguration> configSettings)
        {
            this.configSettings = configSettings.Value;

            conversation = new ConversationHelper(
                this.configSettings.ConversationUsername, 
                this.configSettings.ConversationPassword, 
                this.configSettings.ConversationWorkspaceId,
                this.configSettings.ProxyUse,
                this.configSettings.ProxyHost,
                this.configSettings.ProxyPort,
                this.configSettings.ProxyHost,
                this.configSettings.ProxyPassword);
        }

        // POST api/values
        [HttpPost]
        [Route("")]
        public MessageResponse Post([FromBody]MessageRequest context)
        {
            return conversation.SendMessage(context);
        }

        [HttpPost]
        [Route("tokenTTS")]
        public TTSParams GetTTSToken()
        {
            string token = null;

            var tokenKeeper = TokenState.TokenTTS;

            if (tokenKeeper.IsValid())
            {
                token = tokenKeeper.TokenValue;
            }
            else
            {
                HttpClient client = NetworkHelper.ConstruirHttpClient(this.configSettings.ProxyUse,
                this.configSettings.ProxyHost,
                this.configSettings.ProxyPort,
                this.configSettings.ProxyHost,
                this.configSettings.ProxyPassword);

                string textToSpeechUser = this.configSettings.TextToSpeechUsername;
                string textToSpeechPassword = this.configSettings.TextToSpeechPassword;

                var byteArray = Encoding.ASCII.GetBytes($"{textToSpeechUser}:{textToSpeechPassword}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                HttpResponseMessage response = client.GetAsync(this.configSettings.SpeechToTextWatsonUrlAuthAPI).Result;
                HttpContent content = response.Content;

                token = content.ReadAsStringAsync().Result;

                tokenKeeper.TokenValue = token;
                tokenKeeper.CreationDate = DateTime.Now;
            }

            TTSParams tsParams = new TTSParams()
            {
                Text = "",
                Token = token,
                Voice = this.configSettings.SpeechToTextVoice
            };
                
            return tsParams;
        }

        [HttpPost]
        [Route("tokenSTT")]
        public STTParams GetSTTToken()
        {
            string token = null;

            var tokenKeeper = TokenState.TokenSTT;

            if (tokenKeeper.IsValid())
            {
                token = tokenKeeper.TokenValue;
            }
            else
            {
                HttpClient client = NetworkHelper.ConstruirHttpClient(this.configSettings.ProxyUse,
                this.configSettings.ProxyHost,
                this.configSettings.ProxyPort,
                this.configSettings.ProxyUser,
                this.configSettings.ProxyPassword);

                string speechToTextUser = this.configSettings.SpeechToTextUsername;
                string speechToTextPassword = this.configSettings.SpeechToTextPassword;

                var byteArray = Encoding.ASCII.GetBytes($"{speechToTextUser}:{speechToTextPassword}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                HttpResponseMessage response = client.GetAsync(this.configSettings.TextToSpeechWatsonUrlAuthAPI).Result;
                HttpContent content = response.Content;

                token = content.ReadAsStringAsync().Result;

                tokenKeeper.TokenValue = token;
                tokenKeeper.CreationDate = DateTime.Now;
            }

            STTParams sttParams = new STTParams()
            {
                Token = token,
                Model = this.configSettings.TextToSpeechModel,
                OutputElement = this.configSettings.TextToSpeechOutputElement
            };

            return sttParams;
        }
    }
}
