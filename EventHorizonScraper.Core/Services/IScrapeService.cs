using EventHorizonScraper.Core.Models;

namespace EventHorizonScraper.Core.Services;

public interface IScrapeService
{
    Task<List<EventCard>> FetchDataAsync();
}