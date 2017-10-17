namespace WatsonConversationClient.Configuration
{
    public class PoCConversationConfiguration
    {
        public string ConversationUsername { get; set; }
        public string ConversationPassword { get; set; }
        public string ConversationWorkspaceId { get; set; }
        public string SpeechToTextUsername { get; set; }
        public string SpeechToTextPassword { get; set; }
        public string TextToSpeechUsername { get; set; }
        public string TextToSpeechPassword { get; set; }
        public string TextToSpeechWatsonUrlAuthAPI { get; set; }
        public string TextToSpeechModel { get; set; }
        public string TextToSpeechOutputElement { get; set; }
        public string SpeechToTextVoice { get; set; }
        public string SpeechToTextWatsonUrlAuthAPI { get; set; }
        public bool ProxyUse { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPassword { get; set; }
        public string ProxyHost { get; set; }
        public string ProxyPort { get; set; }
    }


}
