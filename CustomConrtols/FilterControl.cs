using CardHub.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardHub.CustomConrtols
{
    public partial class FilterControl : UserControl
    {
        #region Properties

        private List<string> _customFilterCriteria;
        private TableLayoutPanel _filterLayoutPanel;

        private ComboBox _fieldComboBox;
        private ComboBox _operatorComboBox;
        private TextBox _valueTextBox;

        /// <summary>
        /// Gets or sets the ComboBox control used to display and select fields.
        /// </summary>
        public ComboBox FieldComboBox
        {
            get { return _fieldComboBox; }
            set { _fieldComboBox = value; }
        }

        /// <summary>
        /// Gets or sets the ComboBox control used to select an operator.
        /// </summary>
        public ComboBox OperatorComboBox
        {
            get { return _operatorComboBox; }
            set { _operatorComboBox = value; }
        }

        /// <summary>
        /// Gets or sets the TextBox used to display or input the value.
        /// </summary>
        public TextBox ValueTextBox
        {
            get { return _valueTextBox; }
            set { _valueTextBox = value; }
        }

        /// <summary>
        /// Occurs when the user clicks the "Apply Filter" button.
        /// </summary>
        public event EventHandler ApplyFilterClicked;

        #endregion

        public FilterControl()
        {
            InitializeComponent();
            InitializeFilterTableLayoutPanel();

            // Initialize this property
            _customFilterCriteria = new List<string>();
        }
        
        /// <summary>
        /// Perform initialization of form controls (labels, combo boxes, text box and button)
        /// and add to the TableLayoutControl (this handles all of the spacing and resizing etc.
        /// </summary>
        /// <returns></returns>
        private void InitializeFilterTableLayoutPanel()
        {
            _filterLayoutPanel = new TableLayoutPanel
            {
                Name = "filterLayoutPanel",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 7,
                AutoSize = true
            };

            // Add all the controls that the user will interact with for this filter form
            if (!filterControlBasePanel.Controls.ContainsKey("filterLayoutPanel"))
            {
                _filterLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

                // Define row styles
                _filterLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));           // Label 1
                _filterLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));    // Field ComboBox
                _filterLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));           // Label 2
                _filterLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));    // Operator ComboBox
                _filterLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));           // Label 3
                _filterLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));    // Value TextBox
                _filterLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));           // Apply Filter Button

                // Add to parent panel
                filterControlBasePanel.Controls.Add(_filterLayoutPanel);

                // Create controls
                var fieldLabel = new Label { Text = "Field", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
                _fieldComboBox = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                _fieldComboBox.Items.AddRange(new[] { "Level", "Attribute", "Attack", "Name" });

                var operatorLabel = new Label { Text = "Operator", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
                _operatorComboBox = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                _operatorComboBox.Items.AddRange(new[] { "=", ">", "<", ">=", "<=", "Contains" });

                var valueLabel = new Label { Text = "Value", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
                _valueTextBox = new TextBox
                {
                    Dock = DockStyle.Fill
                };

                // Add controls to layout
                _filterLayoutPanel.Controls.Add(fieldLabel, 0, 0);
                _filterLayoutPanel.Controls.Add(_fieldComboBox, 0, 1);
                _filterLayoutPanel.Controls.Add(operatorLabel, 0, 2);
                _filterLayoutPanel.Controls.Add(_operatorComboBox, 0, 3);
                _filterLayoutPanel.Controls.Add(valueLabel, 0, 4);
                _filterLayoutPanel.Controls.Add(_valueTextBox, 0, 5);

                // Button to apply our filter when clicked
                var applyFilterButton = new Button
                {
                    Text = "Apply Filter",
                    Dock = DockStyle.Fill
                };

                // Give the button a Click event handler
                applyFilterButton.Click += (s, e) =>
                {
                    ApplyFilterClicked?.Invoke(this, EventArgs.Empty);
                };

                // Adda button with it's shiny new Click event handler to the TableLayourPanel
                _filterLayoutPanel.Controls.Add(applyFilterButton, 0, 6);
            }
        }

        /// <summary>
        /// Handle ApplyFilter click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplyFilterClicked(object sender, EventArgs e)
        {
            ApplyFilterClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns the filter clause, based off what the user selected
        /// </summary>
        /// <returns></returns>
        public FilterClause GetFilterClause()
        {
            return new FilterClause
            {
                Field = _fieldComboBox.SelectedItem?.ToString(),
                Operator = _operatorComboBox.SelectedItem?.ToString(),
                Value = _valueTextBox.Text
            };
        }

    }
}
