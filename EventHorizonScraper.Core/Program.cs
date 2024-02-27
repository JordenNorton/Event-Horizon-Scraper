using EventHorizonScraper.Core.Services;
using NLog;

namespace EventHorizonScraper.Core
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly MenuHandler MenuHandler = new MenuHandler();
        
        static void Main(string[] args)
        {
            
            Logger.Info("Application is starting...");
            
            MenuHandler.DisplayMenu();
            
            Logger.Warn("Application is ending...");
        }
    }
}