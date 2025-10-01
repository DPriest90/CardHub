namespace CardHub.Forms
{
    partial class userControlTestForm
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
            this.filterControl1 = new CardHub.CustomConrtols.FilterControl();
            this.SuspendLayout();
            // 
            // filterControl1
            // 
            this.filterControl1.Location = new System.Drawing.Point(177, 87);
            this.filterControl1.Name = "filterControl1";
            this.filterControl1.Size = new System.Drawing.Size(419, 250);
            this.filterControl1.TabIndex = 0;
            // 
            // userControlTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.filterControl1);
            this.Name = "userControlTestForm";
            this.Text = "userControlTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomConrtols.FilterControl filterControl1;
    }
}