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
            Font = font;
            SelectedColor = Color.Red;
            SetRelationship(aRel);
           
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
            var linkPen = (IsHighlighted ? new Pen(LinkColor, 5F) : new Pen(LinkColor));
            DrawLink(g, linkPen);
            DrawStubs(g, linkPen);
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

            SourceStub.Pen = linkPen;
            DestStub.Pen = linkPen;
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
            if(IsSelected) DrawSelected(g);
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
            using (var selectionPen = new Pen(SelectedColor, 7f))
            {
                g.DrawLine(selectionPen, SourceStub.EndPoint, DestStub.EndPoint); ;
                DrawStubs(g, selectionPen);
            }
        }

        // returns if the location is inside the link coordinates
        public override bool Inside(Point loc)
        {
            // distance to the line
            var x0 = loc.X;
            var y0 = loc.Y;
            var x1 = SourceStub.EndPoint.X;
            var y1 = SourceStub.EndPoint.Y;
            var x2 = DestStub.EndPoint.X;
            var y2 = DestStub.EndPoint.Y;

            var above = Math.Abs((y2 - y1) * x0 - (x2 - x1) * y0 + x2 * y1 - y2 * x1);
            var below = Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
            var distanceToLine = above / below;

            // but the line is infinitive
            var distanceToSource = Math.Sqrt((x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1));
            var distanceToDest = Math.Sqrt((x0 - x2) * (x0 - x2) + (y0 - y2) * (y0 - y2));

            // if the addition of these distances are close to the length of the segment
            // then we are close to the line
            var length = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));


            return distanceToLine < 10 && (distanceToSource + distanceToDest - length) < 20;
        }

        public void SetRelationship(Relationship relationship)
        {
            Relationship = relationship;
            SourceStub = new Stub(Source, (relationship.GetHashCode() > 1 ? StubType.Many : StubType.One),
                                    LinkLocation.Right, LinkColor, Font);

            DestStub = new Stub(Destination, (relationship.GetHashCode() % 2 == 0 ? StubType.One : StubType.Many),
                                    LinkLocation.Left, LinkColor, Font);
        }


    }
}
