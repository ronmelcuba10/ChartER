using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace ERObjects
{
    
    public class Entity
    {
        public event EventHandler HandleChange;
        public String Name { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Font Font { get; set; }
        public Color BackColor { get; set; }
        public Color NameColor { get; set; }
        public Color FrameColor { get; set; }
        public BindingList<Attribute> Attributes
        {
            get { return this.attributes; }
        }

        private RectangleF nameRect = Rectangle.Empty;

        BindingList<Attribute> attributes;

        public Entity()
        {
            attributes = new BindingList<Attribute>();
            attributes.ListChanged += AttribsChanged;

        }

        private void AttribsChanged(object sender, ListChangedEventArgs e)
        {
            if (HandleChange != null)
            {
                HandleChange(sender, e);

            }
        }

        public Entity(String name, Point location, Size size, Font font, Color back, Color nameC, Color frame) : this()
        {
            this.Name = name;
            this.Location = location;
            this.Size = size;
            this.Font = font;
            this.BackColor = back;
            this.NameColor = nameC;
            this.FrameColor = frame;
        }

        public Entity(String name, Point location, Size size, Font font) : this(name, location, size, font, Color.White, Color.Black, Color.Black)
        { }


        public bool Inside(Point location)
        {
            Rectangle r = new Rectangle(this.Location, this.Size);
            return r.Contains(location);
        }

        /* Returns true if the attribute list contains the 
         * passed Attribute
         */
        public bool HasAttribute(Attribute a)
        {
            return attributes.Contains(a);
        }

        public void Draw(Graphics g)
        {

            using (Brush textBrush = new SolidBrush(this.NameColor))
            using (Pen framePen = new Pen(this.FrameColor))
            using (Brush backBrush = new SolidBrush(this.BackColor))
            {
                /* Background */
                //g.FillRectangle(backBrush, new Rectangle(this.Location, this.Size));

                /* Draw Title */
                DrawName(g, textBrush, framePen, backBrush);

                /* Draw Attributes in the order they are found
                 * in the attributes list.
                 * We use the factor variable to keep track of where we
                 * are in the list, passed to the DrawAttrib method,
                 * so it can draw strings one under the other based
                 * on Font.GetHeight().
                 */
                int factor = 0;
                foreach (Attribute a in attributes)
                {
                    DrawAttrib(g, a, factor, backBrush);
                    factor++;
                }

                Attribute lastAttribute = attributes.Last();
                /* Resize based on number of attributes */
                this.Size = new Size(this.Size.Width, (int)(lastAttribute.Rect.Bottom) - this.Location.Y);

                /* Draw Frame */
                g.DrawRectangle(framePen, new Rectangle(this.Location, this.Size));

            }
        }

        /* Draws the entity's name */
        private void DrawName(Graphics g, Brush textBrush, Pen framePen, Brush backBrush)
        {

            SizeF titleSize = new SizeF(this.Size.Width, this.Font.GetHeight());

            nameRect = new RectangleF(this.Location, titleSize);

            StringFormat titleStringFormat = new StringFormat(StringFormatFlags.NoWrap);
            titleStringFormat.Trimming = StringTrimming.EllipsisCharacter;
            titleStringFormat.Alignment = StringAlignment.Center;
            titleStringFormat.LineAlignment = StringAlignment.Center;
              
            g.FillRectangle(backBrush, nameRect.X, nameRect.Y, nameRect.Width, nameRect.Height);
            g.DrawRectangle(framePen, nameRect.X, nameRect.Y, nameRect.Width, nameRect.Height);
            g.DrawString(this.Name, this.Font, textBrush, nameRect, titleStringFormat);
        }

        /* Draws the attributes in this entity, positioning them
         * based on the value passed in the factor parameter.
         */
        private void DrawAttrib(Graphics g, Attribute a, int factor, Brush backBrush)
        {
            using (Brush textBrush = new SolidBrush(a.TextColor)) {
                SizeF attribSize = new SizeF(this.Size.Width, a.Font.GetHeight());
                a.Rect = new RectangleF(this.Location.X, nameRect.Bottom + (factor * a.Font.GetHeight()), attribSize.Width, attribSize.Height);
                StringFormat attribFormat = new StringFormat(StringFormatFlags.NoWrap);
                attribFormat.Trimming = StringTrimming.EllipsisCharacter;
                attribFormat.Alignment = StringAlignment.Near;
                attribFormat.LineAlignment = StringAlignment.Center;
                g.FillRectangle(backBrush, a.Rect.X, a.Rect.Y, a.Rect.Width, a.Rect.Height);
                g.DrawString(a.Name, a.Font, textBrush, a.Rect, attribFormat);
            }
        }

        public void AddAttribute(Attribute a)
        {
            attributes.Add(a);
        }
        
    }
}
