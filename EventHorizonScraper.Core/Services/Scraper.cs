using NLog;

namespace EventHorizonScraper.Core.Services;

public static class Scraper
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private static readonly HttpClient client = new HttpClient();

    static Scraper()
    {
        // Set a user-agent to mimic a web browser
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
    }
    
    public static async Task ScrapeAsync()
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync("https://www.meetup.com/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            logger.Info("Http request completed");

        }
        catch (Exception e)
        {
            logger.Error($"{e.Message}");
        }
    }
    
    
}