using Newtonsoft.Json.Linq;using RestSharp;using AventStack.ExtentReports;using Xunit.Abstractions;using SampleGetApi.Tests;using AventStack.ExtentReports.MarkupUtils;

namespace SampleGetApi{    // Collection attribute to group related tests    [Collection("SampleApi")]    public class SampleGetApi : BaseClass    {
        // Public reference to RestClient
        public RestClient restClient;

        // Constructor to initialize RestClient at the start of the scripts
        public SampleGetApi(ITestOutputHelper output) : base(output)        {            // Creating a reference to RestClient and setting the Base Url            restClient = new RestClient(new RestClientOptions            {                BaseUrl = new Uri("https://catfact.ninja/")            });
            // Logging test status information
            _test?.Log(Status.Info, "RestClient initialized with base URL: https://catfact.ninja/");        }        [Theory]        [InlineData("1")]        [InlineData("2")]        [InlineData("-3")]        public void GetCatBreeds(string limitNumber)        {            // Creating a RestRequest for getting cat breeds with a limit            var breedRequest = new RestRequest("/breeds");            _test?.Log(Status.Info, "Request path is : " + "/breeds");

            // Adding a parameter to the request for the limit
            _test?.Log(Status.Info, "Sending the Get Parameterise Request Method");
            breedRequest.AddParameter("limit", limitNumber);
            _test?.Log(Status.Pass, "Successfully Added the Parameters");

            // Making a GET request
            var response = restClient.Get(breedRequest);            _test?.Log(Status.Pass, "Successfully sent the Get Request");
            _test?.Info(MarkupHelper.CreateCodeBlock(response.Content, CodeLanguage.Json));            // Parsing and extracting the total count from the response            var totalValue = JObject.Parse(response.Content!)["total"];            // Asserting the total count            Assert.Equal("98", totalValue);            _test?.Log(Status.Pass, "Successfully Asserted the values of Total Key");            RecordTestResult(_currentTestName, TestResult.PASS);                    }    }}