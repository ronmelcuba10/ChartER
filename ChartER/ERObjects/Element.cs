﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERObjects
{
    [Serializable]
    public abstract class Element
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Color SelectedColor { get; set; }
        public bool IsHighlighted { get; set; }
        public abstract void Draw(Graphics g);
        public void Highlight()
        {
            IsHighlighted = true;
        }
        public void ClearHighLight()
        {
            IsHighlighted = false;
        }

        public virtual bool Inside(Point location)
        {
            var r = new Rectangle(Location, Size);
            return r.Contains(location);
        }

        public virtual void Select(Graphics g)
        {
            using (var selectionPen = new Pen(SelectedColor, 3f))
            {
                g.DrawRectangle(selectionPen, new Rectangle(Location, Size));
            }
        }

        public void HighLightElement( IEnumerable<Element> list, Element element)
        {
            list.ToList().ForEach(currentelement =>
            {
                if (currentelement != element) currentelement.ClearHighLight();
                else currentelement.Highlight();
            });
        }

        public void ClearHighLightedElement( IEnumerable<Element> list)
        {
            list.ToList().ForEach(currentelement => currentelement.ClearHighLight());
        }

        public Element FindElement( IEnumerable<Element> list, Point loc)
        {
            return list.ToList().Find( element => element.Inside(loc));
        }
       
    }
}