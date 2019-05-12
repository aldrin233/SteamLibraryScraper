
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamScraper.Utilities
{
    public class GameSearcher
    {
        public List<string> SearchGames(List<string> names)
        {
            var urlList = new List<string>();
            var steamSearchEngine = @"https://store.steampowered.com/search/?term=";
            var scraper = new ScraperModels.Scraper();

            Parallel.ForEach(
                names,
                new ParallelOptions { MaxDegreeOfParallelism = 20 },
                currentgame => {

                    var url = scraper.StartParse(new SteamGame(){
                        Name = currentgame,
                        Uri = steamSearchEngine+currentgame,
                        XPath = "//div[@id='search_result_container']//a"
                    });

                    urlList.Add(url.FirstOrDefault().Attributes["href"].Value);
                });
             return urlList;

        }
        
    }
}
