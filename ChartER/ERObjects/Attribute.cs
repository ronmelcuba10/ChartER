using System;
using System.Drawing;

namespace ERObjects
{
    [Serializable]
    public class Attribute : Element
    {
        public bool Key { get; set; }
        public string Name { get; set; }
        public Font Font { get; set; }
        public Color TextColor { get; set; }
        public Brush BackBrush { get; set; }
        public Rectangle Rect => new Rectangle(Location, Size);


        /* Must specify default constructor in order to 
         * add new attributes to entities via the attributes
         * bindinglist!
         */

        public Attribute(string name, bool isKey)
        {
            Name = name;
            Key = isKey;
            Font = new Font("Arial", 10);
            TextColor = Color.Black;
        }

        public Attribute(string name, bool isKey, Font font, Color textColor)
        {
            Name = name;
            Key = isKey;
            Font = font;
            TextColor = textColor;
        }

        public Attribute(string name, bool isKey, Font font) : this(name, isKey, font, Color.Black)
        {
            Key = isKey;
        }

        public override void Draw(Graphics g)
        {
            var format = new StringFormat(StringFormatFlags.NoWrap);
            format.Trimming = StringTrimming.EllipsisCharacter;
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            g.FillRectangle(IsHighlighted ? Brushes.LightBlue : BackBrush,
                Location.X, Location.Y, Size.Width, Size.Height);
            var tempFont = new Font("Impact", Font.Size, FontStyle.Bold);
            g.DrawString(Name, Key ? tempFont : Font,
                new SolidBrush(TextColor),
                new RectangleF(Location, Size), format);
        }
    }
}
