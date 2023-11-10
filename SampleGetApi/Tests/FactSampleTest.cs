using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit.Abstractions;

namespace SampleGetApi.Tests
{
    // Collection attribute to group related tests
    [Collection("SampleApi")]
    public class FactSampleTest : BaseClass
    {
        // Public reference to RestClient
        public RestClient restClient;

        // Constructor to initialize RestClient at the start of the scripts
        public FactSampleTest(ITestOutputHelper output) : base(output)
        {
            // Creating a reference to RestClient and setting the Base Url
            restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://api.publicapis.org/")
            });

            // Logging test status information
            _test?.Log(Status.Info, "RestClient initialized with base URL: https://api.publicapis.org/");
        }

        [Fact]
        public void GetEntries()
        {

            // Creating a RestRequest for getting entries
            var breedRequest = new RestRequest("/entries");
            _test?.Log(Status.Info, "Request path is : "+ ("/entries"));

            // Making a GET request
            _test?.Log(Status.Info, "Sending the Get Request Method");
            var response = restClient.Get(breedRequest);
            _test?.Log(Status.Pass, "Successfully sent the Get Request");
            _test?.Info(MarkupHelper.CreateCodeBlock(response.Content, CodeLanguage.Json));

            // Parsing and extracting the total count from the response
            var totalValue = JObject.Parse(response.Content!)["count"];
            _test?.Log(Status.Pass, "Successfully sent the Request");

            // Asserting the total count
            Assert.Equal("1427", totalValue);
            _test?.Log(Status.Pass, "Validated the Totat Value Count");
            RecordTestResult(_currentTestName, TestResult.PASS);
        }


        [Fact]
        public void RandomEnteries()
        {

            // Creating a RestRequest for getting random entries
            var breedRequest = new RestRequest("/random");
            _test?.Log(Status.Info, "Request path is : " + ("/random"));

            // Making a GET request
            _test?.Log(Status.Info, "Sending the Get Request Method");
            var response = restClient.Get(breedRequest);
            _test?.Log(Status.Pass, "Successfully sent the Get Request");
            _test?.Info(MarkupHelper.CreateCodeBlock(response.Content, CodeLanguage.Json));

            // Parsing and extracting the total count from the response
            var totalValue = JObject.Parse(response.Content!)["count"];
            _test?.Log(Status.Pass, "Successfully sent the Request");

            // Asserting the total count
            Assert.Equal("98", totalValue);
            _test?.Log(Status.Pass, "Validated the Totat Value Count");
            RecordTestResult(_currentTestName, TestResult.PASS);
        }
    }
}