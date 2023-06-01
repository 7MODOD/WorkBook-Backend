using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace WorkBook.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {

                    context.Response.Body = responseBody;

                    await _next(context);

                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

                    if (context.Response.ContentType.ToLower().Contains("text/plain"))
                    {
                        // Use the original response body as the data property
                        var jsonResponse = new
                        {
                            status = context.Response.StatusCode,
                            data = responseBodyText
                        };

                        // Serialize the new response object into JSON
                        var jsonResponseText = JsonConvert.SerializeObject(jsonResponse);

                        // Convert the JSON response to bytes
                        var jsonResponseBytes = Encoding.UTF8.GetBytes(jsonResponseText);

                        // Set the Content-Type header
                        context.Response.Headers["Content-Type"] = "application/json";

                        // Set the Content-Length header
                        context.Response.Headers["Content-Length"] = jsonResponseBytes.Length.ToString();

                        // Write the JSON response to the original response body stream
                        await originalBodyStream.WriteAsync(jsonResponseBytes, 0, jsonResponseBytes.Length);
                    }
                    else
                    {
                        // Deserialize the original response body into an object
                        var originalResponse = JsonConvert.DeserializeObject(responseBodyText);

                        // Create a new response object with status and data properties
                        var jsonResponse = new
                        {
                            status = context.Response.StatusCode,
                            data = originalResponse
                        };

                        // Serialize the new response object into JSON
                        var jsonResponseText = JsonConvert.SerializeObject(jsonResponse);

                        // Convert the JSON response to bytes
                        var jsonResponseBytes = Encoding.UTF8.GetBytes(jsonResponseText);

                        // Set the Content-Type header
                        context.Response.Headers["Content-Type"] = "application/json";

                        // Set the Content-Length header
                        context.Response.Headers["Content-Length"] = jsonResponseBytes.Length.ToString();

                        // Write the JSON response to the original response body stream
                        await originalBodyStream.WriteAsync(jsonResponseBytes, 0, jsonResponseBytes.Length);
                    }
                }
            }
            finally
            {
                context.Response.Body = originalBodyStream;

            }
        }
    }
}
