using IBM.WatsonDeveloperCloud.Conversation.v1;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Http;

namespace WatsonConversationClient.Helpers
{
    public class ConversationHelper
    {
        private string username;
        private string password;
        private string workspaceId;
        private ConversationService _conversation;

        public ConversationHelper(string username, string password, string workspaceId, bool useProxy, string proxyAddress, string proxyPort, string proxyUser, string proxyPassword)
        {
            this.username = username;
            this.password = password;
            this.workspaceId = workspaceId;

            _conversation = new ConversationService(username, password, ConversationService.CONVERSATION_VERSION_DATE_2017_05_26);

            if (useProxy)
            {
                WatsonHttpClient httpClient = new WatsonHttpClient(
                    _conversation.Endpoint,
                    username,
                    password,
                    NetworkHelper.ConstruirHttpClient(useProxy, proxyAddress, proxyPort, proxyUser, proxyPassword));

                _conversation.Client = httpClient;
            }
        }

        public MessageResponse SendMessage(string message, dynamic context = null)
        {
            MessageRequest messageRequest0 = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = message
                },
                Context = context
            };

            return _conversation.Message(this.workspaceId, messageRequest0);
        }

        public MessageResponse SendMessage(MessageRequest message)
        {
            if (message == null)
            {
                message = new MessageRequest();
                message.Input = new { Text = "" };
            }

            return _conversation.Message(this.workspaceId, message);
        }

    }
}
