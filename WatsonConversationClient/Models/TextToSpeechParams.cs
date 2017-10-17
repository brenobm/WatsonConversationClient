using System;
using System.Runtime.Serialization;

namespace WatsonConversationClient.Models
{
    [Serializable]
    public class TTSParams
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "voice")]
        public string Voice { get; set; }
    }
}
