using NLog;

namespace EventHorizonScraper.Core.Services;

public class MenuHandler
{
    public async Task DisplayMenu()
    {
        var logger = LogManager.GetCurrentClassLogger();
        var continueRunning = true;
        var scraper = new Scraper();
          
        while (continueRunning)
        {
            try
            {
                Console.WriteLine("Ready to start scraping? \n Y or N");
                string? startScraping = Console.ReadLine()?.ToLower();
                switch (startScraping)
                {
                    case "y":
                        logger.Info("Scraping: initiated");
                        Console.WriteLine("Scraping...");
                        await scraper.FetchDataAsync();
                        Console.ReadLine();
                        break;
                        
                    case "n":
                        logger.Info("Quit: startScraping - User entered 'n'");
                        Console.WriteLine("OK, come back when you are ready!");
                        continueRunning = false;
                        break;
                        
                    default:
                        logger.Info($"Invalid input: startScraping - user entered '{startScraping}'");
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "An unexpected error occurred.");
                continueRunning = false;
            }
        }
    }
}