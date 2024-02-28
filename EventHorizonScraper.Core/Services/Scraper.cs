using NLog;
using HtmlAgilityPack;

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

    public static async Task FetchData()
    {
        // URLs to fetch data from
        string url1 = "https://www.eventbrite.co.uk/d/united-kingdom--essex/tech/";

        // Initiate both requests, but don't wait for them yet
        Task<string?> fetchTask1 = ScrapeAsync(url1);

        // Wait for both requests to complete
        await Task.WhenAll(fetchTask1);

        // Use the data from both requests
        string? result1 = await fetchTask1;

        // Console.WriteLine($"Data from {url1}:\n{result1}\n");

        if (!string.IsNullOrEmpty(result1))
        {
            // Console.WriteLine($"{result1}");
            var htmlProcessor = new HtmlProcessor();
            htmlProcessor.ProcessHtml(result1);
        }
    }

    private static async Task<string?> ScrapeAsync(string url)
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            logger.Info("Http request completed");
            return (responseBody);

        }
        catch (Exception e)
        {
            logger.Error($"{e.Message}");
            return null;
        }
    }
    
    
}