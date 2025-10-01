using CardHub.Classes;
using CardHub.CustomConrtols;
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
using System.Runtime.Remoting;
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

        // This will populate  every time the data set displayed on the DataGridView changes
        private List<Card> _currentlyAppliedGridViewData;

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

        private FilterControl _filterControl;
        private Form _floatingFilterForm;


        #endregion

        public formMainHub()
        {
            InitializeComponent();
            _filterControl = new FilterControl();
            _filterControl.ApplyFilterClicked += OnApplyFilterClicked;
        }

        private void OnApplyFilterClicked(object sender, EventArgs e)
        {
            // Set the filter criteria from values on the FilterControl
            var field = _filterControl.FieldComboBox.Text;
            var op = _filterControl.OperatorComboBox.Text;
            var val = _filterControl.ValueTextBox.Text;

            // Apply the filter to our DGV data source
            ApplyCardFilter(field, op, val);
        }
        
        /// <summary>
        /// Take the filter criteria entered by the user and apply it to the data set currently
        /// set as the DataSource to the Grid  View. Then set the filtered data to be the DataSource
        /// for the Grid View
        /// </summary>
        /// <param name="field"></param>
        /// <param name="op"></param>
        /// <param name="vlu"></param>
        private void ApplyCardFilter(string field, string op, string vlu)
        {
            var prop = typeof(Card).GetProperty(field);
            if (prop == null) return;

            var filtered = _currentlyAppliedGridViewData.Where(card =>
            {
                var value = prop.GetValue(card);
                if (value == null) return false;

                var stringValue = value.ToString();

                return op switch
                {
                    "=" => stringValue == vlu,
                    ">" => double.TryParse(stringValue, out var val1) && double.TryParse(vlu, out var val2) && val1 > val2,
                    "<" => double.TryParse(stringValue, out var val3) && double.TryParse(vlu, out var val4) && val3 < val4,
                    ">=" => double.TryParse(stringValue, out var val5) && double.TryParse(vlu, out var val6) && val5 >= val6,
                    "<=" => double.TryParse(stringValue, out var val7) && double.TryParse(vlu, out var val8) && val7 <= val8,
                    "Contains" => stringValue != null && stringValue.IndexOf(vlu, StringComparison.OrdinalIgnoreCase) >= 0,
                    _ => false
                };
            }).ToList();

            // Apply to grid
            advancedDataGridView1.DataSource = filtered;
            advancedDataGridView1.Refresh();

        }

        //private void ApplyCardFilter(string field, string op, string vlu)
        //{
        //    switch (field)
        //    {
        //        case "Level":
        //            var newData = _currentlyAppliedGridViewData.Where(i => i.Level == vlu).ToList();
        //            break;
        //        case "Attribtue":

        //            break;
        //        case "ATK":

        //            break;
        //        case "Card Name":

        //            break;

        //        default:
        //            break;
        //    }

        //}

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
            }
            else
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
        /// When the user selects a booster pack - populate the Advanced Data Grid View control with the cards
        /// that are in that booster pack set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boosterPackSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if item IS a string and also check the value is not null or just whitespace
            if (boosterPackSelect.SelectedItem is string selectedPack && !string.IsNullOrWhiteSpace(selectedPack))
            {
                // Set property value
                SelectedBoosterPack = selectedPack;

                // Obviously check if file exists, can't use if not not there,,, duhhh
                if (File.Exists(_gridDataFileName))
                {
                    // Read JSON into memory
                    string json = File.ReadAllText(_gridDataFileName);
                    
                    // Load the JSON data into a Dictionary<string, List<T>> object by deserializing it
                    var boosterData = JsonConvert.DeserializeObject<Dictionary<string, List<Card>>>(json);

                    // Make sure we actually have data and then output a List<T> to use
                    if (boosterData != null && boosterData.TryGetValue(selectedPack, out var cardList))
                    {
                        // BindingSource to populate GridView with
                        var bindingSource = new BindingSource();
                       
                        // We need to convert the List<T> to a DataTable. We do this so that the built in
                        // Sorting and filtering functionality of the Grid View can actually be used.
                        // List<T> is NOT capable of being sorted by the Grid, DataTable IS.
                        var table = ToDataTable(cardList);
                        bindingSource.DataSource = table;
                        _currentlyAppliedGridViewData = cardList;
                        advancedDataGridView1.DataSource = bindingSource;
                    }
                }
            }
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
            else // Already on UI thread — Safe to update directly
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
            // Create a new DataTable object, giving the table a name of the "Card"
            // With "Card" being the name of the object used (typeof(T).Name)
            // Then we get the names of each property in the used object
            var dataTable = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();

            // Iterate through Card.cs properties and add a new column to the DataTable object
            // using the property name as the column header
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Iterate through List<T> provided as the function call argument
            // then add each one as a new row to the DataTable object.
            foreach (var item in items)
            {
                var values = props.Select(p => p.GetValue(item, null)).ToArray();
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedBoosterPack = null;
            _gridDataFileName = null;
            _gridData = null;
            _boosterPackNameUrlDict = null;
            _boosterPackNameWithCards = null;
            SelectedBoosterPack = null;
            UpdatedProgressBarValue = 0;
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_floatingFilterForm == null || _floatingFilterForm.IsDisposed)
            {
                // Create and show a form that will hold the FilterControl
                _floatingFilterForm = new Form
                {
                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                    StartPosition = FormStartPosition.CenterParent,
                    Size = new Size(300, 250),
                    Text = "Filter Options",
                    TopMost = true // Optional: keeps it above other windows
                };

                _filterControl.Dock = DockStyle.Fill;

                _floatingFilterForm.Controls.Add(_filterControl);
                _floatingFilterForm.Show();
            }
            else
            {
                _floatingFilterForm.BringToFront();
                _floatingFilterForm.Activate();
            }
        }

        /// <summary>
        /// When user selects item from the right click contect meni=u
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFilterControlContextMenu_Click(object sender, EventArgs e)
        {
            if (_floatingFilterForm == null || _floatingFilterForm.IsDisposed)
            {
                // Create and show a form that will hold the FilterControl
                _floatingFilterForm = new Form
                {
                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                    StartPosition = FormStartPosition.CenterParent,
                    Size = new Size(300, 250),
                    Text = "Filter Options",
                    TopMost = true // Optional: keeps it above other windows
                };

                _filterControl.Dock = DockStyle.Fill;

                _floatingFilterForm.Controls.Add(_filterControl);
                _floatingFilterForm.Show();
            }
            else
            {
                _floatingFilterForm.BringToFront();
                _floatingFilterForm.Activate();
            }
        }

        /// <summary>
        /// When right mouse button is clicked, show the user a contect menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedDataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = advancedDataGridView1.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    advancedDataGridView1.ClearSelection();
                    advancedDataGridView1.Rows[hitTest.RowIndex].Selected = true;

                    // Stoer the DGV cell that the right click was performed on. This can
                    // be used in a way where we use that cells value as filter value.
                    advancedDataGridView1.CurrentCell = advancedDataGridView1.Rows[hitTest.RowIndex].Cells[hitTest.ColumnIndex];

                    // Show the context menu
                    filterContextMenu.Show(advancedDataGridView1, e.Location);
                }
            }
        }
    }
}
