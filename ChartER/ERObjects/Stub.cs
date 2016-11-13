using System;
using System.Drawing;

namespace ERObjects
{

    /* The little straight line extending from the attribute */
    [Serializable]
    public class Stub : Element
    {
        public PointF EndPoint { get; set; }
        public float StubLen { get; set; }
        public LinkLocation LinkLoc { get; set; }
        public Color StubColor { get; set; }
        public StubType StubType { get; set; }
        public Font Font { get; set; }
        public Pen Pen { get; set; }
        
        private PointF startPoint;
        private readonly Attribute Attribute;

        public Stub()
        {
        }

        public Stub(Attribute attrib, StubType stubtype, LinkLocation linkLoc, Color col, Font font) : this()
        {
            StubLen = 5f;
            StubColor = col;
            StubType = stubtype;
            Attribute = attrib;
            LinkLoc = linkLoc;
            Font = font;
        }

        /* Draw the stub to the passed graphics context
         * Where it's draw depends on which side of the attribute we
         * specified via the linkLoc enum value
         */

        public override void Draw(Graphics g)
        {
            startPoint = new PointF(
                LinkLoc == LinkLocation.Right ? Attribute.Rect.Right : Attribute.Rect.Left,
                Attribute.Rect.Top + Attribute.Rect.Height/2);
            EndPoint = new PointF(
                LinkLoc == LinkLocation.Left ? Attribute.Rect.Left - StubLen : Attribute.Rect.Right + StubLen,
                Attribute.Rect.Top + Attribute.Rect.Height/2);

            if (StubType == StubType.One) g.DrawLine( Pen, startPoint, EndPoint);
            else DrawTriangle(g);
            if(IsSelected)DrawSelected(g);
        }

        private void DrawTriangle(Graphics g)
        {
            var size = new Size(0, 5);
            var point1 = PointF.Add(startPoint, size);
            var point2 = PointF.Subtract(startPoint, size);
            var tempPen = (IsSelected ? new Pen(SelectedColor, 3f) : Pen );
            g.DrawLine(tempPen, EndPoint, point1);
            g.DrawLine(tempPen, point1, point2);
            g.DrawLine(tempPen, EndPoint, point2);
        }

        public override void DrawSelected(Graphics g)
        {
            if (StubType == StubType.One) g.DrawLine(Pen, startPoint, EndPoint);
            else DrawTriangle(g);
        }

        public override bool Inside(Point loc)
        {
            var size = new Size(0, 5);
            var point1 = PointF.Add(startPoint, size);
            var r = new RectangleF(point1,size);
            return r.Contains(loc);
            
        }

    }
}