﻿namespace ChartER
{
    partial class frmNewEntity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewEntity));
            this.lblEntityName = new System.Windows.Forms.Label();
            this.tbxEntityName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxKey = new System.Windows.Forms.CheckBox();
            this.lbxAttributes = new System.Windows.Forms.ListBox();
            this.btnAddAttribute = new System.Windows.Forms.Button();
            this.tbxAttributeName = new System.Windows.Forms.TextBox();
            this.lblAttributeName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Location = new System.Drawing.Point(12, 23);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(35, 13);
            this.lblEntityName.TabIndex = 1;
            this.lblEntityName.Text = "Name";
            // 
            // tbxEntityName
            // 
            this.tbxEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxEntityName.Location = new System.Drawing.Point(53, 20);
            this.tbxEntityName.Name = "tbxEntityName";
            this.tbxEntityName.Size = new System.Drawing.Size(300, 20);
            this.tbxEntityName.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbxKey);
            this.groupBox1.Controls.Add(this.lbxAttributes);
            this.groupBox1.Controls.Add(this.btnAddAttribute);
            this.groupBox1.Controls.Add(this.tbxAttributeName);
            this.groupBox1.Controls.Add(this.lblAttributeName);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 182);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attributes";
            // 
            // cbxKey
            // 
            this.cbxKey.AutoSize = true;
            this.cbxKey.Location = new System.Drawing.Point(234, 26);
            this.cbxKey.Name = "cbxKey";
            this.cbxKey.Size = new System.Drawing.Size(44, 17);
            this.cbxKey.TabIndex = 6;
            this.cbxKey.Text = "Key";
            this.cbxKey.UseVisualStyleBackColor = true;
            // 
            // lbxAttributes
            // 
            this.lbxAttributes.FormattingEnabled = true;
            this.lbxAttributes.Location = new System.Drawing.Point(22, 63);
            this.lbxAttributes.Name = "lbxAttributes";
            this.lbxAttributes.Size = new System.Drawing.Size(300, 108);
            this.lbxAttributes.TabIndex = 5;
            // 
            // btnAddAttribute
            // 
            this.btnAddAttribute.Location = new System.Drawing.Point(296, 22);
            this.btnAddAttribute.Name = "btnAddAttribute";
            this.btnAddAttribute.Size = new System.Drawing.Size(23, 23);
            this.btnAddAttribute.TabIndex = 4;
            this.btnAddAttribute.Text = "+";
            this.btnAddAttribute.UseVisualStyleBackColor = true;
            this.btnAddAttribute.Click += new System.EventHandler(this.btnAddAttribute_Click);
            // 
            // tbxAttributeName
            // 
            this.tbxAttributeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxAttributeName.Location = new System.Drawing.Point(60, 24);
            this.tbxAttributeName.Name = "tbxAttributeName";
            this.tbxAttributeName.Size = new System.Drawing.Size(163, 20);
            this.tbxAttributeName.TabIndex = 3;
            // 
            // lblAttributeName
            // 
            this.lblAttributeName.AutoSize = true;
            this.lblAttributeName.Location = new System.Drawing.Point(19, 27);
            this.lblAttributeName.Name = "lblAttributeName";
            this.lblAttributeName.Size = new System.Drawing.Size(35, 13);
            this.lblAttributeName.TabIndex = 2;
            this.lblAttributeName.Text = "Name";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(109, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Add";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(190, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmNewEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 277);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbxEntityName);
            this.Controls.Add(this.lblEntityName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNewEntity";
            this.Text = "New Entity";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.TextBox tbxEntityName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddAttribute;
        private System.Windows.Forms.TextBox tbxAttributeName;
        private System.Windows.Forms.Label lblAttributeName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbxAttributes;
        private System.Windows.Forms.CheckBox cbxKey;
    }
}