using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Repositories;

/// <summary>
///     UserRepository class that implements IUserRepository. This class utilizes a QuizApiService and provides methods for
///     authenticating,
///     getting user(s), updating user, inactivating user and logging out.
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    ///     The API service.
    /// </summary>
    private readonly QuizApiService _apiService;

    /// <summary>
    ///     Initializes a new instance of the UserRepository class.
    /// </summary>
    public UserRepository()
    {
        _apiService = new QuizApiService();
    }

    /// <summary>
    ///     Asynchronously authenticates the user given their email and password.
    ///     It sends a POST request to the "/login" endpoint.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>An ApiResponse containing details of the logged in user.</returns>
    public async Task<ApiResponse<LoginUser>> AuthenticateUser(string email, string password)
    {
        var data = new { email, password };
        return await _apiService.PostAsync<LoginUser>("/login", data);
    }


    /// <summary>
    ///     Asynchronously retrieves a User object with the specified ID from an external API service.
    ///     Uses an authentication token for secure access to the service.
    /// </summary>
    /// <param name="id">The ID of the User to retrieve.</param>
    /// <param name="authToken">The authentication token for secure access to the service.</param>
    /// <returns>A Task representing the asynchronous operation, containing an ApiResponse of User.</returns>
    public async Task<ApiResponse<User>> GetById(int id, string authToken)
    {
        return await _apiService.GetAsync<User>($"/users/{id}", authToken);
    }


    /// <summary>
    ///     Asynchronously retrieves a list of all users.
    /// </summary>
    /// <param name="authToken">The authentication token to use for the API request.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an ApiResponse object with a list
    ///     of User objects.
    /// </returns>
    public async Task<ApiResponse<List<User>>> GetByAll(string authToken)
    {
        return await _apiService.GetAsync<List<User>>("/users", authToken);
    }


    /// <summary>
    ///     Asynchronously updates a user using the provided user object and authentication token.
    /// </summary>
    /// <param name="user">The user to be updated.</param>
    /// <param name="authToken">The authentication token for API access.</param>
    /// <returns>
    ///     A task that represents the asynchronous update operation. The task result contains the API response with the
    ///     updated user.
    /// </returns>
    public async Task<ApiResponse<User>> UpdateUser(User user, string authToken)
    {
        return await _apiService.PutAsync<User>($"/users/{user.Id}", user, authToken);
    }


    /// <summary>
    ///     Asynchronously inactivates the specified user.
    ///     Makes a POST request to the '/users/inactivate/{user.Id}' endpoint of the API with the passed user and
    ///     authentication token.
    /// </summary>
    /// <param name="user">The user to be inactivated.</param>
    /// <param name="authToken">The authentication token required for the API request.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the API response with the
    ///     inactivated user details.
    /// </returns>
    public async Task<ApiResponse<User>> Inactivate(User user, string authToken)
    {
        return await _apiService.PostAsync<User>($"/users/inactivate/{user.Id}", user, authToken);
    }


    /// <summary>
    ///     Logs out the user using their authentication token.
    /// </summary>
    /// <param name="authToken">The authentication token of the user.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an ApiResponseWithNoData that
    ///     represents the API response.
    /// </returns>
    public async Task<ApiResponseWithNoData> LogOut(string authToken)
    {
        return await _apiService.PostAsyncWithNoData("/logout", authToken);
    }
}