using System;
using System.Xml.Linq;
using AventStack.ExtentReports;
using SampleGetApi.ReportsHelper;
using Xunit.Abstractions;

namespace SampleGetApi.Tests;

public class BaseClass : IDisposable
{
    protected readonly ITestOutputHelper _output;
    protected readonly ExtentTest? _test;
    protected readonly string _currentTestName;
    private static readonly Dictionary<string, TestResult> _testResults = new Dictionary<string, TestResult>();

    // Constructor to initialize the BaseClass
    public BaseClass(ITestOutputHelper output)
    {
        _output = output;
        _currentTestName = output.GetTestMethodName();

        if (_test == null && ReportUtils.ExtentReport !=null)
        {
            _test = ReportUtils.ExtentReport.CreateTest(_currentTestName);
        }
        RecordTestResult(_currentTestName, TestResult.UNKNOWN);
    }

    // IDisposable implementation for cleaning up resources
    public void Dispose()
    {
        // Get the test result for the current test
        TestResult currentTestResult = GetTestResult(_currentTestName);
        if (_test != null)
        {
            // Perform actions based on the test result
            switch (currentTestResult)
            {
                case TestResult.PASS:
                    _test.Pass($"{_currentTestName} Successfully Completed");
                    break;
                case TestResult.FAIL:
                    _test.Fail($"{_currentTestName} Failed to Execute the Test");
                    break;
                case TestResult.UNKNOWN:
                    _output.WriteLine("Test FAILED");
                    _test.Warning($"{_currentTestName} Failed to Execute the Test");
                    break;
                case TestResult.SKIP:
                    _output.WriteLine("Test FAILED");
                    _test.Skip($"{_currentTestName} Failed to Execute the Test");
                    break;
            }
        }
    }

    // Method to record the test result in the dictionary
    protected static void RecordTestResult(string testName, TestResult result)
    {
        _testResults[testName] = result;
    }

    // Method to get the test result from the dictionary
    protected TestResult GetTestResult(string testName)
    {

        return _testResults.GetValueOrDefault(testName);
    }
}

// Enum to represent different test result states
public enum TestResult
{
    PASS,
    FAIL,
    SKIP,
    UNKNOWN
}