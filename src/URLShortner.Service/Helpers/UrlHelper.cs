using System;
using System.Net;
using System.Text;
using static URLShortner.Service.Infrastructure.Constants;

namespace URLShortner.Service.Helpers
{
    /// <summary>
    /// Helpers for processing Url.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Check that provided Url is valid HTTP Url.
        /// </summary>
        /// <param name="url">Provided Url.</param>
        /// <returns>True or False (depends on  contains or not HTTP).</returns>
        public static bool SupportsHTTProtocol(string url)
        {
            url = url.ToLower();

            return url.Length > 5 &&
                   (url.StartsWith(Protocol.HTTP) || url.StartsWith(Protocol.HTTPS));
        }

        /// <summary>
        /// Check that resource is exist by sending request.
        /// </summary>
        /// <returns>True or False.</returns>
        public static bool CheckUrlExists(this string url)
        {
            if (!SupportsHTTProtocol(url))
                url = Protocol.HTTPS + url;

            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                var response = request.GetResponse() as HttpWebResponse;

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                // Any exception will returns false.
                return false;
            }
        }

        /// <summary>
        /// Generate short Url for provided Url.
        /// </summary>
        /// <returns>Short Url.</returns>
        public static string GenerateShortUrl()
        {
            var builder = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < 6; i++)
            {
                var randomNumber = random.Next(0, 35);
                randomNumber += randomNumber < 10 
                    ? ASCIITable.ADDITIVE_GET_NUMBER 
                    : ASCIITable.ADDITIVE_GET_CHARACTER;

                builder.Append(char.ConvertFromUtf32(randomNumber));
            }

            return builder.ToString();
        }
    }
}
