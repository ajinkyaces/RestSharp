using System;
using System.Xml.Linq;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SampleGetApi.ReportsHelper;

	public class ReportUtils
	{
    public static ExtentReports? ExtentReport;

    public static void Initalize()
		{
        var reportFolder = CreateReportFolder();

        if (reportFolder != null || reportFolder != "")
        {
            var indexPath = Path.Combine(reportFolder, "index.html");

            var spark = new ExtentSparkReporter(indexPath);
            ExtentReport = new ExtentReports();
            ExtentReport.AttachReporter(spark);
            //Extent.AddSystemInfo("Host Name", System.Net.Dns.GetHostName());
            //Extent.AddSystemInfo("Environment", "QA");
            //Extent.AddSystemInfo("User Name", Environment.UserName);
        }
        else
        {
            Console.WriteLine("Error creating report folder.");
        }
    }

    public static void CloseReport()
		{
        if (ExtentReport != null)
        {
            ExtentReport.Flush();
        }
        else
        {
            Console.WriteLine("Extent object is null. Make sure to call Initalize method first.");
        }
    }

    private static string CreateReportFolder()
    {
        try
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = GetProjectDirectory(currentDirectory);
            var reportFolderName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            var reportFolderPath = Path.Combine(projectDirectory, "ExtentReport", reportFolderName);

            if (!Directory.Exists(reportFolderPath))
            {
                Directory.CreateDirectory(reportFolderPath);
                Console.WriteLine($"Report folder created: {reportFolderPath}");
                return reportFolderPath;
            }
            else
            {
                Console.WriteLine("Report folder already exists.");
                return ""; // or return existing folder path if needed
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating report folder: {ex.Message}");
            return "";
        }
    }

    public static string GetProjectDirectory(string currentDirectory)
    {
        DirectoryInfo directory = new DirectoryInfo(currentDirectory);

        while (directory != null && directory.Name != "SampleGetApi")
        {
            directory = directory.Parent;
        }

        return directory?.FullName;
    }
}

