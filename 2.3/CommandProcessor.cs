using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class CommandProcessor
    {
        private List<Command> _commands = new List<Command>();

        public CommandProcessor()
        {
            _commands.Add(new Look());
            _commands.Add(new Move());
        }

        public string Execute(Player p, string[] text)
        {
            // Check every command that is available in _commands list
            foreach (Command cmd in _commands)
            {
                if (cmd.AreYou(text[0].ToLower()))
                {
                    return cmd.Execute(p, text);
                }
            }

            // Scuffed help command.
            if (text[0].ToLower() == "help")
            {
                return Help;
            }

            return "Invalid command. Enter 'help' to see the list of available commands.";
        }

        private string Help
        {
            get
            {
                string helpString = "The list of the available commands:\n\t-help\n";

                foreach (Command cmd in _commands)
                {
                    helpString += String.Format("\t-{0}\n", cmd.FirstID);
                }

                return helpString;

            }
        }
    }
}
