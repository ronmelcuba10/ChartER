using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERObjects;
using Attribute = ERObjects.Attribute; // Alias


namespace ChartViews
{
    public partial class GridViewer : Form
    {
        private BindingSource entityBS;

        public BindingManagerBase BindingManager
        {
            get { return this.BindingContext[entityBS]; }
        }

        public GridViewer()
        {
            InitializeComponent();
        }

        public GridViewer(BindingSource bs):this()
        {
            this.SetBindingSource(bs);
        }

        public void SetBindingSource(BindingSource bs)
        {
            entityBS = bs;

            dgvEntities.DataSource = this.entityBS;

            /* Bind attributes datagridview to the Attributes IBindingList
             * in the Entities IBindingList
             */
            dgvAttribs.DataSource = this.entityBS;
            dgvAttribs.DataMember = "Attributes";

            /* Hide unneeded columns */
            for (int i = 1; i < dgvEntities.Columns.Count; i++)
                dgvEntities.Columns[i].Visible = false;

            for (int i = 2; i < dgvAttribs.Columns.Count; i++)
                dgvAttribs.Columns[i].Visible = false;

          //  dgvAttribs.Columns[2].Visible = false;

        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
