using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicQuizDesktop.Models;

/// <summary>
///     The Question class represents a question in a system. This class includes properties related to question details
///     like Id, QuestionText, TopicId and a list of the answers.
///     There are two constructors available: a default one which initializes properties with default values, and a
///     parameterized one which allows all properties to be set.
///     Provides a FromJson method to create a Rank object from a JSON string.
/// </summary>
public class Question
{
    /// <summary>
    ///     Initializes a new instance of the Question class with default values.
    /// </summary>
    public Question()
    {
        Id = 0;
        QuestionText = "";
        TopicId = 0;
        Answers = new List<Answer>();
    }

    /// <summary>
    ///     Initializes a new instance of the Question class with the specified identifier, question text, topic identifier and
    ///     list of answers.
    /// </summary>
    /// <param name="id">The identifier of the Question.</param>
    /// <param name="questionText">The text of the Question.</param>
    /// <param name="topicId">The identifier of the Topic relevant to the Question.</param>
    /// <param name="answers">The list of Answers associated with the Question.</param>
    public Question(int id, string questionText, int topicId, List<Answer> answers)
    {
        Id = id;
        QuestionText = questionText;
        TopicId = topicId;
        Answers = answers;
    }


    /// <summary>
    ///     Gets or sets the score.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'score' JSON key.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the score.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'score' JSON key.
    /// </summary>
    [JsonProperty("question_text")]
    public string QuestionText { get; set; }

    /// <summary>
    ///     Gets or sets the score.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'score' JSON key.
    /// </summary>
    [JsonProperty("topic_id")]
    public int TopicId { get; set; }

    /// <summary>
    ///     Gets or sets the score.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'score' JSON key.
    /// </summary>
    [JsonProperty("answers")]
    public List<Answer> Answers { get; set; }

    /// <summary>
    ///     Creates a Question object from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to convert into a Question object.</param>
    /// <returns>A Question object derived from the JSON string.</returns>
    public static Question FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Question>(json, Converter.Settings);
    }
}