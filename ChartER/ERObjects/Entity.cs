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
        private string message;

        // validations and visual help
        public bool IsValid { get; set; }            // no error inside entity
        public string Message { get; set; }         


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
            this.BackColor = Color.White;
            this.NameColor = a.NameColor;
            this.FrameColor = a.FrameColor;
            this.SelectedColor = a.SelectedColor;
            this.KeysCount = a.KeysCount;
            

            foreach (Attribute attribute in a.Attributes)
            {
                this.Attributes.Add(new Attribute(attribute));
            }

            CheckValidity();
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
            this.KeysCount = 0;
            IsHighlighted = false;
            CheckValidity();
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
        // used to avoid duplicates in the entity set
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
                            if (attribute.Key) KeysCount++;
                            factor++;
                        });


                        var lastAttribute = ( Attributes.Count > 0 ? Attributes.Last() : null);
                        Size = lastAttribute != null ? new Size(Size.Width, lastAttribute.Location.Y + lastAttribute.Size.Height + -Location.Y) 
                                                        : new Size(Size.Width, Size.Height);

                        /* Draw Frame */
                        g.DrawRectangle(framePen, new Rectangle(Location, Size));

                        if (IsSelected) DrawSelected(g);
                    }
                }
            }
        }

        /* Draws the entity's name */
        private void DrawName(Graphics g, Brush textBrush, Pen framePen, Brush backBrush)
        {
            Size = new Size(Size.Width, (int) Font.GetHeight());
            _nameRect = new Rectangle(Location, Size);
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

        // Adds an atttribute at the end of the list and updates the keys count
        public void AddAttribute(Attribute attribute)
        {
            if (attribute.Key) KeysCount++;
            CheckValidity();
            Attributes.Add(attribute);
        }

        private void CheckValidity()
        {
            IsValid = KeysCount > 0;
            Message = (IsValid ? "" : "Missing key");
        }

        // Removes the attribute with that name and updates the keys count
        public void RemoveAttribute(Attribute attribute)
        {
            if (attribute.Key) KeysCount--;
            CheckValidity();
            var att = Attributes.First(currentattribute => currentattribute.Name.Equals(attribute.Name));
            Attributes.Remove(att);
        }
        
        // Finds the attribute located under the specified location
        public Attribute FindAttribute(Point loc) => (Attribute) FindElement(Attributes, loc);

        // return the index of the attribute with the name passed as parameter
        public int FindAttribute(string name)
        {
            var att = Attributes.FirstOrDefault(attribute => attribute.Name.Equals(name));
            return Attributes.IndexOf(att);
        }

        // Adds the attribute if there is no attribute with the same name  is already in the list
        public void AddAttributeAfter(Attribute attribute, Attribute indexAttribute)
        {
            if (HasAttribute(attribute.Name)) ReorderAttribute(attribute, indexAttribute);
            else InsertAttribute(attribute,indexAttribute);
        }

        // Reorder only if the attribute(with the specific name) is present
        private void ReorderAttribute(Attribute attribute, Attribute indexAttribute)
        {
            if(FindAttribute(attribute.Name) == FindAttribute(indexAttribute.Name) ) return;
            RemoveAttribute(attribute);
            InsertAttribute(attribute,indexAttribute);
        }

        // Inserts the attibute in the specified position in the list
        private void InsertAttribute(Attribute attribute, Attribute indexAttribute)
        {
            if (attribute.Key) KeysCount++;
            CheckValidity();
            if (Attributes.Count > 0) Attributes.Insert(Attributes.IndexOf(indexAttribute), attribute);
            else Attributes.Add(attribute);
        }
    }
}
