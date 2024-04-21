using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Models;

/// <summary>
///     Represents an interface containing methods for performing CRUD operations on questions in a repository.
/// </summary>
public interface IQuestionRepository
{
    /// <summary>
    ///     Asynchronously gets a list of all questions represented in the ApiResponse making use of the specified
    ///     authorization token.
    /// </summary>
    /// <param name="authToken">The authentication token to be used.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an ApiResponse object that
    ///     includes a list of questions.
    /// </returns>
    Task<ApiResponse<List<Question>>> GetByAll(string authToken);


    /// <summary>
    ///     Adds a question asynchronously.
    /// </summary>
    /// <param name="question">The question to add.</param>
    /// <param name="authToken">The authentication token.</param>
    /// <returns>
    ///     A Task that represents the asynchronous operation. The task result contains the ApiResponse of the added
    ///     question.
    /// </returns>
    Task<ApiResponse<Question>> AddAsync(Question question, string authToken);

    /// <summary>
    ///     Asynchronously updates a question using provided authToken and returns the response encapsulated in an ApiResponse
    ///     object.
    /// </summary>
    /// <param name="question">The question to update.</param>
    /// <param name="authToken">The authentication token.</param>
    /// <returns>
    ///     A Task that represents the asynchronous operation. The task result contains the ApiResponse object with the
    ///     updated question.
    /// </returns>
    Task<ApiResponse<Question>> UpdateAsync(Question question, string authToken);

    /// <summary>
    ///     Asynchronously deletes a question with the specified ID, using the provided authentication token.
    /// </summary>
    /// <param name="questionId">The ID of the question to delete.</param>
    /// <param name="authToken">The authentication token to use for protected API access.</param>
    /// <returns>
    ///     A task that represents the asynchronous delete operation. The task result is an API response with no
    ///     additional data.
    /// </returns>
    Task<ApiResponseWithNoData> DeleteAsync(int questionId, string authToken);
}