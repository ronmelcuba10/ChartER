using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartER
{
    public partial class frmOath : TextDesigner.BaseDialogForm
    {
        public frmOath()
        {
            InitializeComponent();
        }

        private void DrawBackground()
        {

        }

        /*
         * Dynamically draw a background based on a PathGradientBrush
         */
        private void frmOath_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (GraphicsPath bgBrushPath = new GraphicsPath())
            {
                bgBrushPath.AddRectangle(new Rectangle(0, 0, 15, 15));

                using (PathGradientBrush bgBrush = new PathGradientBrush(bgBrushPath))
                {
                    bgBrush.WrapMode = WrapMode.Tile;
                    bgBrush.CenterColor = Color.White;
                    bgBrush.SurroundColors = new Color[] { Color.LightGray };
                    g.FillRectangle(bgBrush, this.ClientRectangle);
                }
            }
        }

        private void frmOath_Load(object sender, EventArgs e)
        {

        }
    }
}
