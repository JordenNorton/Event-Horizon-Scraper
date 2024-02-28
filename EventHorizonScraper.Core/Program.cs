using EventHorizonScraper.Core.Services;
using NLog;

namespace EventHorizonScraper.Core
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly MenuHandler MenuHandler = new MenuHandler();

        static async Task Main(string[] args)
        {

            Logger.Info("Application is starting...");

            await MenuHandler.DisplayMenu();

            Logger.Info("Application is ending...");
        }
    }
}