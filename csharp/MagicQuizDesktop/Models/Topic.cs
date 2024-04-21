using Newtonsoft.Json;

namespace MagicQuizDesktop.Models
{
    /// <summary>
    /// The Topic class represents a Topic in a system. This class includes properties related to a topic details like Id and the name of the topic.
    /// There are two constructors available: a default one which initializes properties with default values, and a parameterized one which allows all properties to be set.
    /// Provides a FromJson method to create a Topic object from a JSON string.
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Initializes a new instance of the Topic class with default properties. 
        /// "Id" is set to 0 and "TopicName" to "Default Topic".
        /// </summary>
        public Topic()
        {
            Id = 0;
            TopicName = "unavailable";
        }


        /// <summary>
        /// Initializes a new instance of the Topic class.
        /// </summary>
        /// <param name="id">The unique identifier for the Topic.</param>
        /// <param name="topicName">The name of the Topic.</param>
        public Topic(int id, string topicName)
        {
            Id = id;
            TopicName = topicName;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// This property is annotated with the JsonProperty attribute to link it with the 'id' JSON key.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TopicName.
        /// This property is annotated with the JsonProperty attribute to link it with the 'topicname' JSON key.
        /// The JSON key is set correctly because of the backend.
        /// </summary>
        [JsonProperty("topicname")]
        public string TopicName { get; set; }

        /// <summary>
        /// Creates a Topic object from a JSON string.
        /// </summary>
        /// <param name="json">The JSON string to convert into a Topic object.</param>
        /// <returns>A Topic object derived from the JSON string.</returns>
        public static Topic FromJson(string json) => JsonConvert.DeserializeObject<Topic>(json, Converter.Settings);
    }
}