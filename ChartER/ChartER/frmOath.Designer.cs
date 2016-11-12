namespace ChartER
{
    partial class frmOath
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
            this.oathControl1 = new TextDesigner.OathControl();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.oathControl1);
            // 
            // oathControl1
            // 
            this.oathControl1.BackColor = System.Drawing.Color.Transparent;
            this.oathControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oathControl1.Location = new System.Drawing.Point(0, 0);
            this.oathControl1.Name = "oathControl1";
            this.oathControl1.Size = new System.Drawing.Size(334, 197);
            this.oathControl1.TabIndex = 1;
            // 
            // frmOath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 305);
            this.Name = "frmOath";
            this.Text = "Oath";
            this.Load += new System.EventHandler(this.frmOath_Load);
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextDesigner.OathControl oathControl1;
    }
}