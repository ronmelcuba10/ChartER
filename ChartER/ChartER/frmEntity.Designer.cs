namespace ChartER
{
    partial class frmEntity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEntity));
            this.txtName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblName = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddAtt = new System.Windows.Forms.Button();
            this.cbxKey = new System.Windows.Forms.CheckBox();
            this.lblAttName = new System.Windows.Forms.Label();
            this.tbxAttName = new System.Windows.Forms.TextBox();
            this.dgvAttribs = new System.Windows.Forms.DataGridView();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblNameColor = new System.Windows.Forms.Label();
            this.lblFrameColor = new System.Windows.Forms.Label();
            this.btnNameColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttribs)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(59, 50);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(262, 20);
            this.txtName.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtName, "Change name");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(19, 53);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(276, 327);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "OK";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(175, 327);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(89, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBackColor);
            this.groupBox1.Controls.Add(this.btnNameColor);
            this.groupBox1.Controls.Add(this.lblFrameColor);
            this.groupBox1.Controls.Add(this.lblNameColor);
            this.groupBox1.Controls.Add(this.btnForward);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 108);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(174, 22);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(147, 23);
            this.btnForward.TabIndex = 9;
            this.btnForward.Text = ">";
            this.toolTip.SetToolTip(this.btnForward, "Next Entity");
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(18, 22);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(147, 23);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "<";
            this.toolTip.SetToolTip(this.btnBack, "Previous entity");
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddAtt);
            this.groupBox2.Controls.Add(this.cbxKey);
            this.groupBox2.Controls.Add(this.lblAttName);
            this.groupBox2.Controls.Add(this.tbxAttName);
            this.groupBox2.Controls.Add(this.dgvAttribs);
            this.groupBox2.Location = new System.Drawing.Point(12, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 184);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attributes";
            // 
            // btnAddAtt
            // 
            this.btnAddAtt.Location = new System.Drawing.Point(260, 28);
            this.btnAddAtt.Name = "btnAddAtt";
            this.btnAddAtt.Size = new System.Drawing.Size(63, 23);
            this.btnAddAtt.TabIndex = 11;
            this.btnAddAtt.Text = "Add";
            this.btnAddAtt.UseVisualStyleBackColor = true;
            this.btnAddAtt.Click += new System.EventHandler(this.btnAddAtt_Click);
            // 
            // cbxKey
            // 
            this.cbxKey.AutoSize = true;
            this.cbxKey.Location = new System.Drawing.Point(210, 32);
            this.cbxKey.Name = "cbxKey";
            this.cbxKey.Size = new System.Drawing.Size(44, 17);
            this.cbxKey.TabIndex = 9;
            this.cbxKey.Text = "Key";
            this.cbxKey.UseVisualStyleBackColor = true;
            // 
            // lblAttName
            // 
            this.lblAttName.AutoSize = true;
            this.lblAttName.Location = new System.Drawing.Point(21, 33);
            this.lblAttName.Name = "lblAttName";
            this.lblAttName.Size = new System.Drawing.Size(35, 13);
            this.lblAttName.TabIndex = 8;
            this.lblAttName.Text = "Name";
            // 
            // tbxAttName
            // 
            this.tbxAttName.Location = new System.Drawing.Point(61, 30);
            this.tbxAttName.Name = "tbxAttName";
            this.tbxAttName.Size = new System.Drawing.Size(135, 20);
            this.tbxAttName.TabIndex = 7;
            // 
            // dgvAttribs
            // 
            this.dgvAttribs.AllowUserToAddRows = false;
            this.dgvAttribs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttribs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttribs.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvAttribs.Location = new System.Drawing.Point(24, 63);
            this.dgvAttribs.MultiSelect = false;
            this.dgvAttribs.Name = "dgvAttribs";
            this.dgvAttribs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttribs.Size = new System.Drawing.Size(299, 106);
            this.dgvAttribs.TabIndex = 6;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // lblNameColor
            // 
            this.lblNameColor.AutoSize = true;
            this.lblNameColor.Location = new System.Drawing.Point(15, 80);
            this.lblNameColor.Name = "lblNameColor";
            this.lblNameColor.Size = new System.Drawing.Size(62, 13);
            this.lblNameColor.TabIndex = 10;
            this.lblNameColor.Text = "Name Color";
            // 
            // lblFrameColor
            // 
            this.lblFrameColor.AutoSize = true;
            this.lblFrameColor.Location = new System.Drawing.Point(212, 80);
            this.lblFrameColor.Name = "lblFrameColor";
            this.lblFrameColor.Size = new System.Drawing.Size(63, 13);
            this.lblFrameColor.TabIndex = 11;
            this.lblFrameColor.Text = "Frame Color";
            // 
            // btnNameColor
            // 
            this.btnNameColor.Location = new System.Drawing.Point(83, 75);
            this.btnNameColor.Name = "btnNameColor";
            this.btnNameColor.Size = new System.Drawing.Size(41, 23);
            this.btnNameColor.TabIndex = 12;
            this.btnNameColor.UseVisualStyleBackColor = true;
            this.btnNameColor.Click += new System.EventHandler(this.btnNameColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Location = new System.Drawing.Point(281, 75);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(41, 23);
            this.btnBackColor.TabIndex = 13;
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // frmEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 362);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmEntity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Entity";
            this.Load += new System.EventHandler(this.frmEntity_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttribs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvAttribs;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnAddAtt;
        private System.Windows.Forms.CheckBox cbxKey;
        private System.Windows.Forms.Label lblAttName;
        private System.Windows.Forms.TextBox tbxAttName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.Button btnNameColor;
        private System.Windows.Forms.Label lblFrameColor;
        private System.Windows.Forms.Label lblNameColor;
    }
}