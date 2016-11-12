using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChartPrint;
using ERObjects;
using Attribute = ERObjects.Attribute;


namespace ChartER
{
    public partial class frmMain : Form
    {

        #region Properties and Constructor

        /* Mouse Stuff */
        private Point mousePoint = Point.Empty; // This is used for moving via right mouse button
        private Point mouseSelected = Point.Empty; // This is used for selecting via left mouse button

        /* Data Stuff */
        private Chart myChart = new Chart(); // The chart "document"
        private BindingSource bs; // For sending to the Entity Editor
        private Entity selectedEntity; // Represents the entity selected with the left mouse button
        private Link selectedLink;     // Represents the link selected with the left mouse button

        /* Form Stuff */
        private Rectangle selectedRect = Rectangle.Empty; // A rect for the selected Entity
        private Color selectedColor = Color.Red;

        /* Bitmap of chart */
        private Image currentBitmap;
        private DataObject dataObject;
        public Image CurrentBitmap => currentBitmap ?? (currentBitmap = new Bitmap(this.Width, this.Height));


        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Form events methods

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Create a binding source based on myChart's list of Entites
            * and pass it to the entity editing form as its binding source
            */

            bs = new BindingSource();
            bs.DataSource = myChart.Entities;
        }

        /* Paint the current chart and selected Entity (if any)
         * Looks so simple, but go deeper into the rabbit hole...
         */

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            myChart.Draw(g);
            selectedEntity?.Select(g);

            /* Update bitmap for drag-drop */
            var bg = Graphics.FromImage(CurrentBitmap);
            bg.Clear(Color.Transparent);
            myChart.Draw(bg);
        }
    

        /* Handle mouse down
         * If right button held down, we are going to move an entity around
         * the documents.
         * If left button is down, we selected an entity for highlighting
         * and for the editor
         */

        private void Form1_Move(object sender, EventArgs e)
        {
        }

        /* Handle mouse up
         * If we were moving an Entity around, stop and reset the selected Entity
         */

        
        private void Form1_Shown(object sender, EventArgs e)
        {
            /* Create sample entities
             * Notice how we link attributes in entites: create a Link object, passing the source and destination
             * attributes to the Link's constructor
             */

            myChart.Size = this.Size;

            var ent1 = new Entity("Vehicle", new Point(10, 10), new Size(200, 200), new Font("Arial", 12),
                Color.White, Color.Black, Color.Blue);
            ent1.AddAttribute(new Attribute("VehicleID", true ,new Font("Arial", 10)));
            ent1.AddAttribute(new Attribute("VehicleType", false, new Font("Arial", 10)));
            ent1.AddAttribute(new Attribute("LicensePlate", false, new Font("Arial", 10)));
            ent1.AddAttribute(new Attribute("DriverID", false, new Font("Arial", 10)));
            myChart.AddEntity(ent1);

            var ent2 = new Entity("Driver", new Point(50, 50), new Size(200, 200), new Font("Arial", 12), Color.White,
                Color.Black, Color.Blue);
            ent2.AddAttribute(new Attribute("DriverID", true, new Font("Arial", 10)));
            ent2.AddAttribute(new Attribute("Age", false, new Font("Arial", 10)));
            ent2.AddAttribute(new Attribute("Gender", false, new Font("Arial", 10)));
            myChart.AddEntity(ent2);

            var ent3 = new Entity("VehicleType", new Point(100, 100), new Size(200, 200), new Font("Arial", 12),
                Color.White, Color.Black, Color.Blue);
            ent3.AddAttribute(new Attribute("Make", false, new Font("Arial", 10)));
            ent3.AddAttribute(new Attribute("Model", false, new Font("Arial", 10)));
            ent3.AddAttribute(new Attribute("Year", false, new Font("Arial", 10)));
            myChart.AddEntity(ent3);

            var l = new Link(ent1.Attributes[3], ent2.Attributes[0], Relationship.Many2One);
            myChart.AddLink(l);

            l = new Link(ent1.Attributes[1], ent3.Attributes[0], Relationship.One2One);

            myChart.AddLink(l);
            Invalidate(true);
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            myChart.Size = this.Size;
            currentBitmap = null; // create a new one with new dimensions
        }

        #endregion

        #region Mouse events

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedEntity = myChart.FindEntity(e.Location);
                mousePoint = ( selectedEntity != null ? e.Location : Point.Empty);
            }
            else if (e.Button == MouseButtons.Left)
            {
                mouseSelected = e.Location;

                // clicked on an entity
                selectedEntity = myChart.FindEntity(mouseSelected);
                bs.Position = myChart.FindEntityPosition(selectedEntity);

                // clicked on a link
                var tempLink = myChart.FindLink(e.Location);
                //tempLink.Select();

                // clicked on an attribute
                var tempAttribute = selectedEntity?.FindAttribute(e.Location);
                if (tempAttribute != null)
                    DoDragDrop(new Attribute(tempAttribute), DragDropEffects.Copy | DragDropEffects.Move);

                // clicked on an empty space = background
                if ( tempLink == null && tempAttribute == null && selectedEntity == null)
                    DoDragDrop(DragDropObject, DragDropEffects.Copy);

                UpdateStatusBar();
            }
            Invalidate(true);
        }



        /* Handle mouse move
         * Only handles moving an Entity around with the right button.
         * All other movements are ignored/not needed
         */

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var tempEnt = myChart.FindEntity(e.Location);
            if (tempEnt != null) myChart.HighlightEntity(tempEnt);
            else myChart.ClearHighLightedEntity();

            var tempAttribute = tempEnt?.FindAttribute(e.Location);
            if (tempAttribute != null)
                tempEnt.HighlightAttribute(tempAttribute);
            else tempEnt?.ClearHighLightedAttribute();

            var tempLink = myChart.FindLink(e.Location);
            if (tempLink != null) myChart.HighlightLink(tempLink);
            else myChart.ClearHighLightedLink();

            if (mousePoint != Point.Empty) MouseMoveObject(selectedEntity, e.Location);

            Invalidate(true);
        }

        /* Move the selected object */

        private void MouseMoveObject(Entity ent, Point loc)
        {
            int dx;
            int dy;

            dx = loc.X - mousePoint.X;
            dy = loc.Y - mousePoint.Y;

            var newLoc = new Point(ent.Location.X + dx, ent.Location.Y + dy);

            /* Account for the point at which we've selected object must move, too! */
            mousePoint.X += dx;
            mousePoint.Y += dy;

            ent.Location = newLoc;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mousePoint = Point.Empty;
                selectedEntity = null;
                Invalidate(true);
            }
            if (e.Button == MouseButtons.Left)
                mouseSelected = Point.Empty;
        }

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Attribute)))
                e.Effect = DragDropEffects.Copy | DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            
            var dropPoint = PointToClient(new Point(e.X, e.Y));
            var tempEntity = myChart.FindEntity(dropPoint);
            if (tempEntity == null) return;
            var tempAttribute = tempEntity.FindAttribute(dropPoint);
            tempEntity.AddAttributeAfter( (Attribute) e.Data.GetData(typeof(Attribute)),tempAttribute);
            Invalidate(true);
            
        }

        public DataObject DragDropObject
        {
            get
            {
                if (dataObject == null)
                {
                    dataObject = new DataObject();
                }
                dataObject.SetImage(CurrentBitmap);
                return dataObject;
            }
        }

        #endregion

        #region Helpers

        /* We changed the selected Entity in the editing window */

        private void Bs_PositionChanged(object sender, EventArgs e)
        {
            if (myChart.HasEntities())
                //selectedEntity = this.myChart.FindEntity(bs.Position);
                selectedEntity = (Entity) bs.Current;
            else
                selectedEntity = null;

            Invalidate(true);
        }

        /* User did something to the Entites in myChart */

        public void EntityChangedByEditor(object sender, EventArgs e)
        {
            /* Traverse the Links and destroy any that may now be
            * invalid due to an entity or attribute being deleted!
            */
            myChart.DestroyLinks();
            Invalidate(true);
        }

        private void UpdateStatusBar()
        {
            stbEntityName.Text = ((Entity)bs.Current).Name;
            stbAtts.Text = ((Entity)bs.Current).Attributes.Count.ToString();
        }

        #endregion

        #region Menu options click

        /* Handler for Entity Editing */
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bs.Position = selectedEntity == null ? -1 : myChart.FindEntityPosition(selectedEntity);

            var entityEdit = new frmEntity();

            /* Keep track of changes */
            myChart.EntityChanged += EntityChangedByEditor;
            bs.PositionChanged += Bs_PositionChanged;
            entityEdit.FormClosed += EntityChangedByEditor;

            var iEntForm = entityEdit as IformEntity;
            iEntForm.SetBindingSource(bs);

            // If we're too close to the right edge of the screen, show prefs form on the left sife of main form
            if (Screen.PrimaryScreen.WorkingArea.Width - Right < entityEdit.Width)
                entityEdit.Left = Left - entityEdit.Width;
            else
                entityEdit.Left = Right;

            entityEdit.Top = Top;

            entityEdit.Show(this);
        }

        private void tsbNewEntity_Click(object sender, EventArgs e)
        {
            using (var ne = new frmNewEntity())
            {
                if (ne.ShowDialog() == DialogResult.OK)
                    myChart.AddEntity(ne.Entity());
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (confirmUnsavedWork()) Close();
        }

        private bool confirmUnsavedWork()
        {
            if (!myChart.Changed) return true;
            return DialogResult.No != MessageBox.Show("Continue without saving?", "Unsaved Changes",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!confirmUnsavedWork())
                return;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true; // add our file extension if user doesn't
            ofd.DefaultExt = "ctr";
            ofd.Filter = "ChartER File|*.ctr";

            if (DialogResult.OK == ofd.ShowDialog())
            {
                myChart = myChart.Load(ofd.FileName);
                if (myChart != null)
                {
                    myChart.Changed = false;
                    myChart.FileName = ofd.FileName;
                    Text = Path.GetFileName(myChart.FileName) + " - " + Application.ProductName;
                    Invalidate(true);
                }
                else
                    MessageBox.Show("There was an error.");
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myChart.Changed = false;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.AddExtension = true; // add our file extension if user doesn't
            sfd.DefaultExt = "ctr";
            sfd.Filter = "ChartER File|*.ctr";

            if (DialogResult.OK == sfd.ShowDialog())
                SaveChart(sfd.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myChart.FileName != null) // if not null, we've Saved As already...
                SaveChart(myChart.FileName);
            else
                saveAsToolStripMenuItem_Click(this, e); // call up the Save As dialog
        }

        private void SaveChart(string fileName)
        {
            myChart.FileName = fileName;
            Text = Path.GetFileName(myChart.FileName) + " - " + Application.ProductName;
            myChart.Save(fileName);
            myChart.Changed = false;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myChart.Clear();
            Invalidate(true);
        }

        private void oathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmOath oath = new frmOath())
            {
                oath.ShowDialog();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAbout about = new frmAbout())
            {
                about.ShowDialog();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var chartPrinter = new ChartPrinter();
            selectedEntity?.ClearHighLight();

            using (var headerFont = new Font("Arial", 12))
                chartPrinter.PrintChart(myChart, headerFont);
        }


        #endregion

    }
}


