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
            if(Type=="JSON")
            {
                request.AddHeader("Content-Type", "application/json");
            }
            else
            {
                request.AddHeader("Content-Type", "application/xml");
            }
            RestResponse = client.ExecuteGet(request);

            //this.Response = restResponse.Content.ToString();
            //Console.WriteLine();

            
        }


        [Then(@"Check The Result return error (.*) And Check The ResultContent is :(.*)")]
        public void ThenCheckTheResultReturnErrorAndCheckTheResultContentIs(int error, string ResultContent)
        {
            Assert.That(error, Is.EqualTo((int)RestResponse.StatusCode));
            Assert.That(ResultContent, Is.EqualTo(RestResponse?.Content?.ToString()));
        }
        [Then(@"Check The Result return (.*) And Check The ResultContent is ValidJSON")]
        public void ThenCheckTheResultReturnAndCheckTheResultContentIsValidJSON(int p0)
        {
            Assert.That(p0, Is.EqualTo((int)RestResponse.StatusCode));
            Assert.IsTrue(fileverification.IsJsonValid(RestResponse?.Content?.ToString()));
        }

        [Then(@"Check The Result return (.*) And Check The ResultContent is ValidXML")]
        public void ThenCheckTheResultReturnAndCheckTheResultContentIsValidXML(int p0)
        {
            Assert.That(p0, Is.EqualTo((int)RestResponse.StatusCode));
            Assert.IsTrue(fileverification.IsValidateXML(RestResponse?.Content?.ToString()));

        }




    }
}
