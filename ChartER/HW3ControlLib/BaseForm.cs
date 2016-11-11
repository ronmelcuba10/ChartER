using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDesigner
{
    public partial class BaseForm : Form
    {

        public bool OathMenuAvailable
        {
            get
            {
               return oathToolStripMenuItem.Enabled;
            }
            set
            {
                oathToolStripMenuItem.Enabled = value;
            }
        }

        public bool AboutMenuAvailable
        {
            get
            {
                return aboutToolStripMenuItem.Enabled;
            }
            set
            {
                aboutToolStripMenuItem.Enabled = value;
            }
        }
        public BaseForm()
        {
            InitializeComponent();
        }

        protected void BaseForm_MouseDown(object sender, MouseEventArgs e)
        {

        }

        protected void BaseForm_MouseUp(object sender, MouseEventArgs e)
        {

        }

        protected void BaseForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }
  
        public virtual void oathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }

        public virtual void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }



    }
}
