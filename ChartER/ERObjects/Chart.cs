using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace ERObjects
{
    public class Chart
    {
        public event EventHandler EntityChanged;

        BindingList<Entity> entities;
        BindingList<Link> links;

        public BindingList<Entity> Entities
        {
            get { return entities; }
        }

        public BindingList<Link> Links
        {
            get { return links; }
        }

        public Chart()
        {
            entities = new BindingList<Entity>();
            links = new BindingList<Link>();

            entities.ListChanged += HandleChange;
            links.ListChanged += HandleChange;
            
        }

        /* Pass changes to Entity (including Attributes) and Links list */
        public void HandleChange (object sender, EventArgs e)
        {
            if (EntityChanged != null)
                EntityChanged(sender, e);
        }

        public void AddEntity(Entity e)
        {
            entities.Add(e);
            e.HandleChange += HandleChange; // handle changes to the attribute list in e
        }

        public void AddLink(Link l)
        {
            links.Add(l);
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (Entity ent in this.Entities)
                ent.Draw(g);
            foreach (Link l in this.Links)
                l.Draw(g);
        }

        /* Traverse list of entites backwards (for Z-Order)
         * to find one including the passed point
         */
        public Entity FindEntity(Point loc)
        {
            for(int i = entities.Count-1; i>=0; i--)
            {
                if (entities[i].Inside(loc))
                    return entities[i];
            }

            return null;
        }

        /* Find Entity at specified index
         */
        public Entity FindEntity(int i)
        {
            return entities.ElementAt(i);
        }

        public bool HasEntities()
        {
            return entities.Count > 0;
        }

        /* Traverse list of entites 
        * to find position
        */
        public int FindEntityPosition(Entity e)
        {
            return this.entities.IndexOf(e);
        }

        private bool ContainsAttribute (Attribute a)
        {
            bool foundAttribute=false;

            foreach (Entity e in this.Entities)
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
            List<Link> deadLinks = new List<Link>();
            
            foreach (Link l in this.Links)
            {
                Console.WriteLine($"{l.Source} = {ContainsAttribute(l.Source)} -- {l.Destination} = {ContainsAttribute(l.Destination)}");


                if (!ContainsAttribute(l.Source) || !ContainsAttribute(l.Destination))
                    deadLinks.Add(l);
            }

            foreach (Link l in deadLinks)
                this.Links.Remove(l);

            deadLinks.Clear();

        }


    }
}
