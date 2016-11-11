namespace TextDesigner
{
    partial class BaseDialogForm
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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.classInfoControl = new ClassInfoControl();
            this.groupInfoControl = new GroupInfoControl();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.AutoSize = true;
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 56);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(334, 197);
            this.pnlContent.TabIndex = 2;
            // 
            // classInfoControl
            // 
            this.classInfoControl.BackColor = System.Drawing.Color.Transparent;
            this.classInfoControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.classInfoControl.Location = new System.Drawing.Point(0, 0);
            this.classInfoControl.Name = "classInfoControl";
            this.classInfoControl.Size = new System.Drawing.Size(334, 56);
            this.classInfoControl.TabIndex = 1;
            this.classInfoControl.Load += new System.EventHandler(this.classInfoControl1_Load);
            // 
            // groupInfoControl
            // 
            this.groupInfoControl.BackColor = System.Drawing.Color.Transparent;
            this.groupInfoControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupInfoControl.Location = new System.Drawing.Point(0, 253);
            this.groupInfoControl.Name = "groupInfoControl";
            this.groupInfoControl.Size = new System.Drawing.Size(334, 52);
            this.groupInfoControl.TabIndex = 0;
            // 
            // BaseDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(334, 305);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.classInfoControl);
            this.Controls.Add(this.groupInfoControl);
            this.Name = "BaseDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseDialogForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseDialogForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupInfoControl groupInfoControl;
        private ClassInfoControl classInfoControl;
        public System.Windows.Forms.Panel pnlContent;
    }
}