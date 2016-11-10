using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace ERObjects
{
    [Serializable]
    public class Entity : Element
    {
        public event EventHandler HandleChange;
        public string Name { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Font Font { get; set; }
        public Color BackColor { get; set; }
        public Color NameColor { get; set; }
        public Color FrameColor { get; set; }
        

        public BindingList<Attribute> Attributes { get; }

        private RectangleF _nameRect = Rectangle.Empty;

        public Entity()
        {
            Attributes = new BindingList<Attribute>();
            Attributes.ListChanged += AttribsChanged;
        }

        private void AttribsChanged(object sender, ListChangedEventArgs e)
        {
            HandleChange?.Invoke(sender, e);
        }

        public Entity(string name, Point location, Size size, Font font, Color back, Color nameC, Color frame) : this()
        {
            Name = name;
            Location = location;
            Size = size;
            Font = font;
            BackColor = back;
            NameColor = nameC;
            FrameColor = frame;
            SelectedColor = Color.Red;
            IsHighlighted = false;
        }

        public Entity(string name, Point location, Size size, Font font) :
            this(name, location, size, font,
                Color.White, Color.Black, Color.Black)
        {
        }

        public Entity(string name) :
            this(name,
                new Point(10, 10),
                new Size(200, 200),
                new Font("Arial", 12),
                Color.White,
                Color.Black,
                Color.Black)
        {
        }


        public override bool Inside(Point location)
        {
            var r = new Rectangle(Location, Size);
            return r.Contains(location);
        }

        /* Returns true if the attribute list contains the 
         * passed Attribute
         */

        public bool HasAttribute(Attribute a)
        {
            return Attributes.Contains(a);
        }

        public override void Draw(Graphics g)
        {
            using (Brush textBrush = new SolidBrush(NameColor))
            {
                using (var framePen = new Pen(FrameColor))
                {
                    using (Brush backBrush = new SolidBrush(BackColor))
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
                        var factor = 0;
                        Attributes.ToList().ForEach( attribute =>
                        {
                            DrawAttrib(g, attribute, factor, backBrush);
                            factor++;
                        });
                        

                        var lastAttribute = Attributes.Last();
                        /* Resize based on number of attributes */
                        Size = new Size(Size.Width, (int) lastAttribute.Rect.Bottom - Location.Y);

                        /* Draw Frame */
                        g.DrawRectangle(framePen, new Rectangle(Location, Size));
                    }
                }
            }
        }

        /* Draws the entity's name */

        private void DrawName(Graphics g, Brush textBrush, Pen framePen, Brush backBrush)
        {
            var titleSize = new SizeF(Size.Width, Font.GetHeight());

            _nameRect = new RectangleF(Location, titleSize);

            var titleStringFormat = new StringFormat(StringFormatFlags.NoWrap);
            titleStringFormat.Trimming = StringTrimming.EllipsisCharacter;
            titleStringFormat.Alignment = StringAlignment.Center;
            titleStringFormat.LineAlignment = StringAlignment.Center;

            g.FillRectangle(IsHighlighted ? Brushes.Aquamarine : backBrush,
                _nameRect.X, _nameRect.Y, _nameRect.Width, _nameRect.Height);
            g.DrawRectangle(framePen, _nameRect.X, _nameRect.Y, _nameRect.Width, _nameRect.Height);
            g.DrawString(Name, Font, textBrush, _nameRect, titleStringFormat);
        }

        /* Draws the attributes in this entity, positioning them
         * based on the value passed in the factor parameter.
         */

        private void DrawAttrib(Graphics g, Attribute a, int factor, Brush backBrush)
        {
            using (Brush textBrush = new SolidBrush(a.TextColor))
            {
                var attribSize = new SizeF(Size.Width, a.Font.GetHeight());
                a.Rect = new RectangleF(Location.X, _nameRect.Bottom + factor*a.Font.GetHeight(), attribSize.Width,
                    attribSize.Height);
                var attribFormat = new StringFormat(StringFormatFlags.NoWrap);
                attribFormat.Trimming = StringTrimming.EllipsisCharacter;
                attribFormat.Alignment = StringAlignment.Near;
                attribFormat.LineAlignment = StringAlignment.Center;
                g.FillRectangle(backBrush, a.Rect.X, a.Rect.Y, a.Rect.Width, a.Rect.Height);
                g.DrawString(a.Name, a.Font, textBrush, a.Rect, attribFormat);
            }
        }

        public void AddAttribute(Attribute a)
        {
            Attributes.Add(a);
        }

        public override void Select(Graphics g)
        {
            using (var selectionPen = new Pen(SelectedColor, 5f))
            {
                g.DrawRectangle(selectionPen, new Rectangle(Location, Size));
            }
        }

       
    }
}
