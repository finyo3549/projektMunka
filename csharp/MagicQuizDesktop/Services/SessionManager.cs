using MagicQuizDesktop.Models;

namespace MagicQuizDesktop.Services;

/// <summary>
///     Represents a SessionManager class. Allows for instance creation,
///     maintaining, setting and managing a current user.
/// </summary>
/// Class level variable _instance represents a single instance of the SessionManager class.
/// Class level variable _usersViewModel holds the ViewModel for the user interactions.
/// Method GetInstance returns the single instance of the SessionManager class.
/// Property CurrentUser gets or sets the current User object that has been set in the SessionManager.
/// Method SetCurrentUser sets the CurrentUser property to the passed in User object.
/// Method ClearCurrentUser resets the CurrentUser property to null.
public class SessionManager
{
    private static SessionManager _instance;

    /// <summary>
    ///     Gets the single instance of the SessionManager, creating it if it doesn't already exist.
    ///     This design is known as the Singleton pattern.
    /// </summary>
    public static SessionManager Instance
    {
        get
        {
            if (_instance == null) _instance = new SessionManager();
            return _instance;
        }
    }

    public User CurrentUser { get; private set; }

    /// <summary>
    ///     Sets the current user to the provided user.
    /// </summary>
    /// <param name="user">The user to set as the current user.</param>
    public void SetCurrentUser(User user)
    {
        CurrentUser = user;
    }

    /// <summary>
    ///     Clears the current user setting it to null.
    /// </summary>
    public void ClearCurrentUser()
    {
        CurrentUser = null;
    }
}