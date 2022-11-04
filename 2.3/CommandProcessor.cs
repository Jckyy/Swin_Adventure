using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class CommandProcessor
    {
        private List<Command> _commands = new List<Command>();
        private Command _cmd;
                
        public CommandProcessor(string[] userInput)
        {
            //_text = userInput;
        }

        public string NewCommand(string[] text)
        {
            if (text == null)
            {
                return "Invalid command. Type 'help' to see the available commands.";
            }



            return null;
        }


    }
}
