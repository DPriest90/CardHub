using CardHub.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CardHub.Forms
{
    public partial class formMainHub : Form
    {
        #region form properties

        private string _selectedBoosterPack = string.Empty;
        private string _gridDataFileName = string.Empty;
        private string _gridData = string.Empty;

        private int _updatedProgressBarValue = 0;

        public Dictionary<string, string> _boosterPackNameUrlDict;
        public Dictionary<string, List<string>> _boosterPackNameWithCards;

        public string SelectedBoosterPack
        {
            get { return _selectedBoosterPack; }
            set { _selectedBoosterPack = value; }
        }


        // This needs to be wired to the "_progressBarCurrentValue" property in the
        // HtmlWorker static class
        public int UpdatedProgressBarValue
        {
            get { return _updatedProgressBarValue; }
            set { _updatedProgressBarValue = value; }

        }

        #endregion

        public formMainHub()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the load event for the main hub form.
        /// </summary>
        /// <remarks>This method asynchronously retrieves booster pack names and populates the data source of the <see
        /// cref="boosterPackSelect"/> control. Ensure that the method is invoked during the form's load event to initialize the
        /// booster pack selection.</remarks>
        /// <param name="sender">The source of the event, typically the form itself.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing event data.</param>
        private async void formMainHub_Load(object sender, EventArgs e)
        {
            // Give HtmlWorker static class access to this form and this forms
            // status strip progress bar (so that proper, accurate progress can
            // be shown on form).
            HtmlWorker._mainForm = this;
            HtmlWorker._mainFormProgressBar = this.toolStripProgressBar1;
             string _boosterPackCardDataJsonFile = ConfigurationManager.AppSettings["BoosterPackCardDataJsonFile"];
            string fileName = ConfigurationManager.AppSettings["BoosterPackCacheData"];
            string boosterPackFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            _gridDataFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _boosterPackCardDataJsonFile);

            // Check if data file exists. If it is the first time the app has been run on this machine
            // there will not be a data file, so the initial run process will need to be run. Otherwise
            // set the data file contents as the data source for our "boosterPackSelect" combox box conntrol.
            if (File.Exists(boosterPackFileName))
            {
                // Set DataSource to the file
                List<string> boosterPackNames = File.ReadAllLines(boosterPackFileName).ToList();

                // Sort the DataSource by descending alphabetical order
                boosterPackSelect.DataSource = boosterPackNames.OrderBy(n => n).ToList();
                boosterPackSelect.SelectedIndex = 0;

                // Force refresh of the control state
                boosterPackSelect.Refresh();

                // Populate PackName/URL dictionary from cached JSON file
                string jsonFileName = ConfigurationManager.AppSettings["BoosterPackNameUrlData"];                
                _boosterPackNameUrlDict = LoadPackUrlDictionaryFromJson(jsonFileName);

                toolStripStatusLabel1.Text = "Ready...";

                boosterPackSelect.Enabled = true;

                // Read the JSON file that contains data for what cards are in each booster pack
                // (for the Data Grid View control)
                //_gridData = File.ReadAllText(_gridDataFileName);
            }
            else // This is the first time running
            {
                // Get HTML - This function gets and parses the HTML. It also populates certain 
                // variables used in
                UpdateStatusBarLabel("Fetching HTML For Booster Packs.This Will Take Some Time...");
                var websiteHtml = await HtmlWorker.GetHtml();
                UpdateStatusBarLabel("Booster Pack HTML Retrieved");

                var packNames = HtmlWorker._boosterPackNames;

                // Just write out all entries in the List<string>. This file will be used as the data source
                // for our "boosterPackSelect" ComboBox control.
                using (StreamWriter writer = new StreamWriter(boosterPackFileName))
                {
                    foreach (string s in packNames)
                    {
                        writer.WriteLine(s);
                    }
                }

                boosterPackSelect.DataSource = packNames.OrderBy(n => n).ToList();
                boosterPackSelect.SelectedIndex = 0;
                boosterPackSelect.Refresh();

                toolStripStatusLabel1.Text = "Ready...";

                boosterPackSelect.Enabled = true;
            }

           
        }

        /// <summary>
        /// Sets the selected booster pack when the user changes the selection in the
        /// booster pack dropdown. Also populates data grid view control with cards
        /// belonging to selected booster pack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boosterPackSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (boosterPackSelect.SelectedItem is string selectedPack && !string.IsNullOrWhiteSpace(selectedPack))
            {
                SelectedBoosterPack = selectedPack;

                if (File.Exists(_gridDataFileName))
                {
                    string json = File.ReadAllText(_gridDataFileName);

                    var boosterData = JsonConvert.DeserializeObject<Dictionary<string, List<Card>>>(json);

                    if (boosterData != null && boosterData.TryGetValue(selectedPack, out var cardList))
                    {
                        var bindingSource = new BindingSource();
                       
                        var table = ToDataTable(cardList);
                        bindingSource.DataSource = table;
                        advancedDataGridView1.DataSource = bindingSource;
                    }
                }
            }

            //// Set the selected booster pack based on the user's selection in the dropdown.
            //if (boosterPackSelect.SelectedItem != null &&
            //    !string.IsNullOrEmpty(boosterPackSelect.SelectedItem.ToString()))
            //{
            //    SelectedBoosterPack = boosterPackSelect.SelectedItem.ToString();

            //    string json = File.ReadAllText(_gridDataFileName);

            //    // Deserialize into a dictionary of booster packs
            //    var boosterData = JsonConvert.DeserializeObject<Dictionary<string, List<Card>>>(json);
            //    string selectedPack = boosterPackSelect.SelectedItem?.ToString();

            //    // Access a specific pack
            //    List<Card> doomPackCards = boosterData[selectedPack];

            //    advancedDataGridView1.DataSource = doomPackCards;
            //}

        }

        /// <summary>
        /// Updates the status strip label with the provided text, using thread-safe invocation if necessary.
        /// </summary>
        /// <param name="newText">The text to display in the status strip label.</param>
        private void UpdateStatusBarLabel(string newText)
        {
            if (string.IsNullOrEmpty(newText)) return;

            // Check if we're on a different thread than the UI thread
            if (this.InvokeRequired) // We are, so marshal the action
            {
                this.Invoke(new Action(() => toolStripStatusLabel1.Text = newText));
            }
            else // Already on UI thread—safe to update directly
            {
                toolStripStatusLabel1.Text = newText;
            }            
        }

        private Dictionary<string, string> LoadPackUrlDictionaryFromJson(string fileName)
        {   
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
                return new Dictionary<string, string>(); // Or handle fallback however you prefer

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        /// <summary>
        /// To enable DGV records to be sorted, we convert our List<T> to a
        /// DataTable object and use the DataTable as DataSource for the Grid
        /// DataBinding source. DataGridView cannot sort List<T> objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = props.Select(p => p.GetValue(item, null)).ToArray();
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }


    }
}
