using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ERObjects
{
    [Serializable]
    public class Chart
    {
        public event EventHandler EntityChanged;
        public BindingList<Entity> Entities { get; }
        public BindingList<Link> Links { get; }
        public bool Changed { get; set; }
        public string FileName { get; set; }
        public Size Size { get; set; }
        
        public Chart()
        {
            Entities = new BindingList<Entity>();
            Links = new BindingList<Link>();
            Changed = false;

            Entities.ListChanged += HandleChange;
            Links.ListChanged += HandleChange;
        }

        /* Pass changes to Entity (including Attributes) and Links list */
        public void HandleChange(object sender, EventArgs e) => EntityChanged?.Invoke(sender, e);

        public void AddEntity(Entity e)
        {
            Entities.Add(e);
            e.HandleChange += HandleChange; // handle changes to the attribute list in e
            Changed = true;
        }

        public void AddLink(Link l)
        {
            Links.Add(l);
            Changed = true;
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Entities.ToList().ForEach( entity => entity.Draw(g));
            Links.ToList().ForEach( link => link.Draw(g));
        }

        // Traverse list of entites backwards (for Z-Order) to find one including the passed point
        public Entity FindEntity(Point loc) => (Entity) Entities.Reverse().ToList().Find( entity => entity.Inside(loc));

        // Find the link in the specified location 
        public Link FindLink(Point loc) => (Link) Links.ToList().Find( link => link.Inside(loc));
        
        // Find Entity at specified index
        public Entity FindEntity(int i) => (Entity) Entities.ElementAt(i);

        public bool HasEntities() => Entities.Count > 0;

        // Traverse list of entites to find position
        public int FindEntityPosition(Entity e) => Entities.IndexOf(e);

        // Clear the highlighted link
        public void ClearHighLightedLink() => Links.ToList().ForEach(link => link.ClearHighLight());

        // Clear the highlighted entity
        public void ClearHighLightedEntity()
        {
            Entities.ToList().ForEach(entity =>
            {
                entity.ClearHighLight();
                entity.ClearHighLightedAttribute();
            });

        }

        private bool ContainsAttribute(Attribute a)
        {
            var foundAttribute = false;

            foreach (Entity e in Entities)
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

            foreach (Link link in Links)
            {
                Console.WriteLine(
                    $"{link.Source} = {ContainsAttribute(link.Source)} -- {link.Destination} = {ContainsAttribute(link.Destination)}");


                if (!ContainsAttribute(link.Source) || !ContainsAttribute(link.Destination))
                    deadLinks.Add(link);
            }

            foreach (var link in deadLinks)
                Links.Remove(link);

            deadLinks.Clear();
            Changed = true;
        }

        public void HighlightEntity(Entity tempEnt)
        {
            Entities.ToList().ForEach( entity =>
            {
                if (entity != tempEnt) entity.ClearHighLight();
                else entity.Highlight();
            });
        }

        public void HighlightLink(Link tempLink)
        {
            Links.ToList().ForEach(link =>
            {
                if (link != tempLink) link.ClearHighLight();
                else link.Highlight();
            });
        }

        

        public bool Save( string filename)
        {
            bool result;
            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    result = true;
                }
                catch (Exception ex) { result = false; }
            }
            return result;
        }

        public Chart Load(string filename)
        {
            Chart tempChart;
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    tempChart = (Chart)formatter.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    tempChart = null;
                }
            }
            return tempChart;
        }

        public void Clear()
        {
            Entities.Clear();
            Links.Clear();
            FileName = "Untitled.ctr";
            Changed = false;
        }
    }
}


