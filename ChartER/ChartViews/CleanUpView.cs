using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERObjects;
using System.ComponentModel;
using System.Drawing;

namespace ChartViews
{
    public class CleanUpView
    {
        public event EventHandler CleanUpStoped;

        private BackgroundWorker bgwker;
        private Chart myChart;

        public CleanUpView(Chart myChart)
        {
            bgwker = new BackgroundWorker();
            bgwker.DoWork += Bgwker_DoWork;
            bgwker.RunWorkerCompleted += Bgwker_RunWorkerCompleted;

            this.myChart = myChart;
        }

        private void Bgwker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                Console.Error.WriteLine("WARN: There was an error cleaning up the chart.");

            if (CleanUpStoped != null)
                CleanUpStoped(sender, null);

        }

        public void StartCleanUp()
        {
            bgwker.RunWorkerAsync(myChart);
        }

        private void Bgwker_DoWork(object sender, DoWorkEventArgs e)
        {
            CleanUpChart((Chart)e.Argument);
        }

        private void CleanUpChart(Chart myChart)
        {

            int newX = 10;
            int newY = 50;
            const int spacer = 10;
            int maxHeight = 0;

            Entity currentEntity;

            /* Iterate through Entities and position in a grid */
            for (int i = 0; i < myChart.Entities.Count; i++)
            {
                currentEntity = myChart.Entities.ElementAt(i);
                maxHeight = (maxHeight < currentEntity.Size.Height) ? currentEntity.Size.Height : maxHeight; // to properly space along Y axis

                /* If positioning the current Entity would overlap with chart width, move it to the next line */
                if (currentEntity.Size.Width + newX > myChart.Size.Width)
                {
                    newX = 10;
                    newY += maxHeight + spacer;
                }

                currentEntity.Location = new Point(newX, newY);
                newX += currentEntity.Size.Width + spacer;

                


            }        
        }

    }
}
