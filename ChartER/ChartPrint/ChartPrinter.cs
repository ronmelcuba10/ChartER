using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ERObjects;

namespace ChartPrint
{
    public class ChartPrinter
    {
        private PrintDocument printDocument;
        private PrintDialog printDialog;
        private Chart printChart;
        private Font headerFont;
        public NotifyIcon NotifyIcon;

        public ChartPrinter()
        {
            this.printDocument = new PrintDocument();


            /* Print Dialog Setup */
            this.printDialog = new PrintDialog();
            this.printDialog.AllowSomePages = false;
            this.printDialog.AllowSelection = false;

            /* Register event handlers */
            this.printDocument.PrintPage += PrintDocument_PrintPage;
            this.printDocument.BeginPrint += PrintDocument_BeginPrint;
            this.printDocument.EndPrint += PrintDocument_EndPrint;
            this.printDocument.QueryPageSettings += PrintDocument_QueryPageSettings;
        }

        public void PrintChart (Chart chart, Font headerFont)
        {
            if (chart == null)
                return;

            

            if (chart.HasEntities())
            {

                this.printDialog.Document = printDocument;
 
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    this.headerFont = headerFont;
                    this.printDocument.DocumentName = chart.FileName;
                    this.printChart = chart;
                    this.printDocument.Print();
                }
            }
        }

        private void PrintDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
      
        }

        private void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            NotifyIcon.ShowBalloonTip(1000, "Printer", "Document ready", ToolTipIcon.None);
        }

        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            NotifyIcon.ShowBalloonTip(1000, "Printer", "Printing document", ToolTipIcon.None);
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            printChart.FileName = "Untitled"; // remove this for production

            /* Get printer's actual bounds */
            Rectangle visibleBounds = GetRealMarginBounds(e,false);
            g.DrawRectangle(Pens.LightGray, visibleBounds);

            /* Prepair and draw chart filename */
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Near;
            SizeF headerHeight = g.MeasureString(printChart.FileName, this.headerFont);
            g.DrawString(printChart.FileName, headerFont, Brushes.Red, visibleBounds, strFormat);
        
            /* Translate origin of graphics to top/left of bounds, accounting for filename stirng */
            g.TranslateTransform(visibleBounds.Top + headerHeight.Height, visibleBounds.Left);

            /* Scale based on chart size to fit one page */
            g.ScaleTransform(g.DpiX/printChart.Size.Width, 0.9f);

            /* Let 'er rip! */
            printChart.Draw(g);
        }

        private static Rectangle GetRealMarginBounds(PrintPageEventArgs e, bool preview)
        {
            if (preview)
                return e.MarginBounds;

            float cx = e.PageSettings.HardMarginX;
            float cy = e.PageSettings.HardMarginY;

            Rectangle marginBounds = e.MarginBounds;

            float dpiX = e.Graphics.DpiX;
            float dpiY =e.Graphics.DpiY;

            marginBounds.Offset((int)(-cx * 100 / dpiX), (int)(-cy * 100 / dpiY));

            return marginBounds;
        }
        
    }
}
