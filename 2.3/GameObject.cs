using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    // Every GameObject is an IdentifiableObject
    public abstract class GameObject : IdentifiableObject
    {
        private string _description;
        private string _name;

        // GameObject constructor also passes the ids received to IdentifiableObject
        public GameObject(string[] ids, string name, string desc) : base(ids)
        { 
            _name = name;
            _description = desc;
        }

        public string Name
        {
            get { return _name; }
        }

        // Returns "Name (First Identifier)"
        public string ShortDescription
        {
            get
            {
                //return _name + " (" + _ids[0] + ")";
                return _name + " (" + this.FirstID + ")";
            }
        }

        public virtual string FullDescription
        {
            get
            {
                return _description;
            }
        }
    }
}
