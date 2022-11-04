using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _inventory = new Inventory();
        private List<Path> _paths = new List<Path>();

        public Location(string[] ids, string name, string desc) : base(ids, name, desc)
        {

        }

        // Find item from inventory
        public GameObject Locate(string id)
        {
            // Return Location
            if (AreYou(id))
            {
                return this;
            }

            GameObject searchObject;

            searchObject = GetPath(id);
            
            if (searchObject == null)
            {
                // Return Inventory Item
                searchObject = _inventory.Fetch(id);
            }

            return searchObject;
        }


        // Inventory
        public Inventory Inventory
        {
            get { return _inventory; }
        }


        // Paths
        public void AddPath(Path path)
        {
            _paths.Add(path);
            path.Source = this;
        }

        private Path GetPath(string id)
        {
            // Return Path
            foreach (Path path in _paths)
            {
                if (path.AreYou(id))
                {
                    return path;
                }
            }

            foreach (Path path in _paths)
            {
                // AreYou path or path's destination
                if (path.AreYou(id) | path.Destination.AreYou(id))
                {
                    return path;
                }
            }

            // Second loop because I want to lower the priority of travelling back
            // to the source. If there are multiple paths I want to check those first
            //
            // 2 statements below are for moving back to original location
            foreach (Path path in _paths)
            {
                if (path.Source == null)
                {
                    break;
                }
                // AreYou source location or opposite direction of AreYou(id)
                else if (path.SourceDirection == id | path.Source.AreYou(id))
                {
                    return path;
                }
            }
            return null;
        }

        public string ShowPaths(Player p)
        {
            if (_paths.Count == 0)
            {
                return "There are no paths at your location";
            }


            string _pathList = "From your location, you can move:";
            foreach (Path path in _paths)
            {
                // Player's location != Path's destination
                if (p.Location != path.Destination)
                {
                    _pathList += String.Format("\n\t-{0}. {1}", path.Destination.ShortDescription, path.FullDescription);
                }
                // Player's location == Path's destination
                else if (p.Location == path.Destination)
                {
                    _pathList += String.Format("\n\t-{0}. {1}", path.Source.ShortDescription, String.Format("Take the {0} path to move to {1}", path.SourceDirection, path.Source.Name));
                }
                
            }
            return _pathList;
        }

        // Find and return a direction from a destination.
        public string FindDirection(string id)
        {
            foreach (Path path in _paths)
            {
                if (path.Destination.AreYou(id))
                {
                    return String.Format("{0} is {1} of {2}.",path.Destination.Name, path.Name, this.Name);
                }
            }
            return null;
        }
    }
}