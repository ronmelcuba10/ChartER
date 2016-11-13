namespace ChartViews
{
    partial class GridViewer
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
            this.dgvEntities = new System.Windows.Forms.DataGridView();
            this.dgvAttribs = new System.Windows.Forms.DataGridView();
            this.bntClose = new System.Windows.Forms.Button();
            this.gboxEntities = new System.Windows.Forms.GroupBox();
            this.gBoxAttribs = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttribs)).BeginInit();
            this.gboxEntities.SuspendLayout();
            this.gBoxAttribs.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEntities
            // 
            this.dgvEntities.AllowUserToAddRows = false;
            this.dgvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvEntities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEntities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntities.Location = new System.Drawing.Point(3, 16);
            this.dgvEntities.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEntities.MultiSelect = false;
            this.dgvEntities.Name = "dgvEntities";
            this.dgvEntities.RowHeadersVisible = false;
            this.dgvEntities.RowTemplate.Height = 33;
            this.dgvEntities.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvEntities.Size = new System.Drawing.Size(208, 293);
            this.dgvEntities.TabIndex = 0;
            // 
            // dgvAttribs
            // 
            this.dgvAttribs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttribs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttribs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttribs.Location = new System.Drawing.Point(3, 16);
            this.dgvAttribs.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAttribs.MultiSelect = false;
            this.dgvAttribs.Name = "dgvAttribs";
            this.dgvAttribs.RowHeadersVisible = false;
            this.dgvAttribs.RowTemplate.Height = 33;
            this.dgvAttribs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAttribs.Size = new System.Drawing.Size(278, 290);
            this.dgvAttribs.TabIndex = 1;
            // 
            // bntClose
            // 
            this.bntClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bntClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntClose.Location = new System.Drawing.Point(438, 326);
            this.bntClose.Margin = new System.Windows.Forms.Padding(2);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(66, 29);
            this.bntClose.TabIndex = 2;
            this.bntClose.Text = "Close";
            this.bntClose.UseVisualStyleBackColor = true;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // gboxEntities
            // 
            this.gboxEntities.Controls.Add(this.dgvEntities);
            this.gboxEntities.Location = new System.Drawing.Point(3, 12);
            this.gboxEntities.Name = "gboxEntities";
            this.gboxEntities.Size = new System.Drawing.Size(214, 312);
            this.gboxEntities.TabIndex = 3;
            this.gboxEntities.TabStop = false;
            this.gboxEntities.Text = "Entities";
            // 
            // gBoxAttribs
            // 
            this.gBoxAttribs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBoxAttribs.Controls.Add(this.dgvAttribs);
            this.gBoxAttribs.Location = new System.Drawing.Point(223, 12);
            this.gBoxAttribs.Name = "gBoxAttribs";
            this.gBoxAttribs.Size = new System.Drawing.Size(284, 309);
            this.gBoxAttribs.TabIndex = 4;
            this.gBoxAttribs.TabStop = false;
            this.gBoxAttribs.Text = "Attributes";
            // 
            // GridViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 371);
            this.Controls.Add(this.gBoxAttribs);
            this.Controls.Add(this.gboxEntities);
            this.Controls.Add(this.bntClose);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridViewer";
            this.Text = "E-R Data";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttribs)).EndInit();
            this.gboxEntities.ResumeLayout(false);
            this.gBoxAttribs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEntities;
        private System.Windows.Forms.DataGridView dgvAttribs;
        private System.Windows.Forms.Button bntClose;
        private System.Windows.Forms.GroupBox gboxEntities;
        private System.Windows.Forms.GroupBox gBoxAttribs;
    }
}