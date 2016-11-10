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
    public partial class BaseDialogForm : Form
    {

        /**
         * Public property to allow inheriting forms to inject content into middle panel
         **/
        public Panel ContentPanel
        {
            get { return this.pnlContent; }
            set { this.pnlContent = value;  }
        }
        public BaseDialogForm()
        {
            InitializeComponent();
            
        }

        private void classInfoControl1_Load(object sender, EventArgs e)
        {

        }

        private void BaseDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
