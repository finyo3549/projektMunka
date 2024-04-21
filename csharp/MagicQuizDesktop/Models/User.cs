using Newtonsoft.Json;

namespace MagicQuizDesktop.Models;

/// <summary>
///     The User class represents a user in a system. This class includes properties related to user details like Id, Name,
///     Email, Password, Avatar, Gender, IsActive, IsAdmin and AuthToken.
///     There are two constructors available: a default one which initializes properties with default values, and a
///     parameterized one which allows all properties to be set.
///     Provides a FromJson method to create a User object from a JSON string.
/// </summary>
public class User
{
    /// <summary>
    ///     Default constructor for the User class. Initializes the User object with default values.
    /// </summary>
    public User()
    {
        Id = 0;
        Name = "unavailable";
        Email = "unavailable";
        Password = "";
        Avatar = "unavailable";
        Gender = "unavailable";
        IsActive = false;
        IsAdmin = false;
        AuthToken = "";
    }


    /// <summary>
    ///     Initializes a new instance of the User class with the specified id, name, email, password, avatar, gender,
    ///     isActive, isAdmin, and authToken.
    /// </summary>
    /// <param name="id">The unique identifier for the User.</param>
    /// <param name="name">The name of the User.</param>
    /// <param name="email">The email of the User.</param>
    /// <param name="password">The password of the User.</param>
    /// <param name="avatar">The avatar of the User.</param>
    /// <param name="gender">The gender of the User.</param>
    /// <param name="isActive">A value indicating whether the User is active.</param>
    /// <param name="isAdmin">A value indicating whether the User is an admin.</param>
    /// <param name="authToken">The authentication token of the User.</param>
    public User(int id, string name, string email, string password, string avatar, string gender, bool isActive,
        bool isAdmin, string authToken)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Avatar = avatar;
        Gender = gender;
        IsActive = isActive;
        IsAdmin = isAdmin;
        AuthToken = authToken;
    }

    /// <summary>
    ///     Gets or sets the Id.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'id' JSON key.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the Name.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'name' JSON key.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the Email.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'email' JSON key.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    ///     Gets or sets the Password.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'password' JSON key.
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; set; }

    /// <summary>
    ///     Gets or sets the Avatar.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'avatar' JSON key.
    /// </summary>
    [JsonProperty("avatar")]
    public string Avatar { get; set; }

    /// <summary>
    ///     Gets or sets the Gender.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'gender' JSON key.
    /// </summary>
    [JsonProperty("gender")]
    public string Gender { get; set; }

    /// <summary>
    ///     Gets or sets whether the user is active.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'is_active' JSON key.
    /// </summary>
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }

    /// <summary>
    ///     Gets or sets whether the user is an administrator.
    ///     This property is annotated with the JsonProperty attribute to link it with the 'is_admin' JSON key.
    /// </summary>
    [JsonProperty("is_admin")]
    public bool IsAdmin { get; set; }

    /// <summary>
    ///     Gets or sets the Auth Token.
    ///     This property does not correspond to a JSON property and is used for handling authentication tokens in the
    ///     application.
    /// </summary>
    public string AuthToken { get; set; }

    /// <summary>
    ///     Creates a User object from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to convert into a User object.</param>
    /// <returns>A User object derived from the JSON string.</returns>
    public static User FromJson(string json)
    {
        return JsonConvert.DeserializeObject<User>(json, Converter.Settings);
    }
}