using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamScraper.Utilities
{
    public class ScrapedGame
    {
        public string Name { get; set; }
        public string MainImage { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string UserDefinedGenre { get; set; }
        public string Genre { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string ReleaseDate { get; set; }

        string ImageList { get; set; }

        public string Icon { get; set; }
        public string ReelImages { get; set; }
    }
}
