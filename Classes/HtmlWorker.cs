using CardHub.Forms;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;

namespace CardHub.Classes
{
    /// <summary>
    /// Provides methods for retrieving and processing HTML data related to booster packs and cards.
    /// </summary>
    /// <remarks>This class includes functionality for fetching HTML content from specified URLs, extracting
    /// booster pack names, and processing card data from HTML elements. It is designed to work with specific HTML
    /// structures and relies on predefined class names and attributes to locate relevant data.</remarks>
    public static class HtmlWorker
    {
        #region Properties

        private static readonly string _url = ConfigurationManager.AppSettings["Card-Site-URL"];
        private static readonly string _baseUrl = ConfigurationManager.AppSettings["Base-URL"];


        public static List<string> _boosterPackNames = new List<string>();
        public static int _progressBarCurrentValue = 0;
        public static formMainHub _mainForm;

        public static ToolStripProgressBar _mainFormProgressBar;

        /// <summary>
        /// Each KVP will have the name of the booster pack as they key and the URL
        /// for that booster packs web page;
        /// </summary>
        private static Dictionary<string, string> _boosterPackWebPageUrlDict = new Dictionary<string, string>();

        private static string _boosterPackCardDataJsonFile = ConfigurationManager.AppSettings["BoosterPackCardDataJsonFile"];
        private static readonly HttpClient _client = new HttpClient();
        /// <summary>
        /// Each KVP will have the name of the booster pack as the key and List<Card> as the value, so we will have data for every card in that booster pack.
        /// </summary>
        private static Dictionary<string, List<Card>> _boosterPackAndCardsDict = new Dictionary<string, List<Card>>();

        /// <summary>
        /// Each KVP will contain the booster pack name as as the Key and a list of
        /// all cards related to that booster pack as the Value
        /// </summary>
        private static Dictionary<string, List<string>> _boosterPackWithCardNamesDictionary = new Dictionary<string, List<string>>();

        private static string _imageUrlListFileName = "Data/image_urls.csv";

        #endregion

        static HtmlWorker()
        {
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("CardHub/1.0");
        }


        /// <summary>
        /// Retrieves and processes HTML content from a predefined URL, extracting booster pack information and
        /// associated card details.
        /// </summary>
        /// <remarks>This method performs the following operations: <list type="bullet">
        /// <item><description>Fetches HTML content from a predefined URL.</description></item>
        /// <item><description>Parses the HTML to extract booster pack links and their associated card
        /// details.</description></item> <item><description>Tracks and stores booster pack names, ensuring uniqueness
        /// by appending a numeric suffix if duplicates are found.</description></item> <item><description>Updates a
        /// progress bar to reflect the processing status.</description></item> <item><description>Writes the booster
        /// pack name-to-URL mapping to a JSON file for caching purposes.</description></item> </list> The method
        /// updates several shared collections and fields, including dictionaries for booster pack URLs and card
        /// details, which are used elsewhere in the application.</remarks>
        /// <returns>A <see cref="Task{String}"/> representing the asynchronous operation. The returned string is currently
        /// empty.</returns>
        public static async Task<string> GetHtml()
        {
            try
            {
                string html = await CallUrl(_url);

                List<string> inputTags = GetBoosterPackInputTags(html);
                Dictionary<string, int> packNameTracker = new Dictionary<string, int>();
                int pbTracker = 0;

                _mainFormProgressBar.Maximum = inputTags?.Count ?? 0;

                if (inputTags != null && inputTags.Count > 0)
                {
                    string packNameElementClass = "broad_title";

                    foreach (string line in inputTags)
                    {
                        pbTracker++;
                        _mainFormProgressBar.Value = pbTracker;

                        string fullPackLink = _baseUrl + line.Substring(48, line.Length - 50);
                        string packHtml = await CallUrl(fullPackLink);

                        var packHtmlDoc = new HtmlAgilityPack.HtmlDocument();
                        packHtmlDoc.LoadHtml(packHtml);

                        var pageTitleContainer = packHtmlDoc.DocumentNode.SelectSingleNode("//title");
                        string completePageTitle = pageTitleContainer?.InnerHtml ?? "UnknownPack";
                        string rawPackName = completePageTitle.Split('|')[0].Trim();
                        string packName = rawPackName.Replace(" ", "");

                        // Track appearances of each pack name
                        if (packNameTracker.ContainsKey(packName))
                        {
                            packNameTracker[packName]++;
                            packName += "_" + packNameTracker[packName];
                        }
                        else
                        {
                            packNameTracker[packName] = 1;
                        }

                        _boosterPackWebPageUrlDict[packName] = fullPackLink;
                        _boosterPackNames.Add(packName);

                        #region Class text values for the information we need to retrieve from HTML elements
                        string cardContainerClassName = "t_row c_normal";
                        string cardImageContainerClassName = "box_card_img";
                        string cardInfoParentContainer = "flex_1";
                        string cardNameContainer = "card_name";
                        string cardTitleClassName = "cnm";
                        #endregion

                        // This gets all cards in the booster pack
                        var cardsInBoosterPack = packHtmlDoc.DocumentNode.Descendants("div")
                            .Where(node => node.GetAttributeValue("class", "").Contains(cardContainerClassName))
                            .ToList();

                        // This get us the raw HTML for each card i the booster pack
                        var cardsHtmlList = cardsInBoosterPack
                            .Select(node => node.InnerHtml)
                            .ToList();

                        // Here we populate a List of Card objects with what data we can get
                        // from the raw HTML and that to a Dict<string, List<Card>> object.
                        // This way we can display every card in a certain booster pack
                        // to the user on request
                        List<Card> packCards = ParseCardsFromHtml(cardsHtmlList);
                        _boosterPackAndCardsDict.Add(packName, packCards);

                        _boosterPackWithCardNamesDictionary.Add(packName, cardsHtmlList);
                    }

                    // Present data to the form
                    _mainForm._boosterPackNameUrlDict = _boosterPackWebPageUrlDict;
                    _mainForm._boosterPackNameWithCards = _boosterPackWithCardNamesDictionary;

                    // Write our booster pack name + URL Dictionary object to JSON file.
                    string urlCacheFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PackNameUrlMap.json");
                    string json = JsonConvert.SerializeObject(_boosterPackWebPageUrlDict, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(urlCacheFilePath, json);

                    // Write our JSON data file that uses a serialized object <Dictionary<string, List<Card>>
                    string boosterPackCards = JsonConvert.SerializeObject(_boosterPackAndCardsDict);
                    File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _boosterPackCardDataJsonFile), boosterPackCards);

                }

                return "";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Populate Card POCO with data from HTML.Data like Name of the card, level, attribute,
        /// attack and defence. 
        /// 
        /// </summary>
        /// <param name="cardsHtmlList"></param>
        /// <returns></returns>
        public static List<Card> ParseCardsFromHtml(List<string> cardsHtmlList)
        {
            List<Card> parsedCards = new List<Card>();

            foreach (string rawHtml in cardsHtmlList)
            {
                HtmlAgilityPack.HtmlDocument cardDoc = new HtmlAgilityPack.HtmlDocument();
                cardDoc.LoadHtml(rawHtml);

                // Populate POCO with data from the Raw HTML
                Card card = new Card();
                card.Name = cardDoc.DocumentNode.SelectSingleNode("//dd[@class='box_card_name flex_1 top_set']")?.InnerText.Trim();
                card.Attribute = cardDoc.DocumentNode.SelectSingleNode("//span[@class='box_card_attribute']//span")?.InnerText.Trim();
                card.Level = cardDoc.DocumentNode.SelectSingleNode("//span[@class='box_card_level_rank level']//span")?.InnerText.Trim();
                card.Link = cardDoc.DocumentNode.SelectSingleNode("//span[@class='box_card_linkmarker']//span")?.InnerText.Trim();
                card.Attack = cardDoc.DocumentNode.SelectSingleNode("//span[@class='atk_power']//span")?.InnerText.Trim();
                card.Defense = cardDoc.DocumentNode.SelectSingleNode("//span[@class='def_power']//span")?.InnerText.Trim();
                card.Card_Text = cardDoc.DocumentNode.SelectSingleNode("//dd[@class='box_card_text c_text flex_1']")?.InnerText.Trim();

                string origTypeValue = cardDoc.DocumentNode.SelectSingleNode("//span[@class='card_info_species_and_other_item']//span")?.InnerText;

                if (origTypeValue != null && origTypeValue.Contains("["))
                    card.Type = origTypeValue.Replace("[", "").Replace("]", "").TrimEnd().TrimStart();
                else
                    card.Type = origTypeValue;

                // As Rank and Level use the same css class name, blank the level property
                // and populate the Rank property instead
                if (card.Level != null && card.Level.ToLower().Contains("rank"))
                {
                    card.Level = "";
                    card.Rank = cardDoc.DocumentNode.SelectSingleNode("//span[@class='box_card_level_rank level']//span")?.InnerText.Trim();
                }

                parsedCards.Add(card);
            }

            return parsedCards;
        }


        /// <summary>
        /// Gets the names of all booster packs available on the specified website.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<string>> GetBoosterPackNamesAsync()
        {
            // Call the URL to get the HTML content
            string html = await CallUrl(_url);

            // If the HTML content is empty or null, return an empty list
            if (string.IsNullOrEmpty(html)) return new List<string>();

            // Load the HTML content into an HtmlDocument for parsing
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // Define the filter value to find the specific div elements that contain booster pack information
            string filterValue = "pack pack_en";

            // Use LINQ to find all div elements that contain the specified class
            var packNodes = doc.DocumentNode.Descendants("div")
                .Where(div => div.GetAttributeValue("class", "").Contains(filterValue))
                .ToList();

            List<string> packNames = new List<string>();

            // Iterate through the found div elements to extract the booster pack names
            foreach (var node in packNodes)
            {
                // Each node contains the name of the booster pack in its inner text
                // We trim the text to remove any leading or trailing whitespace
                var a = node.InnerText.Trim();

                // We find the index of the first newline character to isolate the pack name
                int index = a.IndexOf("\r\n");

                // We get the substring up to the first newline character
                a = a.Substring(0, index);

                // If the extracted name is not null or empty, we add it to the list of pack names
                if (!string.IsNullOrEmpty(a))
                    packNames.Add(a);
               
            }

            // Return the list of booster pack names
            return packNames;
        }

        /// <summary>
        /// Uses HttpClient to retrieve all Html content within the pages <body> tag and return
        /// as a string. Function is asynchronous.
        /// </summary>
        /// <param name="fullUrl"></param>
        /// <returns></returns>
        private static async Task<string> CallUrl(string fullUrl)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                var response = await _client.GetStringAsync(fullUrl);
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static List<string> GetBoosterPackInputTags(string html)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(html);

                string filterValue = "pack pack_en";

                // Get a List of every HtmlNode object in the currently loaded HTML which has a class that
                // matches our search filter value
                var packNodes = htmlDoc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains(filterValue))
                    .ToList();

                List<string> links = new List<string>();

                // Iterate through our div collection
                foreach (HtmlNode node in packNodes)
                {
                    string packHtml = node.OuterHtml;

                    // Create a new temporary HtmlDoc that contains ONLY the div we collected before
                    HtmlAgilityPack.HtmlDocument packHtmlDoc = new HtmlAgilityPack.HtmlDocument();
                    packHtmlDoc.LoadHtml(packHtml);

                    // Query the new HtmlDoc to get ONLY the input HTML tag as this is what contains the link
                    // to every booster packs web page
                    var theInputTag = packHtmlDoc.DocumentNode.Descendants("input")
                        .Where(input => input.GetAttributeValue("class", "").Contains("link_value"))
                        .ToList();


                    // Add input HTML tag to list
                    links.Add(theInputTag[0].OuterHtml);
                }

                return links;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    
        private static List<string> ImportCardImageUrlList()
        {
            List<string> allImageUrls = new List<string>();

            using (StreamReader reader = new StreamReader(_imageUrlListFileName))
            {
                // While the next line to be read is not null or empty
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        allImageUrls.Add(line);
                    }
                }
            }
                return allImageUrls;
        }
    }
}
