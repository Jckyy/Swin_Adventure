using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory = new Inventory();
        private Location _location;
        
        // When you create a new player, a default name and description is also given to GameObject constructor
        public Player(string name, string desc) : base(new string[] {"me","inventory"}, name, desc)
        {
        }

        // Find item from inventory
        public GameObject Locate(string id)
        {
            // First check. They are what is to be located. Locate("inventory")
            if (AreYou(id))
            {
                return this;
            }
            // Second check. They have what is being located.
            else if (_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }
            //Third Check. The location has the item they are looking for.
            else if (_location != null)
            {
                return _location.Locate(id);
            }

            return null;
        }

        public override string FullDescription
        {
            get
            {
                // Name, description and details of items in inventory
                string playerDescription = "You are " + 
                    base.Name +
                    ", " +
                    base.FullDescription + 
                    ".\n\nYou are carrying:" +
                    this.Inventory.ItemList;

                return playerDescription;
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }

        public Location Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }


    }
}
