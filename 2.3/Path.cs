using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Path : GameObject
    {
        private Location _destination, _source;
        private bool _isBlocked;
        private string _sourceDirection;

        // This object needs to be able to move the player's location to a new location
        // Needs to be identifiable
            // Identifiers indicate the direction. 
            // Used to locate the path from the location

        public Path(string[] ids, Location destination) : base(ids, ids[0], String.Format("Take the {0} path to move to {1}.", ids[0], destination.Name))
        {
            _isBlocked = false;
            _destination = destination;

            // Set where the source direction is.
            switch (ids[0])
            {
                case "north":
                    case "up":
                    {
                        _sourceDirection = "south";
                        break;
                }
                case "south":
                case "down":
                    {
                        _sourceDirection = "north";
                        break;
                    }
                case "east":
                case "right":
                    {
                        _sourceDirection = "west";
                        break;
                    }
                case "west":
                case "left":
                    {
                        _sourceDirection = "east";
                        break;
                    }
            }
            _destination.AddPath(this);
        }


        // Want this to move the player to the destination, or return to the source
        // Take string ID as parameter so we can search the source and destination locations
        public string Move(Player p, string id)
        {
            // Check IsBlocked
            if (this.IsBlocked)
            {
                return "This path is blocked. Could not move the player";
            }
            // Check if the player is already at the destination
            if (p.Location == this.Destination)
            {
                // Check if this path has a source to back to.
                if (this.Source == null)
                {
                    return "Could not move the player.";
                }
                // If the id represents the source direction, we move the player
                if (this.SourceDirection == id | this.Source.AreYou(id))
                {
                    p.Location = this.Source;
                    return String.Format("{0} moved to {1}.", p.Name, p.Location.Name);
                }
                return "Could not move the player.";
            }

            // Check if this path is the correct direction
            // OR
            // If the destinations id matches
            if (this.AreYou(id) | this.Destination.AreYou(id))
            {
                p.Location = this.Destination;
                return String.Format("{0} moved to {1}.", p.Name, p.Location.Name);
            }          

            // Do not change player's location if invalid id.
            return "Could not move the player.";
        }

        public Location Destination
        {
            get { return _destination; }
        }
        
        public Location Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string SourceDirection
        {
            get { return _sourceDirection; }
        }

        public bool IsBlocked
        {
            get { return _isBlocked; }
            set { _isBlocked = value; }
        }
    }
}
