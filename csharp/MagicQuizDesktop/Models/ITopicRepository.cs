using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Models;

/// <summary>
///     Repository interface for Topic related CRUD operations.
/// </summary>
public interface ITopicRepository
{
    /// <summary>
    ///     Asynchronously gets all topics wrapped in an API response using the provided authentication token.
    /// </summary>
    /// <param name="authToken">The authentication token to use for the API call.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the API response that wraps the
    ///     list of topics.
    /// </returns>
    Task<ApiResponse<List<Topic>>> GetByAll(string authToken);

    /// <summary>
    ///     Asynchronously adds a new topic with the provided authentication token.
    /// </summary>
    /// <param name="topic">The topic to add.</param>
    /// <param name="authToken">The authentication token used in the request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ApiResponse of Topic.</returns>
    Task<ApiResponse<Topic>> AddAsync(Topic topic, string authToken);

    /// <summary>
    ///     Asynchronously updates a topic.
    /// </summary>
    /// <param name="topic">The topic to update.</param>
    /// <param name="authToken">The authentication token.</param>
    /// <returns>
    ///     A task that represents the asynchronous update operation. The task result contains the API response with the
    ///     updated topic.
    /// </returns>
    Task<ApiResponse<Topic>> UpdateAsync(Topic topic, string authToken);

    /// <summary>
    ///     Asynchronously deletes a topic with the given id using the provided authentication token.
    /// </summary>
    /// <param name="topicId">The identifier of the topic to be deleted.</param>
    /// <param name="authToken">The authentication token to validate the request.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an ApiResponseWithNoData
    ///     representing the response without any data from the server.
    /// </returns>
    Task<ApiResponseWithNoData> DeleteAsync(int topicId, string authToken);
}