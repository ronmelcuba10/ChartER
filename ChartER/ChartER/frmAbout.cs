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
    public partial class frmAbout : TextDesigner.BaseDialogForm
    {
        private float colorAngle = 25.0f;

        public frmAbout()
        {
            InitializeComponent();
        }

        /*
        * Dynamically draw a border around client rect based on a LinearGradientBrush
        */
        private void frmAbout_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (LinearGradientBrush penBrush = new LinearGradientBrush(this.ClientRectangle, Color.Empty, Color.Empty, colorAngle))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { Color.DeepSkyBlue, Color.DarkSalmon, Color.DarkOliveGreen, Color.PapayaWhip };
                blend.Positions = new float[] { 0.0f, 0.4f, 0.8f, 1.0f };
                penBrush.InterpolationColors = blend;

                using (Pen rectPen = new Pen(penBrush, 10.0f))
                {
                    g.DrawRectangle(rectPen, this.ClientRectangle);
                }

            }

        }

        private void angleTimer_Tick(object sender, EventArgs e)
        {
            colorAngle = (colorAngle == 360.0f) ? colorAngle = 1.0f : colorAngle += 10.0f;
            this.Invalidate(true);
        }
    }
}
