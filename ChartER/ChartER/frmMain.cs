using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using ChartPrint;
using ChartViews;
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

        // these members are useful to avoid checking the entire list for the selected/highlighted one
        private Entity selectedEntity; // Represents the entity selected with the left mouse button
        private Entity highlightedEntity; // Represents the highlighted entity 
        private Link selectedLink; // Represents the link selected with the left mouse button
        private Link highlightedLink; // Represents the highlighted link
        private Attribute selectedAttribute; // Represents the attribute selected with the left mouse button
        private Attribute highlightedAttribute; // Represents the highlighted attribute


        private Entity movedEntity; // Represents the entity being moved


        private Entity copyEntity;
        private Link copyLink;

        /* Form Stuff */
        private Rectangle selectedRect = Rectangle.Empty; // A rect for the selected Entity
        private Color selectedColor = Color.Red;
        private bool isCleaningUp; // inhibit painting while cleaning up

        /* Bitmap of chart */
        private Image currentBitmap;
        private DataObject dataObject;
        public Image CurrentBitmap => currentBitmap ?? (currentBitmap = new Bitmap(Width, Height));


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
            if (!isCleaningUp) // inhibit if we're cleaning up chart via threading
            {
                var g = e.Graphics;
                myChart.Draw(g);
                //selectedEntity?.Select(g);

                /* Update bitmap for drag-drop */
                var bg = Graphics.FromImage(CurrentBitmap);
                bg.Clear(Color.Transparent);
                myChart.Draw(bg);
                UpdateStatusBar();
            }
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
           
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            myChart.Size = Size;
            currentBitmap = null; // create a new one with new dimensions
        }

        private void oneToOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.One2One);
            Invalidate(true);
        }

        private void oneToManyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.One2Many);
            Invalidate(true);
        }

        private void manyToOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.Many2One);
            Invalidate(true);
        }

        private void manyToManyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedLink.SetRelationship(Relationship.Many2Many);
            Invalidate(true);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete) RemoveElement();
        }

        #endregion

        #region Mouse events

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // clicked on an entity
                movedEntity = myChart.FindEntity(e.Location);
                mousePoint = movedEntity != null ? e.Location : Point.Empty;
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
                stbLink.Enabled = selectedLink != null;


                // clicked on an attribute
                var tempAttribute = selectedEntity?.FindAttribute(e.Location);
                selectedAttribute = (Attribute) ProcessSelection(tempAttribute, selectedAttribute);
                if (selectedAttribute != null)
                    DoDragDrop(selectedAttribute, DragDropEffects.Copy | DragDropEffects.Move);

                // clicked on an empty space = background
                if ((tempLink == null) && (tempAttribute == null) && (selectedEntity == null))
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
            var draggedAttribute = (Attribute) e.Data.GetData(typeof(Attribute));
            var dropPoint = PointToClient(new Point(e.X, e.Y));
            var tempEntity = myChart.FindEntity(dropPoint);
            if (tempEntity == null)
            {
                selectedEntity.RemoveAttribute(draggedAttribute);
                myChart.DestroyLinks();
                return;
            }
            // if in the same entity the reorder both attributes
            var tempAttribute = tempEntity.FindAttribute(dropPoint);
            if (selectedEntity == tempEntity) selectedEntity.ReorderAttribute(draggedAttribute, tempAttribute);
            else
            {   // if different entities inset att and create a new default link (OnetoOne)
                var copiedAttrributed = new Attribute(draggedAttribute);
                tempEntity.AddAttributeAfter(copiedAttrributed, tempAttribute);
                myChart.AddLink(new Link(draggedAttribute, copiedAttrributed, Relationship.One2One));
            }
            
            Invalidate(true);
        }

        public DataObject DragDropObject
        {
            get
            {
                if (dataObject == null)
                    dataObject = new DataObject();
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
                var tempEntity = (Entity) bs.Current;
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

            var tempEntity = (Entity)bs.Current;
            tempEntity.ResetKeysCount();
            myChart.DestroyLinks();
            Invalidate(true);
        }

        private void HandleGridViewClose(object sender, EventArgs e)
        {
            eRGridToolStripMenuItem.Enabled = true;
        }

        private void UpdateStatusBar()
        {
            var tempEntity = (Entity) bs.Current;
            stbEntityName.Text = tempEntity?.Name;
            stbAtts.Text = tempEntity?.Attributes.Count.ToString();
            stblblEntityMsg.Text = tempEntity?.Message;
            if (tempEntity != null) stblblEntityMsg.BackColor = tempEntity.BackColor;
        }

        // this method is key to avoid iterations in the collections to highlight/select
        // it reduces the highlight/select process from O(n) to O(1)
        private Element ProcessHighLighting(Element tempElement, Element highlightedElement)
        {
            highlightedElement?.ClearHighLight(); // clear the last selection/highlight if any
            tempElement?.Highlight(); // highlight this element if any
            return tempElement; // return this element
        }

        // this method is key to avoid iterations in the collections to highlight/select
        // it reduces the highlight/select process from O(n) to O(1)
        private Element ProcessSelection(Element tempElement, Element selectedElement)
        {
            selectedElement?.ClearSelection(); // clear the last selection/highlight if any
            tempElement?.Select(); // select this element if any
            return tempElement ?? null; // return this element
        }


        /*
        private Element ProcessActions(Element temElement, Element currentElement, 
                                Action<Element> clearAction, Action<Element> setAction)
        {
            currentElement?.
        }
        */

        private void RemoveElement()
        {
            if ((selectedEntity == null) && (selectedLink == null)) return;
            var result = MessageBox.Show("Are you sure you want to remove this element",
                "Remove element", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;
            myChart.RemoveElement(selectedEntity);
            myChart.RemoveElement(selectedLink);
            Invalidate(true);
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
            entityEdit.FormClosed += EntityEdit_FormClosed;

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

        private void EntityEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            EntityChangedByEditor(sender, null);

            /* Unregister Handlers */
            myChart.EntityChanged -= EntityChangedByEditor;
            bs.PositionChanged -= Bs_PositionChanged;
         
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
            var ofd = new OpenFileDialog();
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
            if (!confirmUnsavedWork())
                return;

            myChart.Clear();
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
            using (var oath = new frmOath())
            {
                oath.ShowDialog();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var about = new frmAbout())
            {
                about.ShowDialog();
            }
        }


        public PrintEventHandler ShowBallon;
        public PrintEventHandler HideBallon;


        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedEntity?.ClearHighLight();
            var chartPrinter = new ChartPrinter();
            chartPrinter.NotifyIcon = notifyIcon;
            using (var headerFont = new Font("Arial", 12))
            {
                chartPrinter.PrintChart(myChart, headerFont);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedEntity != null)
                copyEntity = new Entity(selectedEntity);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (copyEntity != null)
            {
                copyEntity.IsSelected = false;
                myChart.AddEntity(copyEntity);
                copyEntity = null;
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedEntity != null)
            {
                copyEntity = new Entity(selectedEntity);
                copyLink = selectedLink;
                myChart.RemoveElement(selectedEntity);

                //removes the links to the imaginary
                myChart.DestroyLinks();
            }
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedEntity == null) return;
            selectedEntity.ClearHighLight();
            myChart.RemoveElement(selectedEntity);
            myChart.DestroyLinks();
        }

        /* Handle E-R Grid View */

        private void eRGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gv = new GridViewer(bs);

            /* Keep track of changes */
            myChart.EntityChanged += EntityChangedByEditor;
            bs.PositionChanged += Bs_PositionChanged;
            gv.FormClosed += EntityChangedByEditor;
            gv.FormClosed += HandleGridViewClose;

            gv.Show(this);

            eRGridToolStripMenuItem.Enabled = false;
        }

        private void CleanUpView_CleanUpStoped(object sender, EventArgs e)
        {
            isCleaningUp = false;
            cleanUpToolStripMenuItem.Enabled = true;
        }

      

        private void cleanUpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            isCleaningUp = true;
            var cleanUpView = new CleanUpView(myChart);
            cleanUpView.CleanUpStoped += CleanUpView_CleanUpStoped;
            cleanUpToolStripMenuItem.Enabled = false;
            cleanUpView.StartCleanUp();
        }


        #endregion
    }
}


