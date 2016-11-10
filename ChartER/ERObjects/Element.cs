using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERObjects
{
    [Serializable]
    public abstract class Element
    {
        public Color SelectedColor { get; set; }
        public bool IsHighlighted { get; set; }
        public abstract void Draw(Graphics g);
        public abstract bool Inside(Point location);
        public abstract void Select(Graphics g);
        public void Highlight()
        {
            IsHighlighted = true;
        }
        public void ClearHighLight()
        {
            IsHighlighted = false;
        }

        
    }
}
