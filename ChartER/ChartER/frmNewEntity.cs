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
            foreach (var item in lbxAttributes.Items)
                ent.AddAttribute(new Attribute((string) item,cbxKey.Checked));
            return ent;
        }

        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            if (tbxAttributeName.Text.Equals(string.Empty)) return;
            if (!lbxAttributes.Items.Contains(tbxAttributeName.Text))
                lbxAttributes.Items.Add(tbxAttributeName.Text);
            tbxAttributeName.Clear();
        }
    }
}
