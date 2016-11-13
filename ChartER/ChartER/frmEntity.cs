using System;
using System.Windows.Forms;
using ERObjects;
using Attribute = ERObjects.Attribute; // Alias

namespace ChartER
{
    public partial class frmEntity : Form, IformEntity
    {
        private BindingSource entityBS;

        public BindingManagerBase BindingManager
        {
            get { return BindingContext[entityBS]; }
        }

        public frmEntity()
        {
            InitializeComponent();
        }

        public frmEntity(BindingSource entBS) : this()
        {
            SetBindingSource(entBS);
        }

        public void SetBindingSource(BindingSource bs)
        {
            entityBS = bs;

            txtName.DataBindings.Add("Text", entityBS, "Name");

            /* Bind attributes datagridview to the Attributes IBindingList
             * in the Entities IBindingList
             */
            dgvAttribs.DataSource = entityBS;
            dgvAttribs.DataMember = "Attributes";
 
            /*
            for (int i = 0; i < BindingManager.Count; i++)
                Console.WriteLine(BindingManager.Current);
                */
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            ++BindingManager.Position;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            --BindingManager.Position;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            /* Remove current Entity */

            entityBS.Remove(BindingManager.Current);

            if (entityBS.Count == 0)
                Close();

            BindingManager.Position++;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvAttribs.Rows.Remove(dgvAttribs.SelectedRows[0]);
        }

        private void frmEntity_Load(object sender, EventArgs e)
        {
            toolTip.SetToolTip(dgvAttribs, "Right click to remove attributes");
        }

        private void btnAddAtt_Click(object sender, EventArgs e)
        {
            var entity = (Entity) BindingManager.Current;
            if (entity.HasAttribute(txtName.Text))
            {
                MessageBox.Show(this, "That attribute is alrteady present", "Exisiting attribute", MessageBoxButtons.OK);
                return;
            }
            entity.AddAttribute(new Attribute(tbxAttName.Text, cbxKey.Checked));
            txtName.Clear();
            cbxKey.Checked = false;
        }
    }
}
