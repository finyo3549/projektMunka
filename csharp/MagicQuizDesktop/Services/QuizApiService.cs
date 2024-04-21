using MagicQuizDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Services;

///<summary>
/// Represents a service for making HTTP requests to a quiz API.
/// This service supports basic CRUD operations (CREATE, READ, UPDATE, DELETE),
/// handles exceptions and responses, and manages the authorization.
/// Includes methods for handling HTTP POST, GET, PUT, DELETE operations, as well as handling exceptions.
/// </summary>
public class QuizApiService
{
    /// <summary>
    ///     The base URL for API calls.
    /// </summary>
    private readonly string _baseUrl = "http://127.0.0.1:8000/api";

    private readonly HttpClient _httpClient;

    /// <summary>
    ///     Initializes a new instance of the QuizApiService class and instantiates the HttpClient object.
    /// </summary>
    public QuizApiService()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    ///     Handles exceptions by creating and returning an ApiResponse with Success set to false, Message set to the exception
    ///     message, and Status set to InternalServerError.
    /// </summary>
    /// <typeparam name="T">The type parameter of the ApiResponse.</typeparam>
    /// <param name="ex">The exception to handle.</param>
    /// <returns>
    ///     A new ApiResponse with Success set to false, Message containing the exception message, and StatusCode set to
    ///     InternalServerError.
    /// </returns>
    private ApiResponse<T> HandleException<T>(Exception ex)
    {
        Debug.WriteLine("Exception caught: " + ex.Message);
        return new ApiResponse<T>
        {
            Success = false,
            Message = "Error: " + ex.Message,
            StatusCode = HttpStatusCode.InternalServerError
        };
    }


    /// <summary>
    ///     Handles exceptions with no data and logs the exception message.
    ///     Returns an ApiResponseWithNoData with the error message and status code as Internal Server Error.
    /// </summary>
    /// <param name="ex">The exception caught.</param>
    /// <returns>Returns an instance of ApiResponseWithNoData with the error information.</returns>
    private ApiResponseWithNoData HandleExceptionWithNoData(Exception ex)
    {
        Debug.WriteLine("Exception caught: " + ex.Message);
        return new ApiResponseWithNoData
        {
            Message = "Error: " + ex.Message,
            StatusCode = HttpStatusCode.InternalServerError
        };
    }


    /// <summary>
    ///     Performs an asynchronous HTTP POST request to the specified URI with the provided data.
    ///     An optional authToken can be used for authenticated requests.
    ///     The returned ApiResponse object includes status code, message, success and data of type T.
    /// </summary>
    /// <param name="uri">The URI for the HTTP request.</param>
    /// <param name="data">The data to be sent in the HTTP POST request.</param>
    /// <param name="authToken">(Optional) The authorization token for authentication.</param>
    /// <returns>
    ///     A Task resulting in an ApiResponse object, containing response status code, message, success, and the response
    ///     data of type T.
    /// </returns>
    public async Task<ApiResponse<T>> PostAsync<T>(string uri, object data, string authToken = "")
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}{uri}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Message = ApiResponse<T>.ParseErrorMessage(jsonResponse),
                Success = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Received response: " + jsonResponse);
                apiResponse.Data = JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            else
            {
                Debug.WriteLine($"Failed POST request: {response.StatusCode}\n{jsonResponse}");
            }

            return apiResponse;
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }


    /// <summary>
    ///     Asynchronously sends a POST request to the specified URI with an authentication token.
    ///     This method does not expect any data in the response body.
    /// </summary>
    /// <param name="uri">The URI to which the POST request will be sent.</param>
    /// <param name="authToken">The authentication token to be included in the request header.</param>
    /// <returns>
    ///     If the request is successful, returns an ApiResponseWithNoData object containing the status code and parsed error
    ///     message.
    ///     In case of an exception, it handles the error and returns an ApiResponseWithNoData object.
    /// </returns>
    public async Task<ApiResponseWithNoData> PostAsyncWithNoData(string uri, string authToken)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}{uri}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var apiResponse = new ApiResponseWithNoData
            {
                StatusCode = response.StatusCode,
                Message = ApiResponseWithNoData.ParseErrorMessage(jsonResponse)
            };

            if (response.IsSuccessStatusCode)
                Debug.WriteLine("Received response: " + jsonResponse);
            else
                Debug.WriteLine($"Failed POST request: {response.StatusCode}\n{jsonResponse}");

            return apiResponse;
        }
        catch (Exception ex)
        {
            return HandleExceptionWithNoData(ex);
        }
    }

    /// <summary>
    ///     Sends an asynchronous GET request to the specified URI with the given authentication token.
    ///     This method expects to receive a JSON payload in response which it will attempt to deserialize into an instance of
    ///     type <typeparamref name="T" />.
    ///     In addition to the deserialized data, the method will also return an ApiResponse object containing HTTP status
    ///     code, flags indicating success or failure, as well as relevant error messages, if any.
    /// </summary>
    /// <param name="uri">The endpoint to which the GET request will be sent.</param>
    /// <param name="authToken">The authentication token to be included in the request headers.</param>
    /// <typeparam name="T">The type into which the JSON response will be deserialized.</typeparam>
    /// <returns>
    ///     An ApiResponse object containing the HTTP status code, flags indicating success or failure, a relevant error
    ///     message, if any, and the deserialized data.
    /// </returns>
    /// <exception cref="JsonReaderException">Thrown when there is an error reading the returned JSON.</exception>
    /// <exception cref="Exception">Thrown when a general error occurs in deserialization.</exception>
    public async Task<ApiResponse<T>> GetAsync<T>(string uri, string authToken)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var response = await _httpClient.GetAsync($"{_baseUrl}{uri}");
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Message = ApiResponse<T>.ParseErrorMessage(jsonResponse),
                Success = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Received GET response: " + jsonResponse);
                try
                {
                    apiResponse.Data = JsonConvert.DeserializeObject<T>(jsonResponse);
                }
                catch (JsonReaderException jex)
                {
                    Debug.WriteLine("JSON reading error in GET: " + jex.Message);
                    apiResponse.Message = "JSON reading error: " + jex.Message;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("General deserialization error in GET: " + ex.Message);
                    apiResponse.Message = "General deserialization error: " + ex.Message;
                }
            }
            else
            {
                Debug.WriteLine($"Failed GET request: {response.StatusCode}\n{jsonResponse}");
            }

            return apiResponse;
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }

    /// <summary>
    ///     Sends a PUT request asynchronously to a specified URI as an asynchronous operation.
    /// </summary>
    /// <typeparam name="T">The type of the object to send.</typeparam>
    /// <param name="uri">The Uniform Resource Identifier (URI) of the web resource to which the data will be sent.</param>
    /// <param name="data">The object to be sent to the target URI.</param>
    /// <param name="authToken">The authentication token to use in the request.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation, which wraps the HTTP response from the server.
    ///     The <see cref="Task{TResult}.Result" /> property returns an ApiResponse object that contains the server response.
    /// </returns>
    public async Task<ApiResponse<T>> PutAsync<T>(string uri, object data, string authToken)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}{uri}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Message = ApiResponse<T>.ParseErrorMessage(jsonResponse),
                Success = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Received response from PUT: " + jsonResponse);
                apiResponse.Data = JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            else
            {
                Debug.WriteLine($"Failed PUT request: {response.StatusCode}\n{jsonResponse}");
            }

            return apiResponse;
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }


    /// <summary>
    ///     Performs an asynchronous DELETE request to a specified URI with an authentication token and returns the result.
    /// </summary>
    /// <param name="uri">The Uniform Resource Identifier of the server endpoint.</param>
    /// <param name="authToken">The authentication token for the request.</param>
    /// <returns>ApiResponse which contains the Status Code and Message without Data payload if any.</returns>
    public async Task<ApiResponseWithNoData> DeleteAsync(string uri, string authToken)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var response = await _httpClient.DeleteAsync($"{_baseUrl}{uri}");
            var responseContent = await response.Content.ReadAsStringAsync();

            var apiResponse = new ApiResponseWithNoData
            {
                StatusCode = response.StatusCode,
                Message = ApiResponseWithNoData.ParseErrorMessage(responseContent)
            };

            if (response.IsSuccessStatusCode)
                Debug.WriteLine("Received response: " + responseContent);
            else
                Debug.WriteLine($"Failed POST request: {response.StatusCode}\n{responseContent}");

            return apiResponse;
        }
        catch (Exception ex)
        {
            return HandleExceptionWithNoData(ex);
        }
    }
}