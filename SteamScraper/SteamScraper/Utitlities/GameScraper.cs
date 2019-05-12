using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamScraper.Utilities
{
    public class GameScraper
    {
        internal string _MainImageXpath = "//img[@class='game_header_image_full']";
        //internal string _MainImageXpath = "/html[1]/body[1]/div[1]/div[7]/div[3]/div[1]/div[2]/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]/img[1]";
        internal string _TitleXpath = "//div[@class='apphub_AppName']";
        internal string _ShortDescriptionXPath = "//div[@class='game_description_snippet']";
        internal string _UserDefinedGenre = "//a[@class='app_tag']";
        internal string _DeveloperXPath = "//div[@class='dev_row']//b";
        internal string _PublisherXPath = "//div[@class='details_block']//div[2]//a";
        internal string _ReleaseDateXPath = "//div[@class='date']";
        internal string _IconsXPath = "//a[@class='name']";
        internal string _LongDescription = "//div[@id='game_area_description']";
        internal string _Highlightreel = "//a[@class='highlight_screenshot_link']";
        internal string _Genre = "//div[@class  = 'details_block']//a";
        public List<ScrapedGame> ScrapeGames(List<string> url)
        {
            var scraper = new ScraperModels.Scraper();
            var ListOfScrapedGames = new List<ScrapedGame>();
            int success = 0;
            int fail = 0;
            Parallel.ForEach(
                url,
                new ParallelOptions { MaxDegreeOfParallelism = 20 },
                links =>
                {
                var game = new ScrapedGame();

                    try
                    {
                        game.MainImage = scraper.StartParse(new SteamGame() { Name = "image", Uri = links, XPath = _MainImageXpath }).FirstOrDefault().Attributes["src"].Value;
                        game.Name = scraper.StartParse(new SteamGame() { Name = "name", Uri = links, XPath = _TitleXpath }).FirstOrDefault().InnerText;
                        game.ShortDescription = Regex.Replace(scraper.StartParse(new SteamGame() { Name = "shortdescription", Uri = links, XPath = _ShortDescriptionXPath }).FirstOrDefault().InnerText, @"\s+", " ");
                        game.LongDescription = Regex.Replace(scraper.StartParse(new SteamGame() { Name = "longdescription", Uri = links, XPath = _LongDescription }).FirstOrDefault().InnerText, @"\s+", " ");
                        game.Publisher = scraper.StartParse(new SteamGame() { Name = "publisher", Uri = links, XPath = _PublisherXPath }).FirstOrDefault().InnerText;
                        game.Developer = scraper.StartParse(new SteamGame() { Name = "developer", Uri = links, XPath = _DeveloperXPath }).FirstOrDefault().NextSibling.NextSibling.InnerText;
                        game.UserDefinedGenre = GetMultipleEntries(new SteamGame() { Name = "genre", Uri = links, XPath = _UserDefinedGenre });
                        game.ReleaseDate = scraper.StartParse(new SteamGame() { Name = "release", Uri = links, XPath = _ReleaseDateXPath }).FirstOrDefault().InnerText;
                        game.ReelImages = GetMultipleEntriesIMG(new SteamGame() { Name = "genre", Uri = links, XPath = _Highlightreel });
                        game.Icon = GetMultipleEntries(new SteamGame() { Name = "genre", Uri = links, XPath = _IconsXPath });
                        game.Genre = GetMultipleEntries(new SteamGame() { Name = "genre", Uri = links, XPath = _Genre });
                        success += 1;
                    }catch(Exception ex)
                    {
                        game.Name = "Cannot scrape the game : " + links;
                        ListOfScrapedGames.Add(game);
                        fail += 1;
                    }
                    ListOfScrapedGames.Add(game);
                }
             );
            ListOfScrapedGames.Add(new ScrapedGame() { Name = string.Format("TOTAL NUMBER OF GAMES : {0} SUCCESS : {1} FAIL : {2}", url.Count.ToString(), success.ToString(), fail.ToString()) });

            return ListOfScrapedGames;
        }

        private string GetMultipleEntries(SteamGame game)
        {
            var scraper = new ScraperModels.Scraper();
            var item = scraper.StartParse(game);
            var result = "";
            foreach( var data in item)
            {
                result += Regex.Replace(data.InnerText, @"\s+", " ") + ",\n";
            }

            return result;
        }
        

        private string GetMultipleEntriesIMG(SteamGame game)
        {
            var scraper = new ScraperModels.Scraper();
            var item = scraper.StartParse(game);
            var result = "";
            foreach (var data in item)
            {
                result += Regex.Replace(data.Attributes["href"].Value, @"\s+", " ") + ",\n";
            }

            return result;
        }

        
    }
}
