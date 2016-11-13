using System;
using System.Windows.Forms;
using ERObjects;
using Attribute = ERObjects.Attribute;

namespace ChartER
{
    public partial class frmNewEntity : Form
    {
        public frmNewEntity()
        {
            InitializeComponent();
        }

        public Entity Entity()
        {
            var ent = new Entity(lblEntityName.Text);
            for (int i = 0; i < clbAttributes.Items.Count; i++)
            {
                var name = clbAttributes.Items[i].ToString();
                var key = clbAttributes.CheckedIndices.Contains(i);
                ent.AddAttribute(new Attribute(name, key));
            }
            return ent;
        }

        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            if (tbxAttributeName.Text.Equals(string.Empty)) return;
            if (clbAttributes.Items.Contains(tbxAttributeName.Text))
            {
                MessageBox.Show(this, "That attribute is alrteady present", "Exisiting attribute", MessageBoxButtons.OK);
                return;
            }
            clbAttributes.Items.Add(tbxAttributeName.Text, cbxKey.Checked);
            tbxAttributeName.Clear();
            cbxKey.Checked = false;
        }

        private void tbxAttributeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) btnAddAttribute_Click(sender, e);
        }

        private void tbxEntityName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) tbxAttributeName.Focus();
        }
    }
}
