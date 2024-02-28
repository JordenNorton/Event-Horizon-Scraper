using HtmlAgilityPack;
using AngleSharp;
using AngleSharp.Dom;

namespace EventHorizonScraper.Core.Services;

public class HtmlProcessor
{
    public async Task ProcessHtml(string content)
    {
        
        IConfiguration config = Configuration.Default;
        IBrowsingContext context = BrowsingContext.New(config);

        IDocument document = await context.OpenAsync(req => req.Content(content));

        var eventCards = document.QuerySelectorAll(
            ".discover-search-desktop-card");

        List<string> titles = new List<string>();
        
        foreach (var eventCard in eventCards)
        {
            titles.Add(eventCard.TextContent.Trim());
        }

        foreach (var title in titles)
        {
            Console.WriteLine(title);
        }
        
        
        
    }
}