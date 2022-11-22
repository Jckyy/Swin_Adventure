using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Move : Command
    {
        // Identified by the words, "move", "go", "head", "leave"                
        public Move() : base(new string[] { "move", "go", "head", "leave" })
        { }

        public override string Execute(Player p, string[] text)
        {
            // Invalid string[]
            if (text.Length != 2 & text.Length != 3)
            {
                return "I don't know how to move like that.";
            }

            if (!this.AreYou(text[0]))
            {
                return "Error in move input.";
            }


            // Get either ID to check
            string id;
            if (text.Length == 2)
            {
                id = text[1];
            }
            else if (text.Length == 3)
            {
                id = text[2];
            }
            else
            {
                return "Could not move the player.";
            }

            // 1. Find path that matches the ID
            Path _path = p.Locate(id) as Path;

            if (_path == null)
            {
                return "Could not move the player.";
            }

            return _path.Move(p, id);
        }   
    }
}