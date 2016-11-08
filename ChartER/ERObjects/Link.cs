using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ERObjects
{
    public enum Relationship
    {
        One2One = 0,
        One2Many = 1,
        Many2One = 2,
        Many2Many = 3
    }

    public enum StubType
    {
        One = 0,
        Many = 1
    }

    public enum LinkLocation
    {
        Left,
        Right
    }

    public enum LinkPen
    {
        Solid,
        Dotted,
        Dashed
    }

    public class Link
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

            SourceStub = new Stub(Source, ( aRel.GetHashCode() > 1 ? StubType.Many : StubType.One ) , 
                                    LinkLocation.Right, LinkColor, font);

            DestStub = new Stub(Destination, ( aRel.GetHashCode()%2 == 0 ? StubType.One : StubType.Many) , 
                                    LinkLocation.Left, LinkColor, font);
        }


        /* Creates Link objects between two attributes with default color and pentype */

        public Link(Attribute a, Attribute b, Relationship c) : 
            this(a, b, c, Color.Red , LinkPen.Dashed, new Font("Arial",12)) // using default a font may be changed later
        {
        }

        /* Draws the link on the passed graphics context */

        public void Draw(Graphics g)
        {
            if ((Source != null) && (Destination != null))
                using (var linkPen = new Pen(LinkColor))
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

                    /* Place the stub on the correct side of the attribute depending
                     * on where it is in relation to its partner attribute */
                    if (Source.Rect.Left == Destination.Rect.Left)
                    {
                        SourceStub.LinkLoc = LinkLocation.Left;
                        DestStub.LinkLoc = LinkLocation.Left;
                    }
                    else if (Source.Rect.Left < Destination.Rect.Right/2)
                    {
                        SourceStub.LinkLoc = LinkLocation.Right;
                        DestStub.LinkLoc = LinkLocation.Left;
                    }
                    else if (Source.Rect.Left > Destination.Rect.Right/2)
                    {
                        SourceStub.LinkLoc = LinkLocation.Left;
                        DestStub.LinkLoc = LinkLocation.Right;
                    }

                    SourceStub.Draw(g);
                    DestStub.Draw(g);

                    g.DrawLine(linkPen, SourceStub.EndPoint, DestStub.EndPoint);
                }
        }

        /* The little straight line extending from the attribute */

        public class Stub
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

            public Stub(Attribute attrib, StubType stubtype,LinkLocation linkLoc, Color col, Font font) : this()
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

            public void Draw(Graphics g)
            {
                startPoint = new PointF(
                    LinkLoc == LinkLocation.Right ? Attribute.Rect.Right : Attribute.Rect.Left,
                    Attribute.Rect.Top + Attribute.Rect.Height/2);
                EndPoint = new PointF(
                    LinkLoc == LinkLocation.Left ? Attribute.Rect.Left - StubLen : Attribute.Rect.Right + StubLen,
                    Attribute.Rect.Top + Attribute.Rect.Height/2);

                using (var stubPen = new Pen(StubColor))
                {
                    if (StubType == StubType.One)
                    {
                        var Size = new Size(0, 5);
                        g.DrawLine(stubPen, startPoint, EndPoint);
                        g.DrawString("1", Font, new SolidBrush(StubColor), PointF.Add(EndPoint, Size));
                    }
                    else
                    {
                        var Size = new Size(0, 5);
                        g.DrawString("M", Font,new SolidBrush(StubColor),PointF.Add(EndPoint,new Size(-12,5)));
                        var Point1 = PointF.Add(startPoint, Size);
                        var Point2 = PointF.Subtract(startPoint, Size);
                        g.DrawLine(stubPen, EndPoint, Point1);
                        g.DrawLine(stubPen, Point1, Point2);
                        g.DrawLine(stubPen, EndPoint, Point2);
                    }
                }
            }
        }
    }
}
