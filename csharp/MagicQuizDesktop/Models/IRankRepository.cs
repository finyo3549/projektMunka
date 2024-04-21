using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Models;

/// <summary>
///     Repository interface for Rank related CRUD operations.
/// </summary>
public interface IRankRepository
{
    /// <summary>
    ///     Puts the user's score.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="rank">The user's rank.</param>
    /// <param name="authToken">The user's authentication token.</param>
    /// <returns>Returns the API response containing the user's rank.</returns>
    Task<ApiResponse<Rank>> PutScore(int userId, Rank rank, string authToken);

    /// <summary>
    ///     Asynchronously retrieves a list of 'Rank' objects from an API.
    /// </summary>
    /// <param name="authToken">The authentication token for the API.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an ApiResponse object that holds
    ///     the list of 'Rank' objects.
    /// </returns>
    Task<ApiResponse<List<Rank>>> GetRanks(string authToken);

    /// <summary>
    ///     Asynchronously gets the score for a specific user.
    /// </summary>
    /// <param name="userId">The Id of the user.</param>
    /// <param name="authToken">The user's authorization token for authentication and authorization purposes.</param>
    /// <returns>A Task yielding an ApiResponse of Rank type containing user's score details.</returns>
    Task<ApiResponse<Rank>> GetScore(int userId, string authToken);

    /// <summary>
    ///     Resets the ranks by using the specified authentication token.
    /// </summary>
    /// <param name="authToken">The authentication token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an API response without data.</returns>
    Task<ApiResponseWithNoData> ResetRanks(string authToken);
}