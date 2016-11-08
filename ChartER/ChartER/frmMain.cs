using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERObjects;

namespace ChartER
{
    public partial class frmMain : Form
    {
        /* Mouse Stuff */
        Point mousePoint = Point.Empty; // This is used for moving via right mouse button
        Point mouseSelected = Point.Empty; // This is used for selecting via left mouse button

        /* Data Stuff */
        Chart myChart = new Chart(); // The chart "document"
        BindingSource bs; // For sending to the Entity Editor
        Entity selectedEntity = null; // Represents the entity selected with the left mouse button

        /* Form Stuff */
        private Rectangle selectedRect = Rectangle.Empty; // A rect for the selected Entity
        private Color selectedColor = Color.Red;
        
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Create a binding source based on myChart's list of Entites
            * and pass it to the entity editing form as its binding source
            */

            bs = new BindingSource();
            bs.DataSource = this.myChart.Entities;
        }

        /* Paint the current chart and selected Entity (if anyy)
         * Looks so simple, but go deeper into the rabbit hole...
         */
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            myChart.Draw(g);
            this.SelectEntity(g, selectedEntity);

 
        }

        /* Handle mouse down
         * If right button held down, we are going to move an entity around
         * the documents.
         * If left button is down, we selected an entity for highlighting
         * and for the editor
         */
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedEntity = myChart.FindEntity(e.Location);

                if (selectedEntity != null)
                    mousePoint = e.Location;
            }
            else if (e.Button == MouseButtons.Left)
            {
                selectedEntity = myChart.FindEntity(e.Location);
                bs.Position = myChart.FindEntityPosition(selectedEntity);
            }

            this.Invalidate(true);

        }

        /* Handle mouse move
         * Only handles moving an Entity around with the right button.
         * All other movements are ignored/not needed
         */
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePoint == Point.Empty)
                return;
            MouseMoveObject(selectedEntity, e.Location);

            this.Invalidate(true);

        }

        /* Move the selected object */
        private void MouseMoveObject(Entity ent, Point loc)
        {
            int dx;
            int dy;

            dx = loc.X - mousePoint.X;
            dy = loc.Y - mousePoint.Y;

            Point newLoc = new Point(ent.Location.X + dx, ent.Location.Y + dy);

            /* Account for the point at which we've selected object must move, too! */
            mousePoint.X += dx;
            mousePoint.Y += dy;

            ent.Location = newLoc;
        }
        private void Form1_Move(object sender, EventArgs e)
        {

        }

        /* Handle mouse up
         * If we were moving an Entity around, stop and reset the selected Entity
         */
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mousePoint = Point.Empty;
                selectedEntity = null;
                this.Invalidate(true);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            /* Create sample entities
             * Notice how we link attributes in entites: create a Link object, passing the source and destination
             * attributes to the Link's constructor
             */

            Entity ent1 = new Entity("Vehicle", new Point(10, 10), new Size(200, 200), new Font("Arial", 12), Color.White, Color.Black, Color.Blue);
            ent1.AddAttribute(new ERObjects.Attribute("VehicleID", new Font("Arial", 10)));
            ent1.AddAttribute(new ERObjects.Attribute("VehicleType", new Font("Arial", 10)));
            ent1.AddAttribute(new ERObjects.Attribute("LicensePlate", new Font("Arial", 10)));
            ent1.AddAttribute(new ERObjects.Attribute("DriverID", new Font("Arial", 10)));
            myChart.AddEntity(ent1);

            Entity ent2 = new Entity("Driver", new Point(50, 50), new Size(200, 200), new Font("Arial", 12), Color.White, Color.Black, Color.Blue);
            ent2.AddAttribute(new ERObjects.Attribute("DriverID", new Font("Arial", 10)));
            ent2.AddAttribute(new ERObjects.Attribute("Age", new Font("Arial", 10)));
            ent2.AddAttribute(new ERObjects.Attribute("Gender", new Font("Arial", 10)));
            myChart.AddEntity(ent2);

            Entity ent3 = new Entity("VehicleType", new Point(100, 100), new Size(200, 200), new Font("Arial", 12), Color.White, Color.Black, Color.Blue);
            ent3.AddAttribute(new ERObjects.Attribute("Make", new Font("Arial", 10)));
            ent3.AddAttribute(new ERObjects.Attribute("Model", new Font("Arial", 10)));
            ent3.AddAttribute(new ERObjects.Attribute("Year", new Font("Arial", 10)));
            myChart.AddEntity(ent3);

            Link l = new Link(ent1.Attributes[3], ent2.Attributes[0], Relationship.Many2One);
            myChart.AddLink(l);

            l = new Link(ent1.Attributes[1], ent3.Attributes[0], Relationship.One2One);
            myChart.AddLink(l);
            this.Invalidate(true);
        }

        /* Handler for Entity Editing */
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bs.Position = (selectedEntity) == null ? -1 : this.myChart.FindEntityPosition(selectedEntity);

            frmEntity entityEdit = new frmEntity();

            /* Keep track of changes */
            this.myChart.EntityChanged += EntityChangedByEditor;
            bs.PositionChanged += Bs_PositionChanged;
            entityEdit.FormClosed += EntityChangedByEditor;

            IformEntity iEntForm = entityEdit as IformEntity;
            iEntForm.SetBindingSource(bs);

            // If we're too close to the right edge of the screen, show prefs form on the left sife of main form
            if (Screen.PrimaryScreen.WorkingArea.Width - Right < entityEdit.Width)
                entityEdit.Left = this.Left - entityEdit.Width;
            else
                entityEdit.Left = this.Right;

            entityEdit.Top = this.Top;

            entityEdit.Show(this);

        }

        /* We changed the selected Entity in the editing window */
        private void Bs_PositionChanged(object sender, EventArgs e)
        {
            if (this.myChart.HasEntities())
                //selectedEntity = this.myChart.FindEntity(bs.Position);
                selectedEntity = (Entity)this.bs.Current;
            else
                selectedEntity = null;

            this.Invalidate(true);
        }

        /* User did something to the Entites in myChart */
        public void EntityChangedByEditor (object sender, EventArgs e)
        {
            /* Traverse the Links and destroy any that may now be
            * invalid due to an entity or attribute being deleted!
            */

            myChart.DestroyLinks();
            this.Invalidate(true);
        }


        /* Highlight currently selected Entity */
        public void SelectEntity(Graphics g, Entity e)
        {
            if (e == null)
                return;

            this.selectedRect = new Rectangle(e.Location, e.Size);
            using (Pen selectionPen = new Pen(this.selectedColor, 5f))
                g.DrawRectangle(selectionPen, this.selectedRect);
        }

    }
}
