using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [JsonProperty("topic_name")]
        public string TopicName { get; set; }
    }
}
