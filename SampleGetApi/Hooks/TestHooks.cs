using System;
using SampleGetApi.ReportsHelper;

namespace SampleGetApi.Hooks
{
	public class TestHooks : IAsyncLifetime 
	{
        public Task InitializeAsync()
        {
            ReportUtils.Initalize();
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            ReportUtils.CloseReport();
            return Task.CompletedTask;
        }
    }
}

