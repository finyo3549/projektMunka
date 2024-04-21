using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Repositories;

/// <summary>
///     The TopicRepository class provides methods for interacting with Topics in a quiz application.
///     It uses REST API requests to get, add, update, and delete topics.
/// </summary>
public class TopicRepository : ITopicRepository
{
    /// <summary>
    ///     The API service.
    /// </summary>
    private readonly QuizApiService _apiService;

    /// <summary>
    ///     Initializes a new instance of the TopicRepository class.
    /// </summary>
    public TopicRepository()
    {
        _apiService = new QuizApiService();
    }

    /// <summary>
    ///     Asynchronously retrieves a list of topics from the API service.
    /// </summary>
    /// <param name="authToken">A string representing the authorization token.</param>
    /// <returns>A task representing the asynchronous operation, containing an ApiResponse of a list of topics.</returns>
    public async Task<ApiResponse<List<Topic>>> GetByAll(string authToken)
    {
        return await _apiService.GetAsync<List<Topic>>("/topics", authToken);
    }

    /// <summary>
    ///     Asynchronously adds a new topic to the API service.
    /// </summary>
    /// <param name="topic">The topic to be added.</param>
    /// <param name="authToken">The authentication token to use for the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is an ApiResponse of the Topic.</returns>
    public async Task<ApiResponse<Topic>> AddAsync(Topic topic, string authToken)
    {
        return await _apiService.PostAsync<Topic>("/topics", topic, authToken);
    }


    /// <summary>
    ///     Asynchronously updates an existing topic using the provided authorization token.
    /// </summary>
    /// <param name="topic">The topic to be updated.</param>
    /// <param name="authToken">The authorization token used for authentication.</param>
    /// <returns>A task that represents the asynchronous operation, containing an ApiResponse with the updated topic.</returns>
    public async Task<ApiResponse<Topic>> UpdateAsync(Topic topic, string authToken)
    {
        return await _apiService.PutAsync<Topic>($"/topics/{topic.Id}", topic, authToken);
    }

    /// <summary>
    ///     Asynchronously deletes a topic identified by the given topic ID using an specific authentication token.
    /// </summary>
    /// <param name="topicId">The ID of the topic to delete.</param>
    /// <param name="authToken">The authentication token used to authorize the request.</param>
    /// <returns>A response from the API with no data.</returns>
    public async Task<ApiResponseWithNoData> DeleteAsync(int topicId, string authToken)
    {
        return await _apiService.DeleteAsync($"/topics/{topicId}", authToken);
    }
}