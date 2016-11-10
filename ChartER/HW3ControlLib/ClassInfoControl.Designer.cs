namespace TextDesigner
{
    partial class ClassInfoControl
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
            this.lblClass = new System.Windows.Forms.Label();
            this.lblSemester = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblClass
            // 
            this.lblClass.BackColor = System.Drawing.Color.Transparent;
            this.lblClass.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblClass.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClass.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblClass.Location = new System.Drawing.Point(0, 0);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(255, 36);
            this.lblClass.TabIndex = 0;
            this.lblClass.Text = "Advanced Windows Programming";
            this.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClass.Click += new System.EventHandler(this.lblClass_Click);
            // 
            // lblSemester
            // 
            this.lblSemester.BackColor = System.Drawing.Color.Transparent;
            this.lblSemester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSemester.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSemester.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSemester.Location = new System.Drawing.Point(0, 36);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(255, 29);
            this.lblSemester.TabIndex = 1;
            this.lblSemester.Text = "Fall 2016";
            this.lblSemester.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClassInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblSemester);
            this.Controls.Add(this.lblClass);
            this.Name = "ClassInfoControl";
            this.Size = new System.Drawing.Size(255, 65);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblSemester;
    }
}
