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
        private Chart myChart = new Chart();   // The chart "document"
        private BindingSource bs;              // For sending to the Entity Editor

        // these members are useful to avoid checking the entire list for the selected/highlighted one
        private Entity selectedEntity;            // Represents the entity selected with the left mouse button
        private Entity highlightedEntity;         // Represents the highlighted entity 
        private Link selectedLink;                // Represents the link selected with the left mouse button
        private Link highlightedLink;             // Represents the highlighted link
        private Attribute selectedAttribute;      // Represents the attribute selected with the left mouse button
        private Attribute highlightedAttribute;   // Represents the highlighted attribute


        private Entity movedEntity;            // Represents the entity being moved


        private Entity copyEntity;
        private Link copyLink;

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
            //selectedEntity?.Select(g);

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

        private void oneToOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.One2One); 
        }

        private void oneToManyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.One2Many);
        }

        private void manyToOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.Many2One);
        }

        private void manyToManyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.Many2Many);
        }

        #endregion

        #region Mouse events

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // clicked on an entity
                movedEntity = myChart.FindEntity(e.Location);
                mousePoint = (movedEntity != null ? e.Location : Point.Empty);
            }
            else if (e.Button == MouseButtons.Left)
            {
                mouseSelected = e.Location;

                // clicked on an entity
                var tempEntity = myChart.FindEntity(mouseSelected);
                selectedEntity = (Entity) ProcessSelection(tempEntity, selectedEntity);
                bs.Position = myChart.FindEntityPosition(selectedEntity);

                // clicked on a link
                var tempLink = myChart.FindLink(e.Location);
                selectedLink = (Link) ProcessSelection(tempLink, selectedLink);
                stbLink.Enabled = ( selectedLink != null );
                
                
                // clicked on an attribute
                var tempAttribute = selectedEntity?.FindAttribute(e.Location);
                selectedAttribute = (Attribute) ProcessSelection(tempAttribute, selectedAttribute);
                if (selectedAttribute != null)
                    DoDragDrop(selectedAttribute, DragDropEffects.Copy | DragDropEffects.Move);

                // clicked on an empty space = background
                if (tempLink == null && tempAttribute == null && selectedEntity == null)
                    DoDragDrop(DragDropObject, DragDropEffects.Copy);

                UpdateStatusBar();
            }
            Invalidate(true);
        }



        /* Handle mouse move moving an Entity around with the right button.
         * and the highlighting process
         */

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // if it is over an entity
            var tempEnt = myChart.FindEntity(e.Location);
            highlightedEntity = (Entity) ProcessHighLighting(tempEnt, highlightedEntity);
            
            // if it is over an attribute
            var tempAttribute = tempEnt?.FindAttribute(e.Location);
            highlightedAttribute = (Attribute) ProcessHighLighting(tempAttribute, highlightedAttribute);

            //if it is over a link
            var tempLink = myChart.FindLink(e.Location);
            highlightedLink = (Link) ProcessHighLighting(tempLink, highlightedLink);

            // moving an entity
            if (mousePoint != Point.Empty) MouseMoveObject(movedEntity, e.Location);

            // repaint
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
                movedEntity = null;
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

        // if the attribute is dropped in the backgorund then is removed
        // if dropped on an entity that does not have it then 
        // is added in the position of the mouse 
        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            var draggedAttribute = (Attribute)e.Data.GetData(typeof(Attribute));
            var dropPoint = PointToClient(new Point(e.X, e.Y));
            var tempEntity = myChart.FindEntity(dropPoint);
            if (tempEntity == null)
            {
                selectedEntity.DeleteAttribute(draggedAttribute);
                myChart.DestroyLinks();
                return;
            }
            var tempAttribute = tempEntity.FindAttribute(dropPoint);
            tempEntity.AddAttributeAfter( new Attribute(draggedAttribute),tempAttribute);

            myChart.AddLink(new Link(draggedAttribute, tempAttribute, Relationship.One2One));

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
            {
                var tempEntity = (Entity)bs.Current;
                selectedEntity = (Entity) ProcessSelection(tempEntity, selectedEntity);
            }
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

        // this method is key to avoid iterations in the collections to highlight/select
        // it reduces the highlight/select process from O(n) to O(1)
        private Element ProcessHighLighting(Element tempElement, Element highlightedElement)
        {
            highlightedElement?.ClearHighLight();      // clear the last selection/highlight if any
            tempElement?.Highlight();                  // highlight this element if any
            return tempElement;                // return this element
        }

        // this method is key to avoid iterations in the collections to highlight/select
        // it reduces the highlight/select process from O(n) to O(1)
        private Element ProcessSelection(Element tempElement, Element selectedElement)
        {
            selectedElement?.ClearSelection();      // clear the last selection/highlight if any
            tempElement?.Select();                  // select this element if any
            return tempElement ?? null;                // return this element
        }


        /*
        private Element ProcessActions(Element temElement, Element currentElement, 
                                Action<Element> clearAction, Action<Element> setAction)
        {
            currentElement?.
        }
        */

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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyEntity = new Entity(selectedEntity);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myChart.AddEntity(copyEntity);
            copyEntity = null;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyEntity = new Entity(selectedEntity);
            copyLink = selectedLink;
            bs.Remove(selectedEntity);

            //removes the links to the imaginary
            myChart.DestroyLinks();
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedEntity.ClearHighLight();
            bs.Remove(selectedEntity);
            myChart.DestroyLinks();
        }




        #endregion

        
    }
}


