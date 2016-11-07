using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ERObjects
{
    public class Attribute
    {
        public string Name { get; set; }
        public Font Font { get; set; }
        public Color TextColor { get; set; }
        public RectangleF Rect { get; set; }

        /* Must specify default constructor in order to 
         * add new attributes to entities via the attributes
         * bindinglist!
         */
        public Attribute()
        {
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
                
    }
}
