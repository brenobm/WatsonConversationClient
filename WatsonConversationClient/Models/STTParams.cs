using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WatsonConversationClient.Models
{
    [Serializable]
    public class STTParams
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "model")]
        public string Model { get; set; }
        [DataMember(Name = "outputElement")]
        public string OutputElement { get; set; }

    }
}
