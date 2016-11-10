namespace TextDesigner
{
    partial class OathControl
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
            this.lblOath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOath
            // 
            this.lblOath.BackColor = System.Drawing.Color.Transparent;
            this.lblOath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOath.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblOath.Location = new System.Drawing.Point(0, 0);
            this.lblOath.Name = "lblOath";
            this.lblOath.Size = new System.Drawing.Size(496, 162);
            this.lblOath.TabIndex = 1;
            this.lblOath.Text = "I understand that this is a group project.\r\n\r\nIt is in my best interest to partic" +
    "ipate in writing the homework and study code from the homework.";
            this.lblOath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OathControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblOath);
            this.Name = "OathControl";
            this.Size = new System.Drawing.Size(496, 162);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblOath;
    }
}
