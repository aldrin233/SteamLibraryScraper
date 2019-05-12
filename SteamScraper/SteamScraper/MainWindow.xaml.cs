using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace SteamScraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string inputlocation;
        private string outputlocation;
        private string outputfilename = "ScrapedGames.csv";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void bt_input_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                inputlocation = openFileDialog.FileName;
                tb_statusblock.Text += "\nInput file selected.";

                FileInfo fi = new FileInfo(openFileDialog.FileName);
                outputlocation = fi.Directory + "\\" + outputfilename;
                tb_outputloc.Text = outputlocation;
                bt_scrape.IsEnabled = true;
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            tb_statusblock.Text += "Welcome! please Select the input file";

        }

        private void bt_scrape_Click(object sender, RoutedEventArgs e)
        {

            tb_statusblock.Dispatcher.Invoke(() => { tb_statusblock.Text += "\nReading input file..."; });
            Action Scrape = new Action( () => {
                try
                {
                    string[] textfile = null;

                    textfile = File.ReadAllLines(inputlocation);

                    tb_statusblock.Dispatcher.Invoke(() => { tb_statusblock.Text += "\nInput file read starting to process data..."; });

                    List<Utilities.ScrapedGame> listOfScrapedGames = new List<Utilities.ScrapedGame>();
                    var toListGameNames = textfile.ToList();
                    var urlList = new List<string>();

                    tb_statusblock.Dispatcher.Invoke(() => { tb_statusblock.Text += "\nData processed starting to search for the games..."; });

                    var GameSearcher = new Utilities.GameSearcher();
                    urlList = GameSearcher.SearchGames(toListGameNames);

                    tb_statusblock.Dispatcher.Invoke(() => { tb_statusblock.Text += "\nGames Searched scraping will now be started..."; });


                    var GameScraper = new Utilities.GameScraper();
                    listOfScrapedGames = GameScraper.ScrapeGames(urlList);
                    TextWriter tx = File.CreateText(outputlocation);
                    var csv = new CsvWriter(tx);
                    csv.WriteRecords(listOfScrapedGames);
                    tx.Close();

                    tb_statusblock.Dispatcher.Invoke(() => { tb_statusblock.Text = "\nGames has been scraped!"; });

                }
                catch (Exception ex)
                {
                    tb_statusblock.Dispatcher.Invoke(() => { tb_statusblock.Text += "\n Error: " + ex.Message; });
                }
            });
            Task.Run(Scrape);
        }

    }

}

