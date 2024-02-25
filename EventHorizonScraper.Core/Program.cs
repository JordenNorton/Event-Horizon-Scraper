using NLog;

namespace EventHorizonScraper.Core // Replace "YourNamespace" with the actual namespace of your project
{
    public static class Program
    {
        // Define the logger for the entire Program class
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Info("Application is starting...");

            try
            {
                
            }
            catch (Exception e)
            {
                Logger.Error(e, "An unexpected error occurred.");
            }

            Logger.Warn("Application is ending...");
        }
    }
}