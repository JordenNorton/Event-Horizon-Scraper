using System.Text;
using System.Text.Json;
using HtmlAgilityPack;
using NLog;

namespace EventHorizonScraper.Core.Services;

public class HtmlProcessor
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    public void ProcessHtml(string content)
    {
        
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(content);

        List<string> titles = new List<string>();
        int cardCount = 5;
        
        for (int i = 1; i <= cardCount; i++)
        {
            // Construct the XPath query dynamically to iterate through cards
            string xpath = $"/html/body/div[2]/div/div[2]/div/div/div/div[1]/div/main/div/div/section[1]/div/section/div/div/section/ul/li[{i}]/div/div[2]/section/div/section[2]/div/a/h2";;

            // Select nodes based on the constructed XPath
            var eventTitles = htmlDoc.DocumentNode.SelectNodes(xpath);
            
            if (eventTitles != null)
            {
                foreach (var eventTitleNode in eventTitles)
                {
                    StringBuilder sb = new StringBuilder();
                    string eventTitleText = eventTitleNode.InnerText.Trim();
        
                    for (int e = 0; e < eventTitleText.Length; e++)
                    {
                        char c = eventTitleText[e];
            
                        if (c == ' ' || (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == '-' || c == '&')
                        {
                            sb.Append(c);
                        }
                    }
                    
                    string cleanedTitle = sb.ToString();
                    cleanedTitle = cleanedTitle.Replace("&amp", "&");
                    
                    titles.Add(cleanedTitle);
                }
            }
        }
        
        Logger.Info("Serializing!");
        string jsonObject = JsonSerializer.Serialize(titles);
        Logger.Info("Finished serializing");
        
        Console.WriteLine(jsonObject);

        
        //List output checker!
        List<string> jsonOutput = JsonSerializer.Deserialize<List<string>>(jsonObject);
        foreach (string title in jsonOutput)
        {
            Console.WriteLine(title);           
        }

    }
}