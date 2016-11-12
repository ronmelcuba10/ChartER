using System;
using System.Collections.Generic;
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
        public Font Font { get; set; }
        public Color NameColor { get; set; }
        public Color FrameColor { get; set; }
        public BindingList<Attribute> Attributes { get; }
        public int KeysCount { get; set; }

        private Rectangle _nameRect = Rectangle.Empty;
        private Color backColor;

        public Color BackColor
        {
            get { return (KeysCount > 0 ? backColor : Color.LightPink); }
            set { backColor = value; }
        }

        public Entity()
        {
            Attributes = new BindingList<Attribute>();
            Attributes.ListChanged += AttribsChanged;
        }

        // Deep Cloning constructor
        public Entity(Entity a) : this()
        {
            this.Name = a.Name;
            this.Location = a.Location;
            this.Size = a.Size;
            this.Font = a.Font;
            this.BackColor = a.BackColor;
            this.NameColor = a.NameColor;
            this.FrameColor = a.FrameColor;
            this.SelectedColor = a.SelectedColor;
            this.IsHighlighted = a.IsHighlighted;

            foreach (Attribute attribute in a.Attributes)
            {
                this.Attributes.Add(new Attribute(attribute));
            }
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

        // Returns true if the attribute list contains the passed Attribute
        public bool HasAttribute(Attribute attribute) => Attributes.Contains(attribute);

        // Returns true if contains any attibute with that name
        public bool HasAttribute(string attributename)
        {
            return Attributes.FirstOrDefault(attribute => attribute.Name.Equals(attributename)) != null;
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
                        Attributes.ToList().ForEach(attribute => //DrawAttrib(g,factor)
                        {
                            DrawAttrib(g, attribute, factor, backBrush);
                            factor++;
                        });


                        var lastAttribute = Attributes.Last();
                        /* Resize based on number of attributes */
                        Size = new Size(Size.Width, lastAttribute.Location.Y + lastAttribute.Size.Height + -Location.Y);

                        /* Draw Frame */
                        g.DrawRectangle(framePen, new Rectangle(Location, Size));

                        if (IsSelected) Select();
                    }
                }
            }
        }

        /* Draws the entity's name */

        private void DrawName(Graphics g, Brush textBrush, Pen framePen, Brush backBrush)
        {

            var titleSize = new Size(Size.Width, (int) Font.GetHeight());
            _nameRect = new Rectangle(Location, titleSize);
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

        private void DrawAttrib(Graphics g, Attribute attribute, int factor, Brush backBrush)
        {
            attribute.Size = new Size(Size.Width, (int) attribute.Font.GetHeight());
            attribute.Location = new Point(Location.X, _nameRect.Bottom + factor*((int) attribute.Font.GetHeight()));
            attribute.BackBrush = backBrush;
            attribute.Draw(g);
        }

        // Adds an atttribute
        public void AddAttribute(Attribute attribute)
        {
            if (attribute.Key) KeysCount++;
            Attributes.Add(attribute);
        }

        public bool DeleteAttribute(Attribute attribute)
        {
            if (attribute.Key) KeysCount--;
            return Attributes.Remove(attribute);
        }

        // Finds the attribute located under the specified location
        public Attribute FindAttribute(Point loc) => (Attribute) FindElement(Attributes, loc);

        // Highlight the attribute under the specified location
        public void HighlightAttribute(Attribute tempAttribute) => HighLightElement(Attributes, tempAttribute);

        // Clear the highlighted attribute
        public void ClearHighLightedAttribute() => ClearHighLightedElement(Attributes);

        public void AddAttributeAfter(Attribute attribute, Attribute indexAttribute)
        {
            if (Attributes.Contains(attribute)) return;
            if (attribute.Key) KeysCount++;
            Attributes.Insert(Attributes.IndexOf(indexAttribute), attribute);
        }


    }
}
