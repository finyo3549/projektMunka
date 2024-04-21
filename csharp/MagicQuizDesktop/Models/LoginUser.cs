using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace MagicQuizDesktop.Models;

/// <summary>
///     The LoginUser class represents a logged-in user. This class includes properties related to the user details like
///     UserId and the Token for authorization.
///     There are two constructors available: a default one which initializes properties with default values, and a
///     parameterized one which allows all properties to be set.
///     Provides a FromJson method to create a LoginUser object from a JSON string and a ToJson method to reverse.
/// </summary>
public class LoginUser
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginUser" /> class with default values.
    /// </summary>
    public LoginUser()
    {
        Token = "";
        UserId = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginUser" /> class with the specified token and user ID.
    /// </summary>
    /// <param name="token">The token to be used for authentication.</param>
    /// <param name="userId">The unique identifier for the user.</param>
    public LoginUser(string token, int userId)
    {
        Token = token;
        UserId = userId;
    }

    [JsonProperty("token")] public string Token { get; set; }

    [JsonProperty("user_id")] public int UserId { get; set; }

    /// <summary>
    ///     Creates an instance of LoginUser from a JSON string using specific converter settings.
    /// </summary>
    /// <param name="json">The JSON string to deserialize into a LoginUser object.</param>
    /// <returns>A LoginUser object represented by the provided JSON string.</returns>
    public static LoginUser FromJson(string json)
    {
        return JsonConvert.DeserializeObject<LoginUser>(json, Converter.Settings);
    }
}

/// <summary>
///     A static Serialize class to a JSON.
/// </summary>
public static class Serialize
{
    public static string ToJson(this LoginUser self)
    {
        return JsonConvert.SerializeObject(self, Converter.Settings);
    }
}

/// <summary>
///     Contains a single "Settings" property of type JsonSerializerSettings, preconfigured with specific settings.
///     The MetadataPropertyHandling setting is set to ignore, meaning it will ignore metadata properties when serializing
///     or deserializing.
///     The DateParseHandling is set to none, meaning it does not attempt to parse dates at all.
///     A converter of type IsoDateTimeConverter is added, where the DateTimeStyles is set to AssumeUniversal, meaning it
///     will treat any date time as being in Universal Time.
/// </summary>
internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        }
    };
}