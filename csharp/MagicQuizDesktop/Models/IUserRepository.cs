using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Models;

/// <summary>
///     Interface for UserRepository, defining methods for user management operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     Authenticates a user with specified username and password.
    /// </summary>
    /// <param name="username">The username of the user to authenticate.</param>
    /// <param name="password">The password of the user to authenticate.</param>
    /// <returns>A task resulting in an ApiResponse containing the authenticated user.</returns>
    Task<ApiResponse<LoginUser>> AuthenticateUser(string username, string password);

    /// <summary>
    ///     Asynchronously retrieves the User object associated with a specific ID, using a specified authentication token.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <param name="authToken">The authentication token used for the API request.</param>
    /// <returns>A ApiResponse object containing the User associated with the given ID.</returns>
    Task<ApiResponse<User>> GetById(int id, string authToken);

    /// <summary>
    ///     Asynchronously retrieves all users as a list.
    /// </summary>
    /// <param name="authToken">The token to authorize the request.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an API response that has a list of
    ///     users.
    /// </returns>
    Task<ApiResponse<List<User>>> GetByAll(string authToken);

    /// <summary>
    ///     Updates the specified user using the provided auth token.
    ///     Returns the server response containing the updated user data.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <param name="authToken">The auth token for authenticating the update operation.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the API response including the updated user data.
    /// </returns>
    Task<ApiResponse<User>> UpdateUser(User user, string authToken);

    /// <summary>
    ///     Asynchronously inactivates the given user using the provided authorization token.
    /// </summary>
    /// <param name="user">The user to be inactivated.</param>
    /// <param name="authToken">The authorization token to use.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The result of the task is an instance of ApiResponse
    ///     containing the user details.
    /// </returns>
    Task<ApiResponse<User>> Inactivate(User user, string authToken);

    /// <summary>
    ///     Logs out a user session with the provided authentication token.
    /// </summary>
    /// <returns>An ApiResponseWithNoData instance representing the result of the logout operation.</returns>
    /// <param name="authToken">The authentication token tied to the user's session.</param>
    Task<ApiResponseWithNoData> LogOut(string authToken);
}