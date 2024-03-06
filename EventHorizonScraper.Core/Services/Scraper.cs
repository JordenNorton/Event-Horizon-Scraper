using EventHorizonScraper.Core.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventHorizonScraper.Core.Services;

public class Scraper : IScrapeService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private static readonly HttpClient Client = new HttpClient();

    static Scraper()
    {
        // Set a user-agent to mimic a web browser
        Client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
    }

    public async Task<List<EventCard>> FetchDataAsync()
    {
        string url = "https://www.eventbrite.co.uk/d/united-kingdom--basildon/tech/";
        string? result = await ScrapeAsync(url);

        if (string.IsNullOrEmpty(result))
        {
            Logger.Error("Failed to fetch or no data returned from URL.");
            return new List<EventCard>();
        }

        var htmlProcessor = new HtmlProcessor();
        return htmlProcessor.ProcessHtml(result);
    }

    private static async Task<string?> ScrapeAsync(string url)
    {
        try
        {
            HttpResponseMessage response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Logger.Info("HTTP request completed successfully.");
            return responseBody;
        }
        catch (Exception e)
        {
            Logger.Error($"Error during scraping: {e.Message}");
            return null;
        }
    }
}