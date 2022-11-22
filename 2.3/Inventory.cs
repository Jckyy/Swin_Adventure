using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Inventory
    {

        private List<Item> _items = new List<Item>();
        public Inventory()
        {
        }

        // Search if you have item
        public bool HasItem(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                { 
                    return true;
                }

            }
            return false;
        }

        // Add to item list
        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        // Find item in inventory, remove it, and return it.
        public Item Take(string id)
        {
            Item takeItem = this.Fetch(id);
            _items.Remove(takeItem);
            return takeItem;
        }

        // Return Item Object
        public Item Fetch(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return null;
        }

        // Return string with the each items ShortDescription
        public string ItemList
        {
            get 
            {
                string itemList = "";
                foreach (Item i in _items)
                {
                    itemList += "\n\t-" + i.ShortDescription;
                }

                return itemList;
            }
        }
    }
}
