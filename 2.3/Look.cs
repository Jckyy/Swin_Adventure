using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Look : Command
    {
        public Look() : base(new string[] { "look" })
        {

        }


        public override string Execute(Player p, string[] text)
        {

            // Remember to use & since | will alawys make it run
            // Putting this check at the start will also disallow a null (string[] {})
            if (text.Length != 1 & text.Length != 3 & text.Length != 5)
            {
                return "I don't know how to look like that.";
            }

            if (!this.AreYou(text[0]))
            {
                return "Error in look input.";
            }


            if (text.Length == 3)
            { 
                if (text[1].ToLower() != "at")
                { 
                    return "What do you want to look at?";
                }
            }

            if (text.Length == 5)
            {
                // if statement inside since it causes out of index bounds exception
                if (text[3].ToLower() != "in")
                {
                    return "What do you want to look in?";
                }
            }

            IHaveInventory _container;

            switch (text.Length)
            {
                // "Look" at location
                case 1:
                    {
                        return String.Format("Current Location: {0}.\n\t{1}\n\n{2}",
                            p.Location.ShortDescription, 
                            p.Location.FullDescription, 
                            p.Location.ShowPaths(p));
                    }

                // "Look at item" and "Look at location"
                case 3:
                    {
                        // If no location OR id doesn't match location, search the player
                        if (p.Location == null | !p.Location.AreYou(text[2]))
                        {
                            // Cast the player to fit into _container
                            _container = p as IHaveInventory;
                            break;
                        }
                        
                        // Else return item list of the player's location.
                        return String.Format("In the {0}, there is:\n{1}", p.Location.Name, p.Location.Inventory.ItemList);
                    }

                // "Look at item in container"
                case 5:
                    {
                        _container = FetchContainer(p, text[4].ToLower());
                        if (_container == null)
                        {
                            return "I can't find the " + text[4] + ".";
                        }
                        break;
                    }
                default:
                    {
                        _container = null;
                        break;
                    }
            }

            string _itemID = text[2];

            return LookAtIn(_itemID, _container);

        }

        private IHaveInventory FetchContainer(Player p, string containerID)
        {
            // This casts the returned container as an IHaveInventory container
            return p.Locate(containerID) as IHaveInventory;
        }

        private string LookAtIn(string thingId, IHaveInventory container)
        {
            if (container.Locate(thingId) == null)
            {
                return String.Format("I can't find the {0}.", thingId);
            }
            return container.Locate(thingId).FullDescription;
            
        }
    }
}
