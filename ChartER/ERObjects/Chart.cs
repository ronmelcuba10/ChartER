using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ERObjects
{
    public class Chart
    {
        public event EventHandler EntityChanged;

        public BindingList<Entity> Entities { get; }

        public BindingList<Link> Links { get; }

        public Chart()
        {
            Entities = new BindingList<Entity>();
            Links = new BindingList<Link>();

            Entities.ListChanged += HandleChange;
            Links.ListChanged += HandleChange;
        }

        /* Pass changes to Entity (including Attributes) and Links list */

        public void HandleChange(object sender, EventArgs e)
        {
            if (EntityChanged != null)
                EntityChanged(sender, e);
        }

        public void AddEntity(Entity e)
        {
            Entities.Add(e);
            e.HandleChange += HandleChange; // handle changes to the attribute list in e
        }

        public void AddLink(Link l)
        {
            Links.Add(l);
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (var ent in Entities)
                ent.Draw(g);
            foreach (var l in Links)
                l.Draw(g);
        }

        /* Traverse list of entites backwards (for Z-Order)
         * to find one including the passed point
         */

        public Entity FindEntity(Point loc)
        {
            for (var i = Entities.Count - 1; i >= 0; i--)
                if (Entities[i].Inside(loc))
                    return Entities[i];

            return null;
        }

        /* Find Entity at specified index
         */

        public Entity FindEntity(int i)
        {
            return Entities.ElementAt(i);
        }

        public bool HasEntities()
        {
            return Entities.Count > 0;
        }

        /* Traverse list of entites 
        * to find position
        */

        public int FindEntityPosition(Entity e)
        {
            return Entities.IndexOf(e);
        }

        private bool ContainsAttribute(Attribute a)
        {
            var foundAttribute = false;

            foreach (var e in Entities)
            {
                foundAttribute = e.HasAttribute(a);
                if (foundAttribute)
                    break;
            }

            return foundAttribute;
        }

        /* Checks for missing attributes/entities
         * and destroys links between them
         */

        public void DestroyLinks()
        {
            var deadLinks = new List<Link>();

            foreach (var l in Links)
            {
                Console.WriteLine(
                    $"{l.Source} = {ContainsAttribute(l.Source)} -- {l.Destination} = {ContainsAttribute(l.Destination)}");


                if (!ContainsAttribute(l.Source) || !ContainsAttribute(l.Destination))
                    deadLinks.Add(l);
            }

            foreach (var l in deadLinks)
                Links.Remove(l);

            deadLinks.Clear();
        }

        public void Highlighted(Entity tempEnt)
        {
            foreach (var entity in Entities)
                if (entity != tempEnt) entity.ClearHighLight();
                else entity.Highlight();
        }

        public void ClearHighLighted()
        {
            foreach (var entity in Entities)
                entity.ClearHighLight();
        }
    }
}
