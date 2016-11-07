using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ERObjects
{
    public enum LinkLocation
    {
        Left,
        Right
    };

    public enum LinkPen
    {
        Solid,
        Dotted,
        Dashed
    };

    public class Link
    {
        public Attribute Source { get; set; }
        public Attribute Destination { get; set; }
        public Color LinkColor { get; set; }
        public LinkPen PenStyle { get; set; }
        public Stub SourceStub { get; set; }
        public Stub DestStub { get; set; }

        public Link()
        { }

        /* Creates a Link object between two attributes */
        public Link(Attribute a, Attribute b, Color linkColor, LinkPen penStyle) 
        {
            this.Source = a;
            this.Destination = b;
            this.LinkColor = linkColor;
            this.PenStyle = penStyle;

            this.SourceStub = new Stub(this.Source, LinkLocation.Right, this.LinkColor);
            this.DestStub = new Stub(this.Destination, LinkLocation.Left, this.LinkColor);
        }

        /* Creates Link objects between two attributes with default color and pentype */
        public Link(Attribute a, Attribute b) : this(a, b, Color.Red, LinkPen.Dashed)
        { }
        
        /* Draws the link on the passed graphics context */
        public void Draw(Graphics g)
        {
            if (this.Source != null && this.Destination != null)
            {
                using (Pen linkPen = new Pen(this.LinkColor))
                {
                    switch (this.PenStyle)
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
                     * on where it is in relateion to its partner attribute */
                    if (Source.Rect.Left == Destination.Rect.Left)
                    {
                        SourceStub.LinkLoc = LinkLocation.Left;
                        DestStub.LinkLoc = LinkLocation.Left;

                    } else if (Source.Rect.Left < Destination.Rect.Right / 2)
                    {
                        SourceStub.LinkLoc = LinkLocation.Right;
                        DestStub.LinkLoc = LinkLocation.Left;
                    } else if (Source.Rect.Left > Destination.Rect.Right / 2)
                    {
                        SourceStub.LinkLoc = LinkLocation.Left;
                        DestStub.LinkLoc = LinkLocation.Right;
                    }

                    this.SourceStub.Draw(g);
                    this.DestStub.Draw(g);

                    g.DrawLine(linkPen, this.SourceStub.EndPoint, this.DestStub.EndPoint);
                }
            }

        }

        /* The little straight line extending from the attribute */
        public class Stub
        {
            public PointF EndPoint { get; set; }
            public float StubLen { get; set; }
            public LinkLocation LinkLoc { get; set; }
            public Color StubColor { get; set; }
            private PointF startPoint;
            private Attribute Attribute;

            public Stub()
            { }
            public Stub (Attribute attrib, LinkLocation linkLoc, Color col):this()
            {
                this.StubLen = 5f;
                this.StubColor = col;
                this.Attribute = attrib;
                this.LinkLoc = linkLoc;
            }

            /* Draw the stub to the passed graphics context
             * Where it's draw depends on which side of the attribute we
             * specified via the linkLoc enum value
             */

            public void Draw(Graphics g)
            {
                this.startPoint = new PointF(
                        this.LinkLoc == LinkLocation.Right ? Attribute.Rect.Right : Attribute.Rect.Left,
                        Attribute.Rect.Top + Attribute.Rect.Height / 2);
                this.EndPoint = new PointF(
                        this.LinkLoc == LinkLocation.Left ? Attribute.Rect.Left - StubLen : Attribute.Rect.Right + StubLen,
                        Attribute.Rect.Top + Attribute.Rect.Height / 2);

                using (Pen stubPen = new Pen(this.StubColor))
                {
                    g.DrawLine(stubPen, this.startPoint, this.EndPoint);
                }
            }
        }
    }  
}
