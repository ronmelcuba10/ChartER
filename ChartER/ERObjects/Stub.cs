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
                Attribute.Rect.Top + Attribute.Rect.Height / 2);
            EndPoint = new PointF(
                LinkLoc == LinkLocation.Left ? Attribute.Rect.Left - StubLen : Attribute.Rect.Right + StubLen,
                Attribute.Rect.Top + Attribute.Rect.Height / 2);

            using (var stubPen = new Pen(StubColor))
            {
                if (StubType == StubType.One) g.DrawLine(stubPen, startPoint, EndPoint);
                else DrawTriangle(g, stubPen);
            }
        }

        private void DrawTriangle(Graphics g, Pen p)
        {
            var size = new Size(0, 5);
            var point1 = PointF.Add(startPoint, size);
            var point2 = PointF.Subtract(startPoint, size);
            g.DrawLine(p, EndPoint, point1);
            g.DrawLine(p, point1, point2);
            g.DrawLine(p, EndPoint, point2);
        }

        public override void DrawSelected(Graphics g)
        {
            using (var selectionPen = new Pen(SelectedColor, 5f))
                DrawTriangle(g, selectionPen);
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