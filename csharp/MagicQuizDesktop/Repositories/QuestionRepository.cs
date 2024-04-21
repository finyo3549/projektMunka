using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Repositories;

/// <summary>
///     Represents a repository for managing questions in a quiz. This class performs API calls for CRUD operations on the
///     question data.
/// </summary>
public class QuestionRepository : IQuestionRepository
{
    /// <summary>
    ///     The API service.
    /// </summary>
    private readonly QuizApiService _apiService;

    /// <summary>
    ///     Initializes a new instance of the QuestionRepository class.
    /// </summary>
    public QuestionRepository()
    {
        _apiService = new QuizApiService();
    }

    /// <summary>
    ///     Asynchronously retrieves a list of all questions from the API.
    /// </summary>
    /// <param name="authToken">The authentication token to be used in the API call.</param>
    /// <returns>A task resulting in an API Response containing a list of questions.</returns>
    public async Task<ApiResponse<List<Question>>> GetByAll(string authToken)
    {
        return await _apiService.GetAsync<List<Question>>("/questions", authToken);
    }


    /// <summary>
    ///     Adds a new question to the API asynchronously.
    /// </summary>
    /// <param name="question">The question object to be added.</param>
    /// <param name="authToken">The authentication token to be used in the API call.</param>
    /// <returns>A task resulting in an API Response containing the added question data.</returns>
    public async Task<ApiResponse<Question>> AddAsync(Question question, string authToken)
    {
        return await _apiService.PostAsync<Question>("/questions", question, authToken);
    }


    /// <summary>
    ///     Updates an existing question in the API asynchronously.
    /// </summary>
    /// <param name="question">The question object to be updated.</param>
    /// <param name="authToken">The authentication token to be used in the API call.</param>
    /// <returns>A task resulting in an API Response containing the updated question data.</returns>
    public async Task<ApiResponse<Question>> UpdateAsync(Question question, string authToken)
    {
        return await _apiService.PutAsync<Question>($"/questions/{question.Id}", question, authToken);
    }


    /// <summary>
    ///     Deletes a specific question from the API asynchronously.
    /// </summary>
    /// <param name="questionId">The question Id to be deleted.</param>
    /// <param name="authToken">The authentication token to be used in the API call.</param>
    /// <returns>A task resulting in an API Response which does not contain any data.</returns>
    public async Task<ApiResponseWithNoData> DeleteAsync(int questionId, string authToken)
    {
        return await _apiService.DeleteAsync($"/questions/{questionId}", authToken);
    }


    /// <summary>
    ///     Retrieves a question by its Id from the API asynchronously.
    /// </summary>
    /// <param name="questionId">The question Id to be retrieved.</param>
    /// <param name="authToken">The authentication token to be used in the API call.</param>
    /// <returns>A task resulting in an API Response containing the question data.</returns>
    public async Task<ApiResponse<Question>> GetByIdAsync(int questionId, string authToken)
    {
        return await _apiService.GetAsync<Question>($"/questions/{questionId}", authToken);
    }
}