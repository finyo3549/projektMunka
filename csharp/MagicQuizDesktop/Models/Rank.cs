using Newtonsoft.Json;

namespace MagicQuizDesktop.Models;

/// <summary>
/// The Rank class represents a rank status and information about the user in a system.
/// This class includes properties related to the user and rank status like RankNumber, RankColor, Name, UserId, Score, Email.
/// There are two constructors available: a default one which initializes properties with default values, and a parameterized one which allows all properties to be set.
/// Provides a FromJson method to create a Rank object from a JSON string.
/// </summary>
public class Rank
{
    /// <summary>
    /// Initializes a new instance of the Rank class with default values.
    /// </summary>
    public Rank()
    {
        RankNumber = 0;
        RankColor = "blue";
        Name = "unavailable";
        UserId = 0;
        Score = 0;
        Email = "unavailable";
    }


    /// <summary>
    /// Initializes a new instance of the Rank class with the specified rank number, rank color, name, user id, score and email. 
    /// </summary>
    /// <param name="rankNumber">The rank number.</param>
    /// <param name="rankColor">The color of the rank.</param>
    /// <param name="name">The name of the user.</param>
    /// <param name="userId">The id of the user.</param>
    /// <param name="score">The score of the user.</param>
    /// <param name="email">The email of the user.</param>
    public Rank(int rankNumber, string rankColor, string name, int userId, int score, string email)
    {
        RankNumber = rankNumber;
        RankColor = rankColor;
        Name = name;
        UserId = userId;
        Score = score;
        Email = email;
    }

    /// <summary>
    /// Gets or sets the rank number.
    /// </summary>
    public int RankNumber { get; set; }

    /// <summary>
    /// Gets or sets the RankColor.
    /// </summary>
    public string RankColor { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// This property is annotated with the JsonProperty attribute to link it with the 'name' JSON key.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// This property is annotated with the JsonProperty attribute to link it with the 'user_id' JSON key.
    /// </summary>
    [JsonProperty("user_id")]
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the score.
    /// This property is annotated with the JsonProperty attribute to link it with the 'score' JSON key.
    /// </summary>
    [JsonProperty("score")]
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// This property is annotated with the JsonProperty attribute to link it with the 'email' JSON key.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    ///     Creates a Rank object from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to convert into a Rank object.</param>
    /// <returns>A Rank object derived from the JSON string.</returns>
    public static Rank FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Rank>(json, Converter.Settings);
    }
}
