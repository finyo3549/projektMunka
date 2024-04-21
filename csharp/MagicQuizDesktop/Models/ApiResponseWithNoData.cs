using Newtonsoft.Json;
using System.Net;

namespace MagicQuizDesktop.Models;

/// <summary>
///     Represents a response from a HTTP request which does not include data in its payload.
///     This response includes status code, a message and a boolean value indicating whether the operation was successful
///     or not.
///     It also includes a helper method that can parse an error message from a json response.
/// </summary>
public class ApiResponseWithNoData
{
    /// <summary>
    ///     Represents the status code of an HTTP response.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     Represents a message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Gets a value indicating whether the operation was successful. The operation is considered successful if the Status
    ///     Code is OK or No Content.
    /// </summary>
    public bool Success => StatusCode == HttpStatusCode.OK || StatusCode == HttpStatusCode.NoContent;

    /// <summary>
    ///     Parses the error message from a JSON response.
    /// </summary>
    /// <param name="jsonResponse">The JSON response to parse.</param>
    /// <returns>The error message, or the original response if an error occurs during parsing.</returns>
    public static string ParseErrorMessage(string jsonResponse)
    {
        try
        {
            var errorObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            return errorObject?.message;
        }
        catch
        {
            return jsonResponse;
        }
    }
}