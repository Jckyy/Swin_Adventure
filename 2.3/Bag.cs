using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Bag : Item, IHaveInventory
    {
        private Inventory _inventory = new Inventory();

        public Bag(string[] ids, string name, string desc) : base(ids, name, desc)
        { }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            return _inventory.Fetch(id);
        }

        public override string FullDescription
        {
            get
            {
                string full_description = "In the " + Name + " you can see:\n";

                full_description += _inventory.ItemList;

                return full_description;
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }
    }
}
