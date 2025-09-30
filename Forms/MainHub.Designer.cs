namespace CardHub.Forms
{
    partial class formMainHub
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.packSelectGroupBox = new System.Windows.Forms.GroupBox();
            this.advancedDgvPanel = new System.Windows.Forms.Panel();
            this.advancedDataGridView1 = new Zuby.ADGV.AdvancedDataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.boosterPackSelect = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attributeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defenseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packSelectGroupBox.SuspendLayout();
            this.advancedDgvPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // packSelectGroupBox
            // 
            this.packSelectGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packSelectGroupBox.Controls.Add(this.advancedDgvPanel);
            this.packSelectGroupBox.Controls.Add(this.label1);
            this.packSelectGroupBox.Controls.Add(this.boosterPackSelect);
            this.packSelectGroupBox.Location = new System.Drawing.Point(37, 53);
            this.packSelectGroupBox.Name = "packSelectGroupBox";
            this.packSelectGroupBox.Size = new System.Drawing.Size(1064, 594);
            this.packSelectGroupBox.TabIndex = 0;
            this.packSelectGroupBox.TabStop = false;
            this.packSelectGroupBox.Text = "Pack Selection";
            // 
            // advancedDgvPanel
            // 
            this.advancedDgvPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedDgvPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.advancedDgvPanel.Controls.Add(this.advancedDataGridView1);
            this.advancedDgvPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.advancedDgvPanel.Location = new System.Drawing.Point(36, 182);
            this.advancedDgvPanel.Name = "advancedDgvPanel";
            this.advancedDgvPanel.Size = new System.Drawing.Size(989, 390);
            this.advancedDgvPanel.TabIndex = 3;
            // 
            // advancedDataGridView1
            // 
            this.advancedDataGridView1.AllowUserToOrderColumns = true;
            this.advancedDataGridView1.AutoGenerateColumns = false;
            this.advancedDataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.advancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn,
            this.attributeDataGridViewTextBoxColumn,
            this.attackDataGridViewTextBoxColumn,
            this.defenseDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.cardTextDataGridViewTextBoxColumn,
            this.linkDataGridViewTextBoxColumn,
            this.rankDataGridViewTextBoxColumn});
            this.advancedDataGridView1.DataSource = this.cardBindingSource1;
            this.advancedDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView1.FilterAndSortEnabled = true;
            this.advancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advancedDataGridView1.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.advancedDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridView1.MaxFilterButtonImageHeight = 23;
            this.advancedDataGridView1.Name = "advancedDataGridView1";
            this.advancedDataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.advancedDataGridView1.RowHeadersWidth = 62;
            this.advancedDataGridView1.RowTemplate.Height = 28;
            this.advancedDataGridView1.Size = new System.Drawing.Size(989, 390);
            this.advancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advancedDataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Booster Pack";
            // 
            // boosterPackSelect
            // 
            this.boosterPackSelect.Enabled = false;
            this.boosterPackSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boosterPackSelect.FormattingEnabled = true;
            this.boosterPackSelect.Location = new System.Drawing.Point(36, 74);
            this.boosterPackSelect.Name = "boosterPackSelect";
            this.boosterPackSelect.Size = new System.Drawing.Size(362, 28);
            this.boosterPackSelect.TabIndex = 0;
            this.boosterPackSelect.SelectedIndexChanged += new System.EventHandler(this.boosterPackSelect_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1159, 32);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(179, 25);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 24);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.ToolTipText = "HTML Retrieval Progress";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.nameDataGridViewTextBoxColumn.ToolTipText = "Name of the card";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            this.levelDataGridViewTextBoxColumn.ReadOnly = true;
            this.levelDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.levelDataGridViewTextBoxColumn.ToolTipText = "Level of the monster card";
            this.levelDataGridViewTextBoxColumn.Width = 150;
            // 
            // attributeDataGridViewTextBoxColumn
            // 
            this.attributeDataGridViewTextBoxColumn.DataPropertyName = "Attribute";
            this.attributeDataGridViewTextBoxColumn.HeaderText = "Attribute";
            this.attributeDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.attributeDataGridViewTextBoxColumn.Name = "attributeDataGridViewTextBoxColumn";
            this.attributeDataGridViewTextBoxColumn.ReadOnly = true;
            this.attributeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.attributeDataGridViewTextBoxColumn.ToolTipText = "Cards Attribute";
            this.attributeDataGridViewTextBoxColumn.Width = 150;
            // 
            // attackDataGridViewTextBoxColumn
            // 
            this.attackDataGridViewTextBoxColumn.DataPropertyName = "Attack";
            this.attackDataGridViewTextBoxColumn.HeaderText = "Attack";
            this.attackDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.attackDataGridViewTextBoxColumn.Name = "attackDataGridViewTextBoxColumn";
            this.attackDataGridViewTextBoxColumn.ReadOnly = true;
            this.attackDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.attackDataGridViewTextBoxColumn.ToolTipText = "Monsters attack power";
            this.attackDataGridViewTextBoxColumn.Width = 150;
            // 
            // defenseDataGridViewTextBoxColumn
            // 
            this.defenseDataGridViewTextBoxColumn.DataPropertyName = "Defense";
            this.defenseDataGridViewTextBoxColumn.HeaderText = "Defense";
            this.defenseDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.defenseDataGridViewTextBoxColumn.Name = "defenseDataGridViewTextBoxColumn";
            this.defenseDataGridViewTextBoxColumn.ReadOnly = true;
            this.defenseDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.defenseDataGridViewTextBoxColumn.ToolTipText = "Monsters defence power";
            this.defenseDataGridViewTextBoxColumn.Width = 150;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.typeDataGridViewTextBoxColumn.ToolTipText = "Monster cards type";
            this.typeDataGridViewTextBoxColumn.Width = 150;
            // 
            // cardTextDataGridViewTextBoxColumn
            // 
            this.cardTextDataGridViewTextBoxColumn.DataPropertyName = "Card_Text";
            this.cardTextDataGridViewTextBoxColumn.HeaderText = "Card_Text";
            this.cardTextDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.cardTextDataGridViewTextBoxColumn.Name = "cardTextDataGridViewTextBoxColumn";
            this.cardTextDataGridViewTextBoxColumn.ReadOnly = true;
            this.cardTextDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.cardTextDataGridViewTextBoxColumn.ToolTipText = "Effect text for the card (or flavour text if a normal monster)";
            this.cardTextDataGridViewTextBoxColumn.Width = 150;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            this.linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn.HeaderText = "Link";
            this.linkDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            this.linkDataGridViewTextBoxColumn.ReadOnly = true;
            this.linkDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.linkDataGridViewTextBoxColumn.ToolTipText = "Link monsters link rating";
            this.linkDataGridViewTextBoxColumn.Width = 150;
            // 
            // rankDataGridViewTextBoxColumn
            // 
            this.rankDataGridViewTextBoxColumn.DataPropertyName = "Rank";
            this.rankDataGridViewTextBoxColumn.HeaderText = "Rank";
            this.rankDataGridViewTextBoxColumn.MinimumWidth = 24;
            this.rankDataGridViewTextBoxColumn.Name = "rankDataGridViewTextBoxColumn";
            this.rankDataGridViewTextBoxColumn.ReadOnly = true;
            this.rankDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.rankDataGridViewTextBoxColumn.ToolTipText = "Xyz monsters rank";
            this.rankDataGridViewTextBoxColumn.Width = 150;
            // 
            // cardBindingSource1
            // 
            this.cardBindingSource1.DataSource = typeof(CardHub.Classes.Card);
            // 
            // cardBindingSource
            // 
            this.cardBindingSource.DataSource = typeof(CardHub.Classes.Card);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1159, 33);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitApplicationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.exitApplicationToolStripMenuItem.Text = "E&xit Application";
            this.exitApplicationToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click);
            // 
            // formMainHub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 682);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.packSelectGroupBox);
            this.Name = "formMainHub";
            this.Text = "Card Hub - Home";
            this.Load += new System.EventHandler(this.formMainHub_Load);
            this.packSelectGroupBox.ResumeLayout(false);
            this.packSelectGroupBox.PerformLayout();
            this.advancedDgvPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox packSelectGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox boosterPackSelect;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Panel advancedDgvPanel;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView1;
        private System.Windows.Forms.BindingSource cardBindingSource;
        private System.Windows.Forms.BindingSource cardBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn attributeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn attackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defenseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankDataGridViewTextBoxColumn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
    }
}