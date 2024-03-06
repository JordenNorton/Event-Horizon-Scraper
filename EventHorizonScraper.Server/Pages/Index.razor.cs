using EventHorizonScraper.Core.Models;
using EventHorizonScraper.Core.Services;
using Microsoft.AspNetCore.Components;

namespace EventHorizonScraper.Server.Pages;

public partial class Index
{
    private List<EventCard>? _events;

    [Inject] public IScrapeService Scraper { get; set; }

    private async Task StartScraping()
    {
        _events = await Scraper.FetchDataAsync();
    }
}