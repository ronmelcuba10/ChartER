using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ERObjects
{
    [Serializable]
    public enum Relationship
    {
        One2One = 0,
        One2Many = 1,
        Many2One = 2,
        Many2Many = 3
    }

    [Serializable]
    public enum StubType
    {
        One = 0,
        Many = 1
    }

    [Serializable]
    public enum LinkLocation
    {
        Left,
        Right
    }

    [Serializable]
    public enum LinkPen
    {
        Solid,
        Dotted,
        Dashed
    }

    [Serializable]
    public class Link : Element
    {
        public Attribute Source { get; set; }
        public Attribute Destination { get; set; }
        public Color LinkColor { get; set; }
        public LinkPen PenStyle { get; set; }
        public Stub SourceStub { get; set; }
        public Stub DestStub { get; set; }
        public Relationship Relationship { get; set; }
        public Font Font { get; set; }


        public Link()
        {
        }

        /* Creates a Link object between two attributes */

        public Link(Attribute a, Attribute b, Relationship aRel, Color linkColor, LinkPen penStyle, Font font)
        {
            Source = a;
            Destination = b;
            LinkColor = linkColor;
            PenStyle = penStyle;
            Relationship = aRel;
            Font = font;
            SelectedColor = Color.Red;


            SourceStub = new Stub(Source, ( aRel.GetHashCode() > 1 ? StubType.Many : StubType.One ) , 
                                    LinkLocation.Right, LinkColor, font);

            DestStub = new Stub(Destination, ( aRel.GetHashCode()%2 == 0 ? StubType.One : StubType.Many) , 
                                    LinkLocation.Left, LinkColor, font);
        }


        /* Creates Link objects between two attributes with default color and pentype */

        public Link(Attribute a, Attribute b, Relationship c) : 
            this(a, b, c, Color.BlueViolet , LinkPen.Dashed, new Font("Arial",12)) // using default a font may be changed later
        {
        }

        /* Draws the link on the passed graphics context */

        
        public override void Draw(Graphics g)
        {
            if ((Source == null) || (Destination == null)) return;

            using (var linkPen = new Pen(LinkColor))
            {
                DrawLink(g,linkPen);
                DrawStubs(g, linkPen);
            }
        }

        private void DrawStubs(Graphics g, Pen linkPen)
        {
            /* Place the stub on the correct side of the attribute depending
                     * on where it is in relation to its partner attribute */
            if (Source.Rect.Left == Destination.Rect.Left)
            {
                SourceStub.LinkLoc = LinkLocation.Left;
                DestStub.LinkLoc = LinkLocation.Left;
            }
            else if (Source.Rect.Left < Destination.Rect.Right / 2)
            {
                SourceStub.LinkLoc = LinkLocation.Right;
                DestStub.LinkLoc = LinkLocation.Left;
            }
            else if (Source.Rect.Left > Destination.Rect.Right / 2)
            {
                SourceStub.LinkLoc = LinkLocation.Left;
                DestStub.LinkLoc = LinkLocation.Right;
            }
            SourceStub.Draw(g);
            DestStub.Draw(g);
        }

        private void DrawLink(Graphics g, Pen linkPen)
        {
            switch (PenStyle)
            {
                case LinkPen.Solid:
                    linkPen.DashStyle = DashStyle.Solid;
                    break;
                case LinkPen.Dotted:
                    linkPen.DashStyle = DashStyle.Dot;
                    break;
                case LinkPen.Dashed:
                    linkPen.DashStyle = DashStyle.Dash;
                    break;
                default:
                    Console.WriteLine("WARN: No penstyle specified");
                    break;
            }
            g.DrawLine(linkPen, SourceStub.EndPoint, DestStub.EndPoint);
        }

        // needs to override to select both stubs
        public override void Select()
        {
            base.Select();
            SourceStub.Select();
            DestStub.Select();
        }

        // needs to override to clearselection both stubs
        public override void ClearSelection()
        {
            base.ClearSelection();
            SourceStub.Select();
            DestStub.Select();
        }

        public override void DrawSelected(Graphics g)
        {
            using (var selectionPen = new Pen(SelectedColor, 5f))
            {
                DrawLink(g, selectionPen);
                DrawStubs(g, selectionPen);
            }
        }

        // returns if the location is inside the link coordinates
        public override bool Inside(Point loc) => GetRectangle(SourceStub.EndPoint,DestStub.EndPoint).Contains(loc);

        // returns the rectangle formed by a widen line
        private Rectangle GetRectangle(PointF point1, PointF point2) => new Rectangle(Point.Empty, Size.Empty);
    }
}
