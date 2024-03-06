using EventHorizonScraper.Core.Models;
using HtmlAgilityPack;
using NLog;

namespace EventHorizonScraper.Core.Services;

public class HtmlProcessor
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public List<EventCard> ProcessHtml(string content)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(content);

        //Initialise a list of type "EventCard" (Model)
        var eventCards = new List<EventCard>();

        //Set event card limit
        var cardCount = 6;

        for (var i = 1; i <= cardCount; i++)
        {
            //XPath strings
            var cardLinkXPath =
                $"/html/body/div[2]/div/div[2]/div/div/div/div[1]/div/main/div/div/section[1]/div/section/div/div/section/ul/li[{i}]/div/div[2]/section/div/section[2]/div/a";
            var dateXPath =
                $"/html/body/div[2]/div/div[2]/div/div/div/div[1]/div/main/div/div/section[1]/div/section/div/div/section/ul/li[{i}]/div/div[2]/section/div/section[2]/div/p[1]";
            var organiserXPath =
                $"/html/body/div[2]/div/div[2]/div/div/div/div[1]/div/main/div/div/section[1]/div/section/div/div/section/ul/li[{i}]/div/div[2]/section/div/section[2]/div/p[2]";

            //XPath nodes
            var eventTitles =
                htmlDoc.DocumentNode
                    .SelectNodes(cardLinkXPath); //Gets event title & location (from html tag 'data-event-location')
            var eventDates = htmlDoc.DocumentNode.SelectSingleNode(dateXPath);
            var eventOrganisers = htmlDoc.DocumentNode.SelectSingleNode(organiserXPath);

            if (eventTitles != null)
                try
                {
                    foreach (var eventTitleNode in eventTitles)
                    {
                        //Clean title of unwanted characters
                        string cleanedTitle = CleanText(eventTitleNode.InnerText.Trim());

                        //Assign manipulated event values to strings
                        string eventDate = eventDates.InnerText.Trim();
                        string eventLocation = eventTitleNode.Attributes["data-event-location"].Value;
                        string eventUrl = eventTitleNode.Attributes["href"].Value;
                        string eventOrganiser = eventOrganisers.InnerText.Trim();


                        //Set values using EventCard model
                        var eventCard = new EventCard
                        {
                            Title = cleanedTitle,
                            Date = eventDate,
                            Location = eventLocation,
                            Organiser = eventOrganiser,
                            Url = eventUrl
                        };

                        //append events to the eventCards list
                        eventCards.Add(eventCard);
                    }

                    Logger.Info("Html extracted succesfully");
                }
                catch (Exception e)
                {
                    Logger.Error($"Failed to extract html data: {e.Message}");
                }
        }

        //Allowed characters method - Cleans the event title text of unwanted characters
        string CleanText(string input)
        {
            var allowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz._-& ";

            //returns the cleaned title & changes any escaped &amp's to the & character
            return new string(input.Where(c => allowedChars.Contains(c)).ToArray()).Replace("&amp", "&");
        }


        /* - Testing Start - */

        // Logger.Info("Serializing!");
        // string jsonObject = JsonSerializer.Serialize(eventCards);
        // Logger.Info("Finished serializing");
        //
        // //List output checker!
        // List<EventCard> jsonOutput = JsonSerializer.Deserialize<List<EventCard>>(jsonObject);
        // foreach (EventCard eventCard in jsonOutput)
        // {
        //     Console.WriteLine($"Title: {eventCard.Title} - Date: {eventCard.Date} - Location: {eventCard.Location} - Organiser: {eventCard.Organiser} - Url: {eventCard.Url}");
        // }

        /* - Testing Finish - */
        return eventCards;
    }
}