using System.Configuration;

namespace ZephyrScaleTraceabilityMatrixReport.Helpers
{
    internal static class HelperClass
    {
        public static void ValidateAppConfig()
        {
            List<string> missingValues = new();
            foreach (string entry in ConfigurationManager.AppSettings)
            {
                if (ConfigurationManager.AppSettings[entry]?.Length == 0)
                {
                    missingValues.Add(entry);
                }
            }

            if (missingValues.Count > 0)
            {
                Console.WriteLine($"{String.Join(", ", missingValues)} is missing from App.config. Please provide their values before running again. Exiting application...");
                return;
            }
            else
            {
                Console.WriteLine("App.config is properly filled out. Running application...");
            }
        }
    }
}
