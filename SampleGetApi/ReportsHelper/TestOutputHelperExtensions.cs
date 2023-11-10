using System;
using System.Reflection;
using Xunit.Abstractions;

namespace SampleGetApi.ReportsHelper
{
    public static class TestOutputHelperExtensions
    {
        public static string GetTestMethodName(this ITestOutputHelper output)
        {
            var test = output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(output) as ITest;

            // Extracting the class and method name from DisplayName
            var displayNameParts = test?.DisplayName?.Split('.') ?? Array.Empty<string>();
            var className = displayNameParts.Length >= 2 ? displayNameParts[0] : "UnknownClass";
            var methodName = displayNameParts.Length >= 3 ? displayNameParts[2].Split('(')[0] : "UnknownMethod";

            // Combine class and method names
            return $"{className}.{methodName}";
        }

        public static string GetTestName(this ITestOutputHelper output)
        {
            var test = output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(output) as ITest;

            // Extracting the method name from DisplayName
            var testName = test?.DisplayName?.Split('.')?[2].Split('(')[0] ?? "UnknownMethod";

            return testName;
        }
    }

}