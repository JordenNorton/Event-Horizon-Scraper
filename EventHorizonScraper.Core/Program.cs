using NLog;

namespace EventHorizonScraper.Core
{
    public static class Program
    {
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