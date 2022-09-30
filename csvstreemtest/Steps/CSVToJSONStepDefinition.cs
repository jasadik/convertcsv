using RestSharp;
using TechTalk.SpecFlow;

namespace csvstreemtest.Steps
{


    [Binding]
    public sealed class CSVUrlConvertionStepDefinition
    {
        public string Url { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }
        public RestResponse RestResponse { get; set; }

        private const string LocalApi = "https://localhost:7059/Convertor/";
        [Given(@"I Send the params url \(""([^""]*)""\) and type \(""([^""]*)""\)")]

        public void GivenISendTheParamsUrlAndType(string url, string type)
        {

            this.Url = url;
            this.Type = type;

            Console.WriteLine(url + " type : " + type);

        }
        [Given(@"Send the request")]
        public void Send_the_request()
        {
            var client = new RestClient(LocalApi);
            var request = new RestRequest();
            request.AddParameter("csvUri", Url);
            request.AddParameter("type", Type);
            RestResponse = client.ExecuteGet(request);

            //this.Response = restResponse.Content.ToString();
            //Console.WriteLine();

            
        }

        [Then(@"Check The Result return error (.*) And Check The ResultContent is \(""([^""]*)""\)")]
        public void ThenCheckTheResultReturnErrorAndCheckTheResultContentIs(int error, string message)
        {
            Assert.AreEqual(error, (int)RestResponse.StatusCode);
            Assert.AreEqual(message, RestResponse.Content.ToString());
        }


    }
}
