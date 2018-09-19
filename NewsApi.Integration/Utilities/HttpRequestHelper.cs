using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NewsApi.Integration.Response;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;

namespace NewsApi.Integration.Utilities
{
    class HttpRequestHelper
    {
        private NewsApiDriver  NewsApiDriver { get; set; }
        private readonly HttpClient _httpClient;
        public HttpRequestHelper(NewsApiDriver newsApiDriver)
        {
            NewsApiDriver = newsApiDriver;
            _httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = (DecompressionMethods.Deflate | DecompressionMethods.GZip)
            });
            _httpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", NewsApiDriver.ApiKey);
        }

        public async Task<SourceResult> MakeSourceRequest(string endpoint, string querystring)
        {
            SourceResult sourceResult = new SourceResult();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, NewsApiDriver.Url + endpoint + "?" + querystring);
            HttpResponseMessage httpResponseMessage = await this._httpClient.SendAsync(httpRequest);
            HttpResponseMessage httpResponse = httpResponseMessage;
            httpResponseMessage = (HttpResponseMessage)null;
            string str = await httpResponse.Content?.ReadAsStringAsync();
            string json = str;
            str = (string)null;
            if (!string.IsNullOrWhiteSpace(json))
            {
                ApiSourceResponse apiResponse = JsonConvert.DeserializeObject<ApiSourceResponse>(json);
                sourceResult.Status = apiResponse.Status;
                if (sourceResult.Status == Statuses.Ok)
                {
                    sourceResult.TotalResults = apiResponse.Sources.Count;
                    sourceResult.SourceResponses = apiResponse.Sources;
                }
                else
                {
                    ErrorCodes errorCode = ErrorCodes.UnknownError;
                    try
                    {
                        errorCode = apiResponse.Code.Value;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("The API returned an error code that wasn't expected: " + (object)apiResponse.Code);
                    }
                    sourceResult.Error = new Error()
                    {
                        Code = errorCode,
                        Message = apiResponse.Message
                    };
                }
                apiResponse = (ApiSourceResponse)null;
            }
            else
            {
                sourceResult.Status = Statuses.Error;
                sourceResult.Error = new Error()
                {
                    Code = ErrorCodes.UnexpectedError,
                    Message = "The API returned an empty response. Are you connected to the internet?"
                };
            }
            return sourceResult;
        }
    }
}
