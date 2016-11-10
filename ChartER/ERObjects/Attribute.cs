using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ERObjects
{
    [Serializable]
    public class Attribute : Element
    {
        public string Name { get; set; }
        public Font Font { get; set; }
        public Color TextColor { get; set; }
        public Brush BackBrush { get; set; }
        public Rectangle Rect => new Rectangle(Location,Size);


        /* Must specify default constructor in order to 
         * add new attributes to entities via the attributes
         * bindinglist!
         */
        public Attribute( string name)
        {
            this.Name = name;
            this.Font = new Font("Arial", 10);
            this.TextColor = Color.Black;
        }

        public Attribute (String name, Font font, Color textColor)
        {
            this.Name = name;
            this.Font = font;
            this.TextColor = textColor;
        }

        public Attribute (String name, Font font) :this(name, font, Color.Black)
        {
        }

        public override void Draw(Graphics g)
        {
            var format = new StringFormat(StringFormatFlags.NoWrap);
            format.Trimming = StringTrimming.EllipsisCharacter;
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            g.FillRectangle(IsHighlighted ? Brushes.LightBlue : BackBrush, 
                                Location.X, Location.Y, Size.Width, Size.Height);
            g.DrawString( Name, Font, new SolidBrush(TextColor), 
                            new RectangleF(Location,Size), format);
        }
    }
}
