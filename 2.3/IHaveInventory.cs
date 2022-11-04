using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public interface IHaveInventory
    {
        // You don't need to put anything in the interface methods
        // The children will implement the things that it will do.
        GameObject Locate(string id);

        public string Name 
        {
            get; 
        }
    }
}
