using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace MagicQuizDesktop.Models
{
    /// <summary>
    /// Defines a standardized response used in API operations. 
    /// It contains information about the success of the request, HTTP status code, message and optional data of generic type.
    /// </summary>
    /// <typeparam name="T">The type of data returned in the API response, if any.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Represents the status code of an HTTP response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        ///  Represents the message of an HTTP response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Represents the data of generic type T.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Parses the error message from a JSON response. If the response contains a "message" key, returns its value.
        /// If the JSON response cannot be deserialized, logs the issue and returns a standardized error message.
        /// If no specific message is found, returns the original JSON response.
        /// </summary>
        /// <param name="jsonResponse">The JSON response to parse.</param>
        /// <returns>The parsed error message, or a standardized error message if parsing fails.</returns>
        public static string ParseErrorMessage(string jsonResponse)
        {
            try
            {
                var messageObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
                if (messageObject.ContainsKey("message"))
                {
                    return messageObject["message"].ToString();
                }
            }
            catch (JsonException ex)
            {
                Debug.WriteLine("Hiba a JSON válasz deszerializálásakor: " + ex.Message);
                return "A válasz formátuma nem megfelelő.";
            }
            return jsonResponse;
        }
    }

}
