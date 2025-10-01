namespace CardHub.CustomConrtols
{
    partial class FilterControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filterControlBasePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // filterControlBasePanel
            // 
            this.filterControlBasePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterControlBasePanel.Location = new System.Drawing.Point(0, 0);
            this.filterControlBasePanel.Name = "filterControlBasePanel";
            this.filterControlBasePanel.Size = new System.Drawing.Size(419, 448);
            this.filterControlBasePanel.TabIndex = 0;
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.filterControlBasePanel);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(419, 448);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel filterControlBasePanel;
    }
}
