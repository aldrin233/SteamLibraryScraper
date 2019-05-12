using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {

            //string url = @"https://store.steampowered.com/app/230410/Warframe/";

            //var scraper = new ScraperModels.Scraper();
            //var image = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "image",
            //    Uri = url,
            //    XPath = "/html[1]/body[1]/div[1]/div[7]/div[3]/div[1]/div[2]/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]/img[1]"
            //});
            //var title = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "title",
            //    Uri = url,
            //    XPath = "//div[@class='apphub_AppName']"
            //});

            //var description = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "description",
            //    Uri = url,
            //    XPath = "//div[@class='game_description_snippet']"
            //});

            //var genre = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "genres",
            //    Uri = url,
            //    XPath = "//a[@class='app_tag']"
            //});

            //var developer = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "developer",
            //    Uri = url,
            //    XPath = "//div[@class='dev_row']//b"
            //});

            //var releasedate = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "releasedate",
            //    Uri = url,
            //    XPath = "//div[@class='date']"
            //});

            //var longdescription = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "releasedate",
            //    Uri = url,
            //    XPath = "//div[@id='game_area_description']"
            //});

            //var gamedetailsicons = scraper.StartParse(new ScraperModels.Models.ScraperItem()
            //{
            //    Name = "gamedetailedicons",
            //    Uri = url,
            //    XPath = "//a[@class='Name']"
            //});


            //foreach (var item in image)
            //{
            //    Console.WriteLine("image url \n" + item.Attributes["src"].Value + "\n");
            //}

            //foreach (var item in title)
            //{
            //    Console.WriteLine("title \n" + item.InnerText);
            //}

            //foreach (var item in releasedate)
            //{
            //    Console.WriteLine("date released \n" + item.InnerText);
            //}

            //foreach (var item in description)
            //{
            //    Console.WriteLine("\ndescription " + item.InnerText.Replace("								", ""));
            //}
            //Console.WriteLine("\n");
            //foreach (var item in gamedetailsicons)
            //{
            //    Console.WriteLine("items under side bar : " + item.InnerText);
            //}

            //Console.WriteLine("\n");
            //foreach (var item in developer)
            //{
            //    Console.Write(item.InnerText);
            //    Console.Write(" : " + item.NextSibling.NextSibling.InnerText + "\n");
            //}

            //Console.WriteLine("\n");

            //foreach (var item in longdescription)
            //{
            //    Console.WriteLine(item.InnerText);
            //}

            //Console.WriteLine("\n" + "user defined genre");

            //foreach (var item in genre)
            //{
            //    Console.Write("\n" + Regex.Replace(item.InnerText, @"\s+", " "));
            //}


            //var searchURL = @"https://store.steampowered.com/search/?term=cuisine";
            //var scraper = new ScraperModels.Scraper();

            //var results = scraper.StartParse(new SteamScraper.Utilities.SteamGame()
            //{
            //    Name = "cusine",
            //    Uri = searchURL,
            //    XPath = "//div[@id='search_result_container']//a"
            //});

            //Console.WriteLine(results.FirstOrDefault().Attributes["href"].Value);

            try
            {
                string[] textfile = System.IO.File.ReadAllLines(@"C:\Users\Aldrin\Desktop\et.txt");

                var listTextfile = textfile.ToList();

                var x = new SteamScraper.Utilities.GameSearcher();

                var games = x.SearchGames(listTextfile);

                var y = new SteamScraper.Utilities.GameScraper();

                var g = y.ScrapeGames(games);

                foreach (var ga in g)
                {
                    Console.WriteLine(ga.Name);
                    Console.WriteLine(ga.ShortDescription);
                    Console.WriteLine(ga.MainImage);
                    Console.WriteLine(ga.UserDefinedGenre);
                    Console.WriteLine(ga.ReelImages);
                    Console.WriteLine(ga.Developer);
                    Console.WriteLine(ga.Publisher);
                    Console.WriteLine(ga.LongDescription);
                }



            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
