using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ReplaceJakob
{
    class Recognition
    {
        static public bool ImageHasTag(byte[] imageBytes, string tag)
        {
            string subscriptionKey = "81dabbdd41704041bb692a942f1d5041";
            string contentString;
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "visualFeatures=Tags&language=en";

            // Assemble the URI for the REST API Call.
            string uri = "https://northeurope.api.cognitive.microsoft.com/vision/v1.0/analyze" + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            /*
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] imageBytes = binaryReader.ReadBytes((int)fileStream.Length);
            */

            using (ByteArrayContent content = new ByteArrayContent(imageBytes))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = client.PostAsync(uri, content).Result;
                Console.WriteLine(response + "\n");

                // Get the JSON response.
                contentString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(contentString + "\n");
            }
            
            return contentString.Contains(tag);
        }
    }
}
