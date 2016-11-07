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

namespace ChartER
{
    public partial class frmEntity : Form, IformEntity
    {
        private BindingSource entityBS;

        public BindingManagerBase BindingManager
        {
            get { return this.BindingContext[entityBS]; }
        }
        public frmEntity()
        {
            InitializeComponent();
        }

        public frmEntity(BindingSource entBS):this()
        {
            this.SetBindingSource(entBS);
        }

        public void SetBindingSource(BindingSource bs)
        {
            this.entityBS = bs;

            txtName.DataBindings.Add("Text", entityBS, "Name");

            /* Bind attributes datagridview to the Attributes IBindingList
             * in the Entities IBindingList
             */
            dgvAttribs.DataSource = entityBS;
            dgvAttribs.DataMember = "Attributes";

            for (int i = 0; i < BindingManager.Count; i++)
                Console.WriteLine(BindingManager.Current);



        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            ++this.BindingManager.Position;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            --this.BindingManager.Position;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            /* Remove current Entity */

            entityBS.Remove(BindingManager.Current);

            if (entityBS.Count == 0)
                this.Close();

            BindingManager.Position++;
        }
    }
}
