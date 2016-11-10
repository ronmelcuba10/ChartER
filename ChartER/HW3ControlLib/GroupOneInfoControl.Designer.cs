namespace TextDesigner
{
    partial class GroupInfoControl
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
            this.lblGroupInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGroupInfo
            // 
            this.lblGroupInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblGroupInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGroupInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupInfo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblGroupInfo.Location = new System.Drawing.Point(0, 0);
            this.lblGroupInfo.Name = "lblGroupInfo";
            this.lblGroupInfo.Size = new System.Drawing.Size(340, 68);
            this.lblGroupInfo.TabIndex = 0;
            this.lblGroupInfo.Text = "Group 1: Ronny Alfonso, Armando Carrasquillo, Joe Cassara";
            this.lblGroupInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGroupInfo.Click += new System.EventHandler(this.lblGroupInfo_Click);
            // 
            // GroupInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.Controls.Add(this.lblGroupInfo);
            this.Name = "GroupInfoControl";
            this.Size = new System.Drawing.Size(340, 68);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGroupInfo;
    }
}
