using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Repositories;

/// <summary>
///     This class performs API calls for CRUD operations on the question data.
///     The class for repository that handles operations related to ranking in a quiz application.
///     Implements the <see cref="IRankRepository" />.
/// </summary>
public class RankRepository : IRankRepository
{
    /// <summary>
    ///     The API service.
    /// </summary>
    private readonly QuizApiService _apiService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RankRepository" /> class.
    /// </summary>
    public RankRepository()
    {
        _apiService = new QuizApiService();
    }

    /// <summary>
    ///     Asynchronously updates the rank of a specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="rank">The new rank of the user.</param>
    /// <param name="authToken">The authentication token to authorize the request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated rank of the user.</returns>
    public async Task<ApiResponse<Rank>> PutScore(int userId, Rank rank, string authToken)
    {
        return await _apiService.PutAsync<Rank>($"/user-ranks/{userId}", rank, authToken);
    }

    /// <summary>
    ///     Asynchronously retrieves the score for a specific user using their ID and authentication token.
    /// </summary>
    /// <param name="userId">The unique identifier for the user.</param>
    /// <param name="authToken">The authentication token for the user.</param>
    /// <returns>The user's score wrapped in ApiResponse.</returns>
    public async Task<ApiResponse<Rank>> GetScore(int userId, string authToken)
    {
        return await _apiService.GetAsync<Rank>($"/user-ranks/{userId}", authToken);
    }

    /// <summary>
    ///     Asynchronously retrieves the ranks of users.
    /// </summary>
    /// <param name="authToken">The authentication token used in the API service call.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an API response with a list of
    ///     user ranks.
    /// </returns>
    public async Task<ApiResponse<List<Rank>>> GetRanks(string authToken)
    {
        return await _apiService.GetAsync<List<Rank>>("/user-ranks", authToken);
    }

    /// <summary>
    ///     Resets the ranks of a user asynchronously.
    /// </summary>
    /// <param name="authToken">The authentication token of the user whose ranks are to be reset.</param>
    /// <returns>Returns a response with no data from the API service.</returns>
    public async Task<ApiResponseWithNoData> ResetRanks(string authToken)
    {
        return await _apiService.PostAsyncWithNoData("/user-ranks-reset", authToken);
    }
}