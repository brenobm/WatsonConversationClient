using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatsonConversationClient.Helpers
{
    public class TokenState
    {
        private static TokenState tokenTTS;
        private static TokenState tokenSTT;

        private TokenState()
        {
            TokenValue = string.Empty;
            CreationDate = DateTime.MinValue;
        }

        public static TokenState TokenTTS
        {
            get
            {
                if (tokenTTS == null)
                    tokenTTS = new TokenState();

                return tokenTTS;
            }
        }

        public static TokenState TokenSTT
        {
            get
            {
                if (tokenSTT == null)
                    tokenSTT = new TokenState();

                return tokenSTT;
            }
        }

        public string TokenValue { get; set; }
        public DateTime CreationDate { get; set; }

        public bool IsValid()
        {
            return (DateTime.Now - CreationDate).TotalHours < 1;
        }

    }
}
